using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using MailScraper;
using System.Net.Http;

namespace MailScraper_v2.cls
{
    class DataScraper
    {

        private static String pageContent = "";
        private static String exceptionError = "";
        public string EncounteredError = "";



        public string Emails = "";
        public string Facebook = "";
        public string Twitter = "";
        public string Instagram = "";
        public string LinkedIn = "";



        //public DataScraper(string _URL)
        //{
        //    string _PageContent = null;
        //    try
        //    {

        //        // Open a connection
        //        HttpWebRequest _HttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(_URL);

        //        // You can also specify additional header values like the user agent or the referer: (Optional)
        //        _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
        //        _HttpWebRequest.Referer = "http://www.google.com/";


        //        // set timeout for 10 seconds (Optional)
        //        _HttpWebRequest.Timeout = 10000;


        //        // Request response:
        //        WebResponse _WebResponse = _HttpWebRequest.GetResponse();


        //        // Open data stream:
        //        Stream _WebStream = _WebResponse.GetResponseStream();

        //        // Create reader object:
        //        StreamReader _StreamReader = new StreamReader(_WebStream);


        //        // Read the entire stream content:
        //        _PageContent = _StreamReader.ReadToEnd();


        //        // Cleanup
        //        _StreamReader.Close();
        //        _WebStream.Close();
        //        _WebResponse.Close();

        //    }
        //    catch (Exception _Exception)
        //    {
        //        // Error
        //        Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
        //    }
        //}

        public void DataScraper_1(string str_url)
        {

            //WebProxy wp = new WebProxy("170.83.176.163", 12345);
            //NetworkCredential netcredit = new NetworkCredential("oadvantage", "123");

            Emails = "";
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "MyRSSReader/1.0");
            wc.Encoding = System.Text.Encoding.UTF8;

         
            //wc.Proxy = wp;
            //wc.Credentials = netcredit;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                pageContent = wc.DownloadString(str_url);
            }
            catch (Exception ex)
            {
                EncounteredError = ex.Message;
            }

        }
        public string PageContent { get { return pageContent; } }

        public List<String> ExtractEmailAddresses()
        {
            List<string> eml = new List<string>();
            eml.Clear();

            // standard email pattern

            const string MatchEmailPattern =
                                          @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                          + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                          + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                          + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";

            // regex execution

            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);


            // data collection

            MatchCollection collectedMatches = rx.Matches(pageContent);

            foreach (Match match in collectedMatches)
            {
                if (!eml.Contains(match.Value))
                {
                    eml.Add(match.Value);
                    Emails += (Emails !=""? "," + match.Value  : match.Value);
                }
            }


            return eml;
        }

        public bool IsValidUri(Uri uri)
        {

            using (HttpClient Client = new HttpClient())
            {

                HttpResponseMessage result = Client.GetAsync(uri).Result;
                HttpStatusCode StatusCode = result.StatusCode;

                switch (StatusCode)
                {

                    case HttpStatusCode.Accepted:
                        return true;
                    case HttpStatusCode.OK:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public DataScraper(string str_url, Models.ScraperModels.ScrapeProxy p)
        {

            Emails = "";
            pageContent = "";
            TimedWebClient twc = new TimedWebClient();
            //WebClient wc = new WebClient();
            //             wc.Headers.Add("user-agent", "MyRSSReader/1.0");
            //             wc.Encoding = System.Text.Encoding.UTF8;
            //             if(p != null)
            //             {
            //                 wc.Proxy = new WebProxy(p.Address, p.Port);
            //                 wc.Credentials = new NetworkCredential(p.Username, p.Password);
            //             }

            twc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36");
            twc.Encoding = System.Text.Encoding.UTF8;
            if (p != null)
            {
                twc.Proxy = new WebProxy(p.Address, p.Port);
                if(p.Username != "" && p.Password != "")
                    twc.Credentials = new NetworkCredential(p.Username, p.Password);
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
               // if(IsValidUri(new Uri(str_url)))
                    pageContent = twc.DownloadString(str_url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "  url : " + str_url);
                pageContent = ex.Message;
            }

            try
            {
                Regex rx;
                List<string> unq = new List<string>();
                string sVal = "";


                // *Emails
                const string MatchEmailPattern =
                                                  @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                                  + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                                  + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                                  + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
                rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection collectedMatches = rx.Matches(pageContent);

                unq.Clear();
                foreach (Match match in collectedMatches)
                {
                    if (!unq.Contains(match.Value))
                    {
                        unq.Add(match.Value);
                        Emails += (Emails != "" ? "," + match.Value : match.Value);
                    }
                }


                // Facebook
                const string MatchFBPattern = "facebook.com/(.*?)\"";
                rx = new Regex(MatchFBPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                collectedMatches = rx.Matches(pageContent);

                unq.Clear();
                foreach (Match match in collectedMatches)
                {
                    if ((!unq.Contains(match.Value)) && (match.Value.ToLower().Contains("facebook")) && (match.Value.Length <= 100)) ;
                    {
                        sVal = match.Value;
                        sVal = (sVal != string.Empty ? $"www.{ sVal.Replace("\"", "") }" : "");

                        unq.Add(sVal);
                        Facebook += (Facebook != "" ? "," + sVal : sVal);
                    }
                }

                // Twitter
                const string MatchTwitterPattern = "twitter.com/(.*?)\"";
                rx = new Regex(MatchTwitterPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                collectedMatches = rx.Matches(pageContent);

                unq.Clear();
                foreach (Match match in collectedMatches)
                {
                    if ((!unq.Contains(match.Value)) && (match.Value.ToLower().Contains("twitter")) && (match.Value.Length <= 100)) ;
                    {
                        sVal = match.Value;
                        sVal = (sVal != string.Empty ? $"www.{ sVal.Replace("\"", "") }" : "");

                        unq.Add(sVal);
                        Twitter += (Twitter != "" ? "," + sVal : sVal);
                    }
                }

                // Instagram
                const string MatchInstagramPattern = "instagram.com/(.*?)\"";
                rx = new Regex(MatchInstagramPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                collectedMatches = rx.Matches(pageContent);

                unq.Clear();
                foreach (Match match in collectedMatches)
                {
                    if ((!unq.Contains(match.Value)) && (match.Value.ToLower().Contains("instagram")) && (match.Value.Length <= 100)) ;
                    {
                        sVal = match.Value;
                        sVal = (sVal != string.Empty ? $"www.{ sVal.Replace("\"", "") }" : "");

                        unq.Add(sVal);
                        Instagram += (Instagram != "" ? "," + sVal : sVal);
                    }
                }

                // LinkedIn
                const string MatchLinkedInPattern = "linkedin.com/(.*?)\"";
                rx = new Regex(MatchLinkedInPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                collectedMatches = rx.Matches(pageContent);

                unq.Clear();
                foreach (Match match in collectedMatches)
                {
                    if ((!unq.Contains(match.Value)) && (match.Value.ToLower().Contains("linkedin")) && (match.Value.Length <= 100)) ;
                    {
                        sVal = match.Value;
                        sVal = (sVal != string.Empty ? $"www.{ sVal.Replace("\"", "") }" : "");

                        unq.Add(sVal);
                        LinkedIn += (LinkedIn != "" ? "," + sVal : sVal);
                    }
                }












                //string facebook_regex = Regex.Match(pageContent, "facebook.com/(.*?)\"").Value;
                //string twitter_regex = Regex.Match(pageContent, "twitter.com/(.*?)\"").Value;
                //string linkedin_regex = Regex.Match(pageContent, "twitter.com/(.*?)\"").Value;
                //string instagram_regex = Regex.Match(pageContent, "instagram.com/(.*?)\"").Value;
                //lead.Facebook = facebook_regex != string.Empty ? $"www.{ facebook_regex.Replace("\"", "") }" : "";
                //lead.Twitter = twitter_regex.Replace("\"", "") != string.Empty ? $"www.{ twitter_regex.Replace("\"", "") }" : "";
                //lead.LinkedIn = linkedin_regex.Replace("\"", "") != string.Empty ? $"www.{ linkedin_regex.Replace("\"", "") }" : "";
                //lead.Instagram = instagram_regex.Replace("\"", "") != string.Empty ? $"www.{ instagram_regex.Replace("\"", "") }" : "";
                //lead.ListEmails = emails;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "222222222222");
            }







        }


    }
}
