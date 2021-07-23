using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scraper;
using CsvExport;
using static Models.ScraperModels;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;

//using RestSharp;

namespace MailScraper
{

    
    public partial class MainForm : Form
    {

        string current_status = "";
        bool scrappingFinished = false;
        bool isInProcessing = false;
        List<string> data = new List<string>();



        AutoResetEvent mre = new AutoResetEvent(true);
        public static List<string> lstEmails = new List<string>();
        public static List<string> lstFb = new List<string>();
        public static List<string> lstTw = new List<string>();
        public static List<string> lstIg = new List<string>();
        public static List<string> lstLi = new List<string>();




        ScraperMain s = new ScraperMain();
        Export ce = new Export();
        List<ScrapeProxy> proxies = new List<ScrapeProxy>();
        ScrapeProxy proxy = new ScrapeProxy();
        DataTable leadsTable;
        List<Lead> leads = new List<Lead>();
        List<string> urls;
        List<int> curCount = new List<int>();
        string totCount = "";
        int processNo = 0;
        int minIOC = 0;
        int maxIOC = 0;
        Thread[] iThread = null;
        bool waitForStop = false;
        bool isStop = false;
        private DataSet _dataSetProxyObject = null;
        private DataTable _dataTableProxyObject = null;

        public MainForm()
        {
            InitializeComponent();
            InitializeLeadsTable();
            InitializeProxies();
            // Get the current settings.

            int minWorker;
            int maxWoker;

            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            ThreadPool.GetMaxThreads(out maxWoker, out maxIOC);
            txtThreadCount.Minimum = minWorker;
            txtThreadCount.Maximum = maxWoker;

        }

        public void InitializeProxies()
        {

        }

        public void InitDataLists()
        {
            
            data.Clear();
            lstEmails.Clear();
            lstFb.Clear();
            lstTw.Clear();
            lstIg.Clear();
            lstLi.Clear();
            lstCompleted.Items.Clear();
        }

        private async void BtnScrape_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (isInProcessing == true)
            {
                MessageBox.Show("Processing Urls.....");
                return;
            }

            isInProcessing = true;
            isStop = false;
            InitDataLists();

            
            current_status = "Initializing parameters to begin the scrapping...";

            progBar.Value = 0;
            progBar.Minimum = 0;
            lblprocessCnt.Text = String.Format("{0} of {1}", lstCompleted.Items.Count, lstUrl.Items.Count);
            btnrunscraper.Enabled = false;


            btnImport.Enabled = false;
            //nExportCSV_Click. 

            Thread iThread = new Thread(Scrape);
            iThread.Start();
            //if(lstUrl.Items.Count == 0)
            //{
            //    MessageBox.Show("URLs is empty");
            //}
            //else
            //{
            //    urls = lstUrl.Items.Cast<object>().Select(o => o.ToString()).ToList();
            //    totCount = $"{ urls.Count() } Scraped Websites";
            //    curCount.Add(0);
            //    processNo++;
            //    int i = 0;
            //    GrpUrlManager.Enabled = false;
            //    btnScrape.Enabled = false;
            //    int thread_size = Convert.ToInt32(txtThreadCount.Value);
            //    ThreadPool.SetMaxThreads(thread_size, thread_size);
            //    var tasks = new List<Task>();
            //    foreach (var url in urls)
            //    {
            //        tasks.Add(Task.Run(() => RunScraper(url, progBar, urls.Count, processNo)));
            //        Console.WriteLine($"thread { processNo }; item {i}");
            //        i++;
            //    }
            //    await Task.WhenAll(tasks);
                
            //    btnScrape.Enabled = true;
            //    GrpUrlManager.Enabled = true;
            //}
            


        }

        public static DataTable DataScraped;

        public void MakeDataTable()
        {
            DataScraped = new DataTable("MyData");
            DataColumn column;
            DataRow row;

            string[] columns = ("url,email,facebook,twitter,instagram,linkedin").Split(',');

            for (int i = 0; i <  columns.Count(); i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = columns[i];
                column.AutoIncrement = false;
                column.Caption = columns[i];
                column.ReadOnly = false;
                //column.AllowDBNull = true;
                column.Unique = false;
                DataScraped.Columns.Add(column);
            }
           

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = DataScraped.Columns["id"];
            DataScraped.PrimaryKey = PrimaryKeyColumns;
        }

        private  void Scrape()
        {


            MakeDataTable();

            //ScrapeProxy p = (ScrapeProxy) advancedDataGridView_proxies.SelectedRows[0];
            ScrapeProxy p = null;
            if(this.checkUserProxy.Checked)
            {
                if(advancedDataGridView_proxies.SelectedRows.Count > 0)
                {
                    string strIP = Convert.ToString(advancedDataGridView_proxies.SelectedRows[0].Cells[0].Value);
                    int nPort = Convert.ToInt32(advancedDataGridView_proxies.SelectedRows[0].Cells[1].Value);
                    p = proxies.Where(o => o.Address == strIP && o.Port == nPort).FirstOrDefault();
                }
            }
            
            data.Clear();

            urls = lstUrl.Items.Cast<object>().Select(o => o.ToString()).ToList();
            int i = 0;
            int j = 1;
            iThread = new Thread[urls.Count()];
            foreach (var url in urls)
            {
                current_status = "Please wait while program performs the operation...";
                iThread[i] = new Thread(() => RunScraper(url, p));
                iThread[i].Start();
                i++;
                //                 if(i >= 200 * j)
                //                 {
                //                     while(true)
                //                     {
                //                         Thread.Sleep(100);
                //                         if (progBar.Value == i)
                //                         {
                //                             //MessageBox.Show("hello");
                //                             break;
                //                         }
                //                             
                //                     }
                //                     j++;
                //                            }
            }

        }

       
        private void RunScraper(string url, ScrapeProxy p)
        {
            string src = "";


            //mre.WaitOne();
            MailScraper_v2.cls.DataScraper ds = new MailScraper_v2.cls.DataScraper(url, p);
            //DataRow row;

            if (this.isStop)
                return;


            //row = DataScraped.NewRow();
            //row["url"] = url;
            //DataScraped.Rows.Add(row);

            data.Add(String.Format("{0}|{1}|{2}", url,"url",url));

            src = (src == "" ? url : "");

            if (ds.Emails != "")
            {
                string[] emls = ds.Emails.ToLower().Split(',');
                for (int i = 0; i < emls.Count(); i++)
                {
                    if (lstEmails.IndexOf(emls[i]) <0)
                    {
                        lstEmails.Add(emls[i]);
                        //data.Add("\"" + src + "\",\"" + emls[i] + "\"");
                        data.Add(String.Format("{0}|{1}|{2}", url, "email", emls[i]));
                        src = "";
                    }
                }
            }

            if (ds.Facebook != "")
            {
                string[] socialMedia = ds.Facebook.ToLower().Split(',');
                for (int i = 0; i < socialMedia.Count(); i++)
                {
                    if (lstFb.IndexOf(socialMedia[i]) < 0)
                    {
                        lstFb.Add(socialMedia[i]);
                        //data.Add("\"" + src + "\",\"\",\"" + socialMedia[i] + "\"");
                        data.Add(String.Format("{0}|{1}|{2}", url, "facebook", socialMedia[i]));
                        src = "";
                    }
                }
            }

            if (ds.Twitter != "")
            {
                string[] socialMedia = ds.Twitter.ToLower().Split(',');
                for (int i = 0; i < socialMedia.Count(); i++)
                {
                    if (lstTw.IndexOf(socialMedia[i]) < 0)
                    {
                        lstTw.Add(socialMedia[i]);
                        //data.Add("\"" + src + "\",\"\",\"" + socialMedia[i] + "\"");
                        data.Add(String.Format("{0}|{1}|{2}", url, "twitter", socialMedia[i]));
                        src = "";
                    }
                }
            }

            if (ds.Instagram != "")
            {
                string[] socialMedia = ds.Instagram.ToLower().Split(',');
                for (int i = 0; i < socialMedia.Count(); i++)
                {
                    if (lstIg.IndexOf(socialMedia[i]) < 0)
                    {
                        lstIg.Add(socialMedia[i]);
                        //data.Add("\"" + src + "\",\"\",\"" + socialMedia[i] + "\"");
                        data.Add(String.Format("{0}|{1}|{2}", url, "instagram", socialMedia[i]));
                        src = "";
                    }
                }
            }

            if (ds.LinkedIn != "")
            {
                string[] socialMedia = ds.LinkedIn.ToLower().Split(',');
                for (int i = 0; i < socialMedia.Count(); i++)
                {
                    if (lstLi.IndexOf(socialMedia[i]) < 0)
                    {
                        lstLi.Add(socialMedia[i]);
                        //data.Add("\"" + src + "\",\"\",\"" + socialMedia[i] + "\"");
                        data.Add(String.Format("{0}|{1}|{2}", url, "linkedin", socialMedia[i]));
                        src = "";
                    }
                }
            }

            lock (this)
            {
                UpdateUI(url);
            }
        }






        private void UpdateUI(string url) {
            if (InvokeRequired) //set current number of url scraped
            {
                BeginInvoke(new Action(() =>
                {
                    if (isStop)
                        return;
                    //curCount[p]++;
                    //prog.Value = (curCount[p] * 100)/totUrl;
                    if(progBar.Value < progBar.Maximum)
                        progBar.Value += 1;
                    btndownloadresult.Enabled = (progBar.Value == progBar.Maximum);
                    btnImport.Enabled = (progBar.Value == progBar.Maximum);
                    if (progBar.Value == progBar.Maximum)
                    {
                        scrappingFinished = true;
                        isInProcessing = false;
                    }
                    //lblprocessCnt.Text = $"{ curCount[p].ToString() }/{ totCount }";
                    lstCompleted.Items.Add(String.Format("Done! {0}", url));
                    lstCompleted.SelectedIndex = lstCompleted.Items.Count - 1;
                    //txtLogs.AppendText($"{ url } -- Done{ Environment.NewLine }");
                    lblprocessCnt.Text = String.Format("{0} of {1}", lstCompleted.Items.Count, lstUrl.Items.Count);

                }));
            }
        }









        private void BtnAddUrl_Click(object sender, EventArgs e)
        {
            if (txtUrl.Text != "")
            {
                urls = ParseUrls(txtUrl.Text);
                lstUrl.Items.AddRange(urls.ToArray());
                lblUrlCount.Text = $"No. of urls: { lstUrl.Items.Count.ToString() }";
                InitDataLists();
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            //ImportUrlFromFile_old()

            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            current_status = "Awaiting for the file to import...";
            txtnumberofurls.Text = "0";
            txtduplicatesurls.Text = "0";
            txtuniqueurls.Text = "0";

            List<String> temp = new List<string>();
            data.Clear();
            temp.Clear();
            lstUrl.Items.Clear();
            lstCompleted.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt |CSV files(*.csv)|*.csv";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                current_status = "Reading file...";
                StreamReader sr = new StreamReader(ofd.FileName);
                if(ofd.FileName.Substring(ofd.FileName.Length - 5).Contains("txt") || ofd.FileName.Substring(ofd.FileName.Length - 5).Contains("csv"))
                {
                    Thread t = new Thread(() => LoadBatchFile(sr));
                    t.Start();
                }
            }
            else {
                current_status = "Import terminated! No file selected.";
            }

        }



        private void LoadBatchFile_CSV(StreamReader sr)
        {

            current_status = "Loading url to the list...";
            List<String> temp = new List<string>();
            int ctr_all = 0;
            int ctr_unique = 0;
            int ctr_dups = 0;
            string str = "";
            do
            {
                ctr_all++;
                String lineContent = sr.ReadLine();
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        progBar.Minimum = 0;
                        progBar.Value = 0;
                        txtnumberofurls.Text = ctr_all.ToString();
                        if (temp.IndexOf(lineContent) < 0 && lineContent != "")
                        {
                            str = lineContent.Substring(0, 4);

                            if (str == "http")
                            {
                                temp.Add(lineContent);
                                this.lstUrl.Items.Add(lineContent);
                            }
                            else
                            {
                                temp.Add("http://" + lineContent);
                                this.lstUrl.Items.Add("http://" + lineContent);
                            }

                            ctr_unique++;
                            txtuniqueurls.Text = ctr_unique.ToString();
                            lblUrlCount.Text = String.Format("{0} URL(s) ready for process!", ctr_unique);
                            current_status = String.Format("{0} URLs set for Scrapping!", ctr_unique); ;
                            progBar.Maximum = ctr_unique;
                        }
                        else
                        {
                            ctr_dups++;
                            txtduplicatesurls.Text = ctr_dups.ToString();
                        }
                    }));
                }

            } while (!sr.EndOfStream);

        }

        private void LoadBatchFile(StreamReader sr)
        {
           
            current_status = "Loading url to the list...";
            List<String> temp = new List<string>();
            int ctr_all = 0;
            int ctr_unique = 0;
            int ctr_dups = 0;
            string str = "";
            do
            {
                ctr_all++;
                String lineContent = sr.ReadLine();
                
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        progBar.Minimum = 0;
                        progBar.Value = 0;
                        txtnumberofurls.Text = ctr_all.ToString();
                        if (temp.IndexOf(lineContent) < 0 && lineContent !="")
                        {
                            str = lineContent.Substring(0, 4);

                            if (str == "http")
                            {
                                temp.Add(lineContent);
                                this.lstUrl.Items.Add(lineContent);
                            }
                            else
                            {
                                temp.Add("http://" + lineContent);
                                this.lstUrl.Items.Add("http://" +lineContent);
                            }
                            
                            ctr_unique++;
                            txtuniqueurls.Text = ctr_unique.ToString();
                            lblUrlCount.Text = String.Format("{0} URL(s) ready for process!", ctr_unique);
                            current_status = String.Format("{0} URLs set for Scrapping!", ctr_unique); ;
                            progBar.Maximum = ctr_unique;
                        }
                        else
                        {
                            ctr_dups++;
                            txtduplicatesurls.Text = ctr_dups.ToString();
                        }
                    }));
                }

            } while (!sr.EndOfStream);

        }

        public bool TestProxies(String address, int port)
        {

            /*
              var response = new RestClient
            {
                //this site is no longer up
                BaseUrl = "https://webapi.theproxisright.com/",
                Proxy = new WebProxy(address, port)
            }.Execute(new RestRequest
            {
                Resource = "api/ip",
                Method = Method.GET,
                Timeout = 10000,
                RequestFormat = DataFormat.Json
            });
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            bool success = (response.Content == address);
            return success;
             */

            return false;
        }


        private static bool CanPing(string strIP, int intPort)
        {

            bool blProxy = false;
            try
            {
                TcpClient client = new TcpClient(strIP, intPort);

                blProxy = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return blProxy;
        }

        private void BtnImportProxy_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ImportProxyFromFile();
        }

        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.DefaultExt = "csv";
            //sfd.FileName = "Export.csv";
            //sfd.AddExtension = true;
            //sfd.Filter = "(*.csv)|";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    ToDataTable(leads);
            //    ce.ToCSV(leads, sfd.FileName);
            //}


            if (data.Count <=0)
            {
                MessageBox.Show("Result cache is empty!", "Download", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else
            {

                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.DefaultExt = "csv";
                //sfd.FileName = "Export.csv";
                //sfd.AddExtension = true;
                //sfd.Filter = "(*.csv)|";
                //if (sfd.ShowDialog() == DialogResult.OK)
                //{
                //    ToDataTable(leads);
                //    ce.ToCSV(leads, sfd.FileName);
                //}


                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = @"Documents";
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.Title = "Export CSV File";
                sfd.FileName = String.Format("ScrapedData_{0}.csv", DateTime.Now.ToString("ddMMyyyy_HHmmss"));
                sfd.DefaultExt = "csv";
                sfd.ShowDialog();




                MakeDataTable();


                bool urlHasRecord = false;
                DataRow row;
                for (int i = 0; i < data.Count; i++)
                {
                    string[] content = data[i].Split('|');

                    string c_url = content[0].ToString();
                    string c_type = content[1].ToString();
                    string c_value = content[2].ToString();

                    urlHasRecord = false;
                    foreach (DataRow rw in DataScraped.Rows)
                    {
                        if ((rw["url"].ToString() == c_url) && (rw[c_type].ToString() == ""))
                        {
                            urlHasRecord = true;
                            rw.SetField(c_type, c_value);
                            break;
                        }
                    }
                    if (!urlHasRecord)
                    {
                        row = DataScraped.NewRow();
                        //row["url"] = c_url;
                        row[c_type] = c_value;
                        DataScraped.Rows.Add(row);
                    }


                }















                StreamWriter sw = new StreamWriter(sfd.FileName);
                sw.WriteLine("\"Url\",\"Email\",\"Facebook\",\"Twitter\",\"Instagram\",\"LinkedIn\"");
                for (int i = 0; i < DataScraped.Rows.Count; i++)
                {
                    string writeThis = String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"", DataScraped.Rows[i][0].ToString(), DataScraped.Rows[i][1].ToString(), DataScraped.Rows[i][2].ToString(), DataScraped.Rows[i][3].ToString(), DataScraped.Rows[i][4].ToString(), DataScraped.Rows[i][5].ToString());
                    sw.WriteLine(writeThis);
                }
                sw.Close();

                MessageBox.Show("Export successfully completed!", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }




        }

        private void BtnClearProxies_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            proxies = new List<ScrapeProxy>();
            advancedDataGridView_proxies.DataBindings.Clear();
            _dataTableProxyObject.Clear();
        }
        private void BtnClearUrlList_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lstUrl.Items.Clear();
            lblUrlCount.Text = $"No. of urls: { lstUrl.Items.Count }";
        }

        delegate void SetTextCallback(string text);

        AutoResetEvent are = new AutoResetEvent(true);

        

        private List<string> ParseUrls(string urlInput)
        {
            var urls = new List<string>();

            var i = urlInput.Split(',');
            foreach (var _i in i)
            {
                urls.Add(_i.Trim());
            }

            return urls;
        }

        private ProgressBar CreateProgressBar(int procNo)
        {
            Label lbl = new Label
            {
                Name = $"lblProcess{processNo}",
                Text = $"Process #{ procNo }"
            };
            ProgressBar prog = new ProgressBar();
            prog.Width = 500;
            prog.Name = $"progProcess{ procNo }";
            //layoutProcs.Controls.Add(lbl);
            //layoutProcs.Controls.Add(prog);

            return prog;
        }

      

        private void InitializeLeadsTable()
        {
            leadsTable = new DataTable("Leads");
            DataColumn dtCol;

            dtCol = new DataColumn();
            dtCol.DataType = typeof(string);
            dtCol.ColumnName = "website";
            dtCol.Caption = "Website";
            dtCol.ReadOnly = false;
            leadsTable.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = typeof(string);
            dtCol.ColumnName = "email";
            dtCol.Caption = "Email";
            dtCol.ReadOnly = false;
            leadsTable.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = typeof(string);
            dtCol.ColumnName = "facebook";
            dtCol.Caption = "Facebook";
            dtCol.ReadOnly = false;
            leadsTable.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = typeof(string);
            dtCol.ColumnName = "twitter";
            dtCol.Caption = "Twitter";
            dtCol.ReadOnly = false;
            leadsTable.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = typeof(string);
            dtCol.ColumnName = "linkedin";
            dtCol.Caption = "LinkedIn";
            dtCol.ReadOnly = false;
            leadsTable.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = typeof(string);
            dtCol.ColumnName = "instagram";
            dtCol.Caption = "Instagram";
            dtCol.ReadOnly = false;
            leadsTable.Columns.Add(dtCol);
        }

        private void ImportUrlFromFile_old()
        {
            string filePath;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                if (filePath.Substring(filePath.Length - 3) == "txt")
                {
                    string[] lines = System.IO.File.ReadAllLines(filePath);
                    lstUrl.Items.AddRange(lines);
                }
                if (filePath.Substring(filePath.Length - 3) == "csv")
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        List<string> listA = new List<string>();
                        List<string> listB = new List<string>();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            for (int i = 0; i < values.Length - 1; i++)
                            {
                                if (values[i] != "")
                                    lstUrl.Items.Add(values[i]);
                            }
                        }
                    }
                }
            }
            lblUrlCount.Text = $"No. of urls: { lstUrl.Items.Count.ToString() }";
        }



        private void ImportProxyFromFile()
        {

            string filePath;
            proxies = new List<ScrapeProxy>();
            OpenFileDialog ofd = new OpenFileDialog();
            int nValidProxy = 0;
            int index = 0;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                if (filePath.Substring(filePath.Length - 3) == "txt" || filePath.Substring(filePath.Length - 3) == "csv")
                {
                    char chSplitKey = ':';
                    if (filePath.Substring(filePath.Length - 3) == "csv")
                        chSplitKey = ',';
                    string[] lines = System.IO.File.ReadAllLines(filePath);
                    
                    foreach (var line in lines)
                    {
                        var p = new ScrapeProxy();
                        p.id = index;
                        string[] strDatas = line.Split(chSplitKey);
                        if(strDatas.Length > 1 )
                        {
                            p.Address = strDatas[0];
                            try
                            {
                                p.Port = Convert.ToInt32(strDatas[1]);
                            }
                            catch(Exception e)
                            {
                                p.Port = -1;
                            }
                            

                            if(strDatas.Length > 3)
                            {
                                p.Username = line.Split(chSplitKey)[2];
                                p.Password = line.Split(chSplitKey)[3];
                            }
                            else
                            {
                                p.Username = "";
                                p.Password = "";
                            }

                            
                            if (strDatas.Length > 4)
                                p.NumberThreads = Convert.ToInt32(line.Split(chSplitKey)[4]);
                            else
                                p.NumberThreads = 0;
                            //if(CanPing(p.Address, p.Port))
                            {
                                proxies.Add(p);
                                nValidProxy++;
                            }
                            
                            index++;
                        }
                    }
                }
            }

            _dataSetProxyObject = new DataSet();
            _dataTableProxyObject = new DataTable();
            //_dataTableProxyObject.ColumnChanged += new DataColumnChangeEventHandler(_dataTableProxyObject_ColumValChanged);
            bindingSource_proxies.DataSource = _dataSetProxyObject;

            advancedDataGridView_proxies.SetDoubleBuffered();
            advancedDataGridView_proxies.DataSource = bindingSource_proxies;
            advancedDataGridView_proxies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            _dataTableProxyObject = _dataSetProxyObject.Tables.Add("Proxies");

            //_dataTableProxyObject.Columns.Add(" ", typeof(bool));

            //_dataTableProxyObject.Columns.Add("No", typeof(int));
            _dataTableProxyObject.Columns.Add("Proxy Address", typeof(string));
            _dataTableProxyObject.Columns.Add("Port", typeof(int));
            _dataTableProxyObject.Columns.Add("UserName", typeof(string));
            _dataTableProxyObject.Columns.Add("Password", typeof(string));
            _dataTableProxyObject.Columns.Add("Number of Threads", typeof(int));

            bindingSource_proxies.DataMember = _dataTableProxyObject.TableName;
            bindingSource_proxies.RemoveFilter();

            this.lblCntProxies.Text = string.Format("{0} proxies are loaded / {1} valied", index, nValidProxy);

            if (proxies.Count == 0)
                return;

            


            
            foreach (ScrapeProxy p in proxies)
            {
                object[] newrow = new object[] {
         //               false,
                        //p.id,
                        p.Address,
                        p.Port,
                        p.Username,
                        p.Password,
                        p.NumberThreads
                    };
                _dataTableProxyObject.Rows.Add(newrow);
            }

           
        }

        
        private string cleanUrl(string s)
        {
            s = Regex.Replace(s, "'[\\s\\S]*", string.Empty);
            s = Regex.Replace(s, "/?[\\s\\S]*", string.Empty);
            return s;
        }

        private void ToDataTable(List<Lead> leads)
        {
            foreach (var lead in leads)
            {
                if (lead != null)
                {
                    DataRow dr = leadsTable.NewRow();
                    dr["website"] = lead.Website;
                    dr["email"] = lead.Email;
                    dr["facebook"] = lead.Facebook;
                    dr["twitter"] = lead.Twitter;
                    dr["linkedin"] = lead.LinkedIn;
                    dr["instagram"] = lead.Instagram;
                    leadsTable.Rows.Add(dr);
                }
            }
        }

        
        private void BtnPaste_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string s = Clipboard.GetText();
            string[] lines = s.Split('\n');

            lstUrl.Items.AddRange(lines);
            lblUrlCount.Text = $"No. of urls: { lstUrl.Items.Count }";
        }

        private void BtnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lstUrl.SelectedIndex != -1)
            {
                int last_selected = lstUrl.SelectedIndex;
                int last_index = lstUrl.Items.Count - 1;
                lstUrl.Items.RemoveAt(lstUrl.SelectedIndex);
                if(last_selected!= last_index)
                {
                    lstUrl.SetSelected(last_selected, true);
                }
                lblUrlCount.Text = $"No. of urls: { lstUrl.Items.Count }";
            }
            
        }



        private void DisableInputs(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = '\0';
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            current_status = "Program ready. Standby mode!";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblstatus.Text = current_status;
            btnrunscraper.Enabled = (lstUrl.Items.Count > 0);
//            btnrunscraper.Enabled

            if (scrappingFinished)
            {
                scrappingFinished = false;
                btnImport.Enabled = true;
                MessageBox.Show("Operation successfully completed!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                current_status = "Done! Result is now ready for download! Standby Mode";
            }
        }

        private void stopThreads()
        {
            foreach (Thread t in iThread)
            {
                t.Abort();
            }
            waitForStop = false;
            MessageBox.Show("Stopped All Current Processing!!\n Try to do new job", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.waitForStop)
            {
                MessageBox.Show("Wait for Stopping Process!!", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                
            if (btnrunscraper.Enabled)
            {
                waitForStop = true;
                isInProcessing = false;
                isStop = true;
                progBar.Value = 0;
                btndownloadresult.Enabled = true;
                btnImport.Enabled = true;

                Thread t = new Thread(stopThreads);
                t.Start();
                
            }
        }

        /*
          private void advancedDataGridView_proxies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex
            if(e.ColumnIndex == 0)
            {
                
                bool isCheck = Convert.ToBoolean(advancedDataGridView_proxies.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if(isCheck)
                {
                    for(int i = 0; i < advancedDataGridView_proxies.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                            continue;
                        bool isCheck2 = Convert.ToBoolean(advancedDataGridView_proxies.Rows[i].Cells[e.ColumnIndex].Value);
                        if (isCheck2)
                            advancedDataGridView_proxies.Rows[i].Cells[e.ColumnIndex].Value = false;
                    }
                }
            }
            
        }
         */

    }
}
