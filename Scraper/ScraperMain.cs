using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static Models.ScraperModels;

namespace Scraper
{
    public class ScraperMain
    {
        public Lead ProcessUrls(string url, ScrapeProxy proxy, int connectionLimit)
        {
            var leads = new List<Lead>();

            string page = Navigate(url, proxy, connectionLimit);
            leads.Add(GetContacts(url, page));

            string aboutUsUrl = GetAboutUsUrl(page, url);
            if (aboutUsUrl != "")
                page = Navigate(aboutUsUrl, proxy, connectionLimit);
            if (page != "")
                leads.Add(GetContacts(url, page));

            string contactUsUrl = GetContactUsUrl(page, url);
            if(contactUsUrl != "")
                page = Navigate(contactUsUrl, proxy, connectionLimit);
            if (page != "")
                leads.Add(GetContacts(url, page));

            return MergeLeads(leads);
        }

        private string Navigate(string url, ScrapeProxy p,int connectionLimit)
        {
            CookieContainer cookies;
            HttpWebRequest webRequest;
            HttpWebResponse response;
            StreamReader responseReader;
            string responseData = string.Empty;

            try
            {
                string strUrl = url;
                string postData = "";
                cookies = new CookieContainer();
                webRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                //sets the connection limit
                webRequest.ServicePoint.ConnectionLimit = connectionLimit > webRequest.ServicePoint.ConnectionLimit ? webRequest.ServicePoint.ConnectionLimit : connectionLimit;
                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";
                webRequest.ContentLength = postData.Length;
                webRequest.CookieContainer = cookies;
                webRequest.Timeout = Timeout.Infinite;
                webRequest.AllowAutoRedirect = true;
                SetProxy(ref webRequest, p);
                response = (HttpWebResponse)webRequest.GetResponse();
                response.Cookies = webRequest.CookieContainer.GetCookies(webRequest.RequestUri);
                responseReader = new StreamReader(response.GetResponseStream(), true);
                responseData = responseReader.ReadToEnd();
                response.Close();
                responseReader.Close();
            }
            catch 
            {

            }

            return responseData;
        }

        private string GetAboutUsUrl(string html, string url)
        {
            var aboutUsUrl = "";
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html.ToLower());

            var node = doc.DocumentNode.SelectSingleNode("//a[contains(., 'about us')]");
            if (node == null)
                node = doc.DocumentNode.SelectSingleNode("//a[contains(., 'about')]");

            if (node != null)
            {
                aboutUsUrl = node.GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(aboutUsUrl))
                {
                    if (aboutUsUrl.Substring(0, 1) == "/")
                    {
                        if (url.Substring(url.Length - 1, 1) == "/")
                            aboutUsUrl = $"{url.Remove(url.Length - 1, 1)}{aboutUsUrl}";
                        else
                            aboutUsUrl = $"{url}{aboutUsUrl}";
                    }
                }
                
            }
            return aboutUsUrl;
        }

        private string GetContactUsUrl(string html, string url)
        {
            var contactUsUrl = "";
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html.ToLower());

            var node = doc.DocumentNode.SelectSingleNode("//a[contains(., 'contact us')]");
            if(node == null)
                node = doc.DocumentNode.SelectSingleNode("//a[contains(., 'contact')]");

            if (node != null)
            {
                contactUsUrl = node.GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(contactUsUrl))
                {
                    if (contactUsUrl.Substring(0, 1) == "/")
                    {
                        if (url.Substring(url.Length - 1, 1) == "/")
                            contactUsUrl = $"{url.Remove(url.Length - 1, 1)}{contactUsUrl}";
                        else
                            contactUsUrl = $"{url}{contactUsUrl}";
                    }
                }
                
            }
            return contactUsUrl;
        }

        private Lead GetContacts(string url, string html)
        {
            var emails = new List<string>();
            var lead = new Lead();

            const string MatchEmailPattern =
                                           @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            html = html.Replace("www.facebook.com/2008/fbml", ""); //exclude this facebook link which is found in almost all websites
            html = html.Replace("sentry.io", ""); //exclude this facebook link which is found in almost all websites
            html = html.Replace("wix.com", ""); //exclude this facebook link which is found in almost all websites
            html = html.Replace("sentry.wixpress.com", ""); //exclude this facebook link which is found in almost all websites

            MatchCollection matches = rx.Matches(html);
            //int noOfMatches = matches.Count;
            //Console.WriteLine("noOfMatches :" + noOfMatches);
            string em = string.Empty;
            foreach (Match match in matches)
            {
                 //get all emails found in the website
                if (!emails.Contains(match.Value))
                {
                    emails.Add(match.Value);
                    //em = $"{ em }\n{ match.Value }";
                }
            }

            //if (em.Length > 1)
            //    em = em.Remove(0, 1);

            lead.Website = url;
            //lead.Email = string.Join(";",emails);
            string facebook_regex = Regex.Match(html, "facebook.com/(.*?)\"").Value;
            string twitter_regex = Regex.Match(html, "twitter.com/(.*?)\"").Value;
            string linkedin_regex = Regex.Match(html, "linkedin.com/(.*?)\"").Value;
            string instagram_regex = Regex.Match(html, "instagram.com/(.*?)\"").Value;
            lead.Facebook = facebook_regex != string.Empty ? $"www.{ facebook_regex.Replace("\"", "") }" : "";
            lead.Twitter = twitter_regex.Replace("\"", "") != string.Empty ? $"www.{ twitter_regex.Replace("\"", "") }" : "";
            lead.LinkedIn = linkedin_regex.Replace("\"", "") != string.Empty ? $"www.{ linkedin_regex.Replace("\"", "") }" : "";
            lead.Instagram = instagram_regex.Replace("\"", "") != string.Empty ? $"www.{ instagram_regex.Replace("\"", "") }" : "";
            lead.ListEmails = emails;

            //removed not used code
            //var ls = new List<string>();
            //ls.Add("facebook.com/(.*?)\"");
            //ls.Add("linkedin.com/(.*?)\"");
            //ls.Add("twitter.com/(.*?)\"");
            //ls.Add("instagram.com/(.*?)\"");
            //ls.Add("medium.com/(.*?)\"");
            //ls.Add("youtube.com/channel/(.*?)\"");
            //ls.Add("wa.me/(.*?)\"");

            //foreach (var s in ls)
            //{
            //    MatchCollection mc = Regex.Matches(html, s);
            //    foreach (Match m in mc)
            //    {
            //        if (!m.Value.Contains(@"instagram.com/p/")) //exclude instagram posts
            //            emails.Add($"www.{ m.Value.Replace("\"", "") }");
            //    }
            //}
            return lead;
        }
        private void SetProxy(ref HttpWebRequest wr, ScrapeProxy p)
        {
            if (p.Address!=string.Empty)
            {
                var proxy = new WebProxy(p.Address, p.Port);
                if (p.Username != string.Empty)
                {
                    var cred = new NetworkCredential(p.Username, p.Password);
                    proxy.Credentials = cred;
                }
                wr.Proxy = proxy;
            }
        }

        private Lead MergeLeads(List<Lead> leads)
        {
            var lead = new Lead();
            lead.ListEmails = new List<string>();
            lead.Email = leads.FirstOrDefault()?.ListEmails.FirstOrDefault(u=>u!=""&&u!=null);
            foreach (Lead l in leads)
            {
                lead.Website = l.Website;
                lead.ListEmails.AddRange(l.ListEmails);
                //lead.Email = string.Join(Environment.NewLine, lead.ListEmails.Distinct());
                //if (l.Email!="" && l.Email!=null)
                //{
                //    //lead.Email = SetLeadValue(lead.Email, l.Email);
                //    //lead.Email = lead.Email == null ? "" : lead.Email;
                //    //if (!lead.Email.Contains(l.Email))
                //    //{
                //    //    if (!l.Email.Contains(lead.Email))
                //    //        lead.Email = l.Email;
                //    //    else
                //    //        lead.Email = lead.Email=="" ? l.Email : l.Email.Replace(lead.Email, "");
                //    //}
                //}
                if(l.Facebook!="" && l.Facebook!=null)
                {
                    lead.Facebook = SetLeadValue(lead.Facebook, l.Facebook);
                    //lead.Facebook = lead.Facebook == null ? "" : lead.Facebook;
                    //if(!lead.Facebook.Contains(l.Facebook))
                    //    lead.Facebook = $"{ lead.Facebook }\n{  l.Facebook }";
                }
                if (l.Twitter!="" && l.Twitter!=null)
                {
                    lead.Twitter = SetLeadValue(lead.Twitter, l.Twitter);
                    //lead.Twitter = lead.Twitter == null ? "" : lead.Twitter;
                    //if(!lead.Twitter.Contains(l.Twitter))
                    //    lead.Twitter = $"{ lead.Twitter }\n{ l.Twitter }";
                }
                if (l.LinkedIn!=string.Empty && l.LinkedIn!=null)
                {
                    lead.LinkedIn = SetLeadValue(lead.LinkedIn, l.LinkedIn);
                    //lead.LinkedIn = lead.LinkedIn == null ? "" : lead.LinkedIn;
                    //if(!lead.LinkedIn.Contains(l.LinkedIn))
                    //    lead.LinkedIn = $"{ lead.LinkedIn }\n{ l.LinkedIn }";
                }
                if (l.Instagram!=string.Empty && l.Instagram!=null)
                {
                    lead.Instagram = SetLeadValue(lead.Instagram, l.Instagram);
                    //lead.Instagram = lead.Instagram == null ? "" : lead.Instagram;
                    //if(!lead.Instagram.Contains(l.Instagram))
                    //    lead.Instagram = $"{ lead.Instagram }\n{ l.Instagram }";
                }
            }
            lead.ListEmails = lead.ListEmails.Distinct().ToList();
                        
            //if (lead.Email != null)
            //    lead.Email = RemoveLeadingLineBreaks(lead.Email);
            if (lead.Facebook != null)
                lead.Facebook = RemoveLeadingLineBreaks(lead.Facebook);
            if (lead.Twitter != null)
                lead.Twitter = RemoveLeadingLineBreaks(lead.Twitter);
            if (lead.LinkedIn != null)
                lead.LinkedIn = RemoveLeadingLineBreaks(lead.LinkedIn);
            if (lead.Instagram != null)
                lead.Instagram = RemoveLeadingLineBreaks(lead.Instagram);

            return lead;
        }

        private string SetLeadValue(string str, string newStr)
        {
            str = str == null ? "" : str;
            if (!str.Contains(newStr))
            {
                if (!newStr.Contains(str))
                    str = newStr;
                else
                    str = str == "" ? newStr : newStr.Replace(str, "");
            }

            return str;
        }

        private string RemoveLeadingLineBreaks(string str)
        {
            if (str=="") return str;
            do
            {
                if (str.Substring(0, 1) == "\n")
                    str = str != string.Empty ? str.Remove(0, 1) : "";
                else
                    break;
            } while (true);

            return str;
        }
    }
}