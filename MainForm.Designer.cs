namespace MailScraper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnrunscraper = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblprocessCnt = new System.Windows.Forms.Label();
            this.lstUrl = new System.Windows.Forms.ListBox();
            this.btnAddUrl = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblUrlCount = new System.Windows.Forms.Label();
            this.btndownloadresult = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.lstCompleted = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtuniqueurls = new System.Windows.Forms.TextBox();
            this.txtduplicatesurls = new System.Windows.Forms.TextBox();
            this.txtnumberofurls = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCntProxies = new System.Windows.Forms.Label();
            this.checkUserProxy = new System.Windows.Forms.CheckBox();
            this.advancedDataGridView_proxies = new Zuby.ADGV.AdvancedDataGridView();
            this.btnClearProxies = new System.Windows.Forms.Button();
            this.btnImportProxy = new System.Windows.Forms.Button();
            this.btnClearUrlList = new System.Windows.Forms.Button();
            this.BtnPaste = new System.Windows.Forms.Button();
            this.BtnRemoveSelected = new System.Windows.Forms.Button();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtThreadCount = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bindingSource_proxies = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_proxies)).BeginInit();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreadCount)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_proxies)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(393, 250);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(270, 23);
            this.txtUrl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(393, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add a single url or multiple url separated by comma";
            // 
            // btnrunscraper
            // 
            this.btnrunscraper.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrunscraper.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnrunscraper.ImageKey = "Actions-go-next-view-page-icon.png";
            this.btnrunscraper.ImageList = this.imageList1;
            this.btnrunscraper.Location = new System.Drawing.Point(553, 409);
            this.btnrunscraper.Name = "btnrunscraper";
            this.btnrunscraper.Padding = new System.Windows.Forms.Padding(5);
            this.btnrunscraper.Size = new System.Drawing.Size(159, 45);
            this.btnrunscraper.TabIndex = 2;
            this.btnrunscraper.Text = "&Run Scraper";
            this.btnrunscraper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrunscraper.UseVisualStyleBackColor = true;
            this.btnrunscraper.Click += new System.EventHandler(this.BtnScrape_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Actions-go-next-view-page-icon.png");
            this.imageList1.Images.SetKeyName(1, "Actions-mail-receive-icon.png");
            // 
            // lblprocessCnt
            // 
            this.lblprocessCnt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprocessCnt.Location = new System.Drawing.Point(12, 617);
            this.lblprocessCnt.Name = "lblprocessCnt";
            this.lblprocessCnt.Size = new System.Drawing.Size(1075, 23);
            this.lblprocessCnt.TabIndex = 5;
            this.lblprocessCnt.Text = "0 of 0";
            this.lblprocessCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstUrl
            // 
            this.lstUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUrl.FormattingEnabled = true;
            this.lstUrl.ItemHeight = 17;
            this.lstUrl.Location = new System.Drawing.Point(12, 240);
            this.lstUrl.Name = "lstUrl";
            this.lstUrl.Size = new System.Drawing.Size(369, 344);
            this.lstUrl.TabIndex = 6;
            // 
            // btnAddUrl
            // 
            this.btnAddUrl.Location = new System.Drawing.Point(664, 250);
            this.btnAddUrl.Name = "btnAddUrl";
            this.btnAddUrl.Size = new System.Drawing.Size(48, 21);
            this.btnAddUrl.TabIndex = 7;
            this.btnAddUrl.Text = "+";
            this.btnAddUrl.UseVisualStyleBackColor = true;
            this.btnAddUrl.Click += new System.EventHandler(this.BtnAddUrl_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(393, 277);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(144, 50);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import url from txt or csv file";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // lblUrlCount
            // 
            this.lblUrlCount.AutoSize = true;
            this.lblUrlCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrlCount.Location = new System.Drawing.Point(12, 217);
            this.lblUrlCount.Name = "lblUrlCount";
            this.lblUrlCount.Size = new System.Drawing.Size(168, 17);
            this.lblUrlCount.TabIndex = 9;
            this.lblUrlCount.Text = "0 URL(s) ready for process!";
            // 
            // btndownloadresult
            // 
            this.btndownloadresult.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndownloadresult.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btndownloadresult.ImageKey = "Actions-mail-receive-icon.png";
            this.btndownloadresult.ImageList = this.imageList1;
            this.btndownloadresult.Location = new System.Drawing.Point(553, 509);
            this.btndownloadresult.Name = "btndownloadresult";
            this.btndownloadresult.Padding = new System.Windows.Forms.Padding(5);
            this.btndownloadresult.Size = new System.Drawing.Size(159, 45);
            this.btndownloadresult.TabIndex = 10;
            this.btndownloadresult.Text = "&Download Result";
            this.btndownloadresult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndownloadresult.UseVisualStyleBackColor = true;
            this.btndownloadresult.Click += new System.EventHandler(this.BtnExportCSV_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.lstCompleted);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblprocessCnt);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.progBar);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.lblUrlCount);
            this.groupBox1.Controls.Add(this.btndownloadresult);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.btnrunscraper);
            this.groupBox1.Controls.Add(this.btnClearUrlList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lstUrl);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.btnAddUrl);
            this.groupBox1.Controls.Add(this.BtnPaste);
            this.groupBox1.Controls.Add(this.BtnRemoveSelected);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1101, 658);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process Setup";
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStop.Location = new System.Drawing.Point(553, 460);
            this.btnStop.Name = "btnStop";
            this.btnStop.Padding = new System.Windows.Forms.Padding(5);
            this.btnStop.Size = new System.Drawing.Size(159, 43);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop Working";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lstCompleted
            // 
            this.lstCompleted.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCompleted.FormattingEnabled = true;
            this.lstCompleted.ItemHeight = 17;
            this.lstCompleted.Location = new System.Drawing.Point(718, 240);
            this.lstCompleted.Name = "lstCompleted";
            this.lstCompleted.Size = new System.Drawing.Size(369, 344);
            this.lstCompleted.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(718, 221);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 17);
            this.label11.TabIndex = 18;
            this.label11.Text = "Completed URL(s)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtuniqueurls);
            this.groupBox5.Controls.Add(this.txtduplicatesurls);
            this.groupBox5.Controls.Add(this.txtnumberofurls);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(393, 389);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(154, 195);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Url File Info";
            // 
            // txtuniqueurls
            // 
            this.txtuniqueurls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtuniqueurls.Location = new System.Drawing.Point(15, 149);
            this.txtuniqueurls.Name = "txtuniqueurls";
            this.txtuniqueurls.Size = new System.Drawing.Size(123, 23);
            this.txtuniqueurls.TabIndex = 19;
            this.txtuniqueurls.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DisableInputs);
            // 
            // txtduplicatesurls
            // 
            this.txtduplicatesurls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtduplicatesurls.Location = new System.Drawing.Point(15, 103);
            this.txtduplicatesurls.Name = "txtduplicatesurls";
            this.txtduplicatesurls.Size = new System.Drawing.Size(123, 23);
            this.txtduplicatesurls.TabIndex = 18;
            this.txtduplicatesurls.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DisableInputs);
            // 
            // txtnumberofurls
            // 
            this.txtnumberofurls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnumberofurls.Location = new System.Drawing.Point(15, 57);
            this.txtnumberofurls.Name = "txtnumberofurls";
            this.txtnumberofurls.Size = new System.Drawing.Size(123, 23);
            this.txtnumberofurls.TabIndex = 17;
            this.txtnumberofurls.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DisableInputs);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 17);
            this.label10.TabIndex = 16;
            this.label10.Text = "Duplicates :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Unique :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "No of URLs Imported :";
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(12, 595);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(1075, 18);
            this.progBar.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblCntProxies);
            this.groupBox3.Controls.Add(this.checkUserProxy);
            this.groupBox3.Controls.Add(this.advancedDataGridView_proxies);
            this.groupBox3.Controls.Add(this.btnClearProxies);
            this.groupBox3.Controls.Add(this.btnImportProxy);
            this.groupBox3.Location = new System.Drawing.Point(6, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1081, 180);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proxies";
            // 
            // lblCntProxies
            // 
            this.lblCntProxies.AutoSize = true;
            this.lblCntProxies.Location = new System.Drawing.Point(425, 27);
            this.lblCntProxies.Name = "lblCntProxies";
            this.lblCntProxies.Size = new System.Drawing.Size(0, 16);
            this.lblCntProxies.TabIndex = 25;
            // 
            // checkUserProxy
            // 
            this.checkUserProxy.AutoSize = true;
            this.checkUserProxy.Location = new System.Drawing.Point(326, 26);
            this.checkUserProxy.Name = "checkUserProxy";
            this.checkUserProxy.Size = new System.Drawing.Size(93, 20);
            this.checkUserProxy.TabIndex = 24;
            this.checkUserProxy.Text = "Use Proxies";
            this.checkUserProxy.UseVisualStyleBackColor = true;
            // 
            // advancedDataGridView_proxies
            // 
            this.advancedDataGridView_proxies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView_proxies.FilterAndSortEnabled = false;
            this.advancedDataGridView_proxies.Location = new System.Drawing.Point(9, 52);
            this.advancedDataGridView_proxies.MultiSelect = false;
            this.advancedDataGridView_proxies.Name = "advancedDataGridView_proxies";
            this.advancedDataGridView_proxies.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.advancedDataGridView_proxies.RowTemplate.Height = 23;
            this.advancedDataGridView_proxies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.advancedDataGridView_proxies.Size = new System.Drawing.Size(1066, 122);
            this.advancedDataGridView_proxies.TabIndex = 20;
            // 
            // btnClearProxies
            // 
            this.btnClearProxies.Location = new System.Drawing.Point(183, 22);
            this.btnClearProxies.Name = "btnClearProxies";
            this.btnClearProxies.Size = new System.Drawing.Size(120, 24);
            this.btnClearProxies.TabIndex = 23;
            this.btnClearProxies.Text = "Clear all proxies";
            this.btnClearProxies.UseVisualStyleBackColor = true;
            this.btnClearProxies.Click += new System.EventHandler(this.BtnClearProxies_Click);
            // 
            // btnImportProxy
            // 
            this.btnImportProxy.Location = new System.Drawing.Point(6, 22);
            this.btnImportProxy.Name = "btnImportProxy";
            this.btnImportProxy.Size = new System.Drawing.Size(176, 24);
            this.btnImportProxy.TabIndex = 22;
            this.btnImportProxy.Text = "Import proxies from txt file";
            this.btnImportProxy.UseVisualStyleBackColor = true;
            this.btnImportProxy.Click += new System.EventHandler(this.BtnImportProxy_Click);
            // 
            // btnClearUrlList
            // 
            this.btnClearUrlList.Location = new System.Drawing.Point(539, 332);
            this.btnClearUrlList.Name = "btnClearUrlList";
            this.btnClearUrlList.Size = new System.Drawing.Size(173, 32);
            this.btnClearUrlList.TabIndex = 15;
            this.btnClearUrlList.Text = "Clear all url";
            this.btnClearUrlList.UseVisualStyleBackColor = true;
            this.btnClearUrlList.Click += new System.EventHandler(this.BtnClearUrlList_Click);
            // 
            // BtnPaste
            // 
            this.BtnPaste.Location = new System.Drawing.Point(538, 277);
            this.BtnPaste.Name = "BtnPaste";
            this.BtnPaste.Size = new System.Drawing.Size(174, 50);
            this.BtnPaste.TabIndex = 16;
            this.BtnPaste.Text = "Paste Copied Urls";
            this.BtnPaste.UseVisualStyleBackColor = true;
            this.BtnPaste.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // BtnRemoveSelected
            // 
            this.BtnRemoveSelected.Location = new System.Drawing.Point(393, 332);
            this.BtnRemoveSelected.Name = "BtnRemoveSelected";
            this.BtnRemoveSelected.Size = new System.Drawing.Size(145, 32);
            this.BtnRemoveSelected.TabIndex = 14;
            this.BtnRemoveSelected.Text = "Remove selected url";
            this.BtnRemoveSelected.UseVisualStyleBackColor = true;
            this.BtnRemoveSelected.Click += new System.EventHandler(this.BtnRemoveSelected_Click);
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.label7);
            this.grpSettings.Controls.Add(this.txtThreadCount);
            this.grpSettings.Enabled = false;
            this.grpSettings.Location = new System.Drawing.Point(895, 675);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(315, 106);
            this.grpSettings.TabIndex = 17;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            this.grpSettings.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Threads";
            // 
            // txtThreadCount
            // 
            this.txtThreadCount.Location = new System.Drawing.Point(69, 20);
            this.txtThreadCount.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.txtThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtThreadCount.Name = "txtThreadCount";
            this.txtThreadCount.Size = new System.Drawing.Size(120, 21);
            this.txtThreadCount.TabIndex = 1;
            this.txtThreadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 675);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1112, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblstatus
            // 
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(131, 17);
            this.lblstatus.Text = "toolStripStatusLabel1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 697);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_proxies)).EndInit();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreadCount)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_proxies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnrunscraper;
        private System.Windows.Forms.Label lblprocessCnt;
        private System.Windows.Forms.Button btnAddUrl;
        private System.Windows.Forms.ListBox lstUrl;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblUrlCount;
        private System.Windows.Forms.Button btndownloadresult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearUrlList;
        private System.Windows.Forms.Button BtnRemoveSelected;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnImportProxy;
        private System.Windows.Forms.Button btnClearProxies;
        private System.Windows.Forms.Button BtnPaste;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtThreadCount;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtuniqueurls;
        private System.Windows.Forms.TextBox txtduplicatesurls;
        private System.Windows.Forms.TextBox txtnumberofurls;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblstatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListBox lstCompleted;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView_proxies;
        private System.Windows.Forms.BindingSource bindingSource_proxies;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox checkUserProxy;
        private System.Windows.Forms.Label lblCntProxies;
    }
}

