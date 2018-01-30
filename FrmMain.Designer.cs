namespace FileTest2
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.gvTable = new System.Windows.Forms.DataGridView();
            this.cmsOpenFileAddress = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.fsWatcher = new System.IO.FileSystemWatcher();
            this.mns = new System.Windows.Forms.MenuStrip();
            this.tsmItemSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.alltsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audiotsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ziptsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txttsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exetsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagetsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videotsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemArrange = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).BeginInit();
            this.cmsOpenFileAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).BeginInit();
            this.mns.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExit,
            this.tsmiShowWindows});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(124, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiShowWindows
            // 
            this.tsmiShowWindows.Name = "tsmiShowWindows";
            this.tsmiShowWindows.Size = new System.Drawing.Size(124, 22);
            this.tsmiShowWindows.Text = "显示窗口";
            this.tsmiShowWindows.Click += new System.EventHandler(this.tsmiShowWindows_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "一键文件";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // gvTable
            // 
            this.gvTable.AllowUserToAddRows = false;
            this.gvTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTable.Location = new System.Drawing.Point(0, 56);
            this.gvTable.Name = "gvTable";
            this.gvTable.RowHeadersVisible = false;
            this.gvTable.RowTemplate.Height = 23;
            this.gvTable.Size = new System.Drawing.Size(746, 317);
            this.gvTable.TabIndex = 3;
            this.gvTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTable_CellContentClick);
            this.gvTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTable_CellEndEdit);
            this.gvTable.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvTable_CellMouseDown);
            // 
            // cmsOpenFileAddress
            // 
            this.cmsOpenFileAddress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenAddress,
            this.tsmiOpenFile});
            this.cmsOpenFileAddress.Name = "cmsOpenFileAddress";
            this.cmsOpenFileAddress.Size = new System.Drawing.Size(149, 48);
            // 
            // tsmiOpenAddress
            // 
            this.tsmiOpenAddress.Name = "tsmiOpenAddress";
            this.tsmiOpenAddress.Size = new System.Drawing.Size(148, 22);
            this.tsmiOpenAddress.Text = "打开文件位置";
            this.tsmiOpenAddress.Click += new System.EventHandler(this.tsmiOpenAddress_Click);
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(148, 22);
            this.tsmiOpenFile.Text = "打开文件";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(4, 29);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(670, 21);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(680, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // fsWatcher
            // 
            this.fsWatcher.EnableRaisingEvents = true;
            this.fsWatcher.SynchronizingObject = this;
            this.fsWatcher.Created += new System.IO.FileSystemEventHandler(this.fsWatcher_Created);
            this.fsWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fsWatcher_Deleted);
            // 
            // mns
            // 
            this.mns.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.mns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItemSearch,
            this.tsmItemArrange});
            this.mns.Location = new System.Drawing.Point(0, 0);
            this.mns.Name = "mns";
            this.mns.Size = new System.Drawing.Size(746, 25);
            this.mns.TabIndex = 7;
            this.mns.Text = "menuStrip1";
            // 
            // tsmItemSearch
            // 
            this.tsmItemSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alltsmItem,
            this.audiotsmItem,
            this.ziptsmItem,
            this.txttsmItem,
            this.exetsmItem,
            this.imagetsmItem,
            this.videotsmItem});
            this.tsmItemSearch.Name = "tsmItemSearch";
            this.tsmItemSearch.Size = new System.Drawing.Size(68, 21);
            this.tsmItemSearch.Text = "分类搜索";
            // 
            // alltsmItem
            // 
            this.alltsmItem.Name = "alltsmItem";
            this.alltsmItem.Size = new System.Drawing.Size(136, 22);
            this.alltsmItem.Text = "所有";
            this.alltsmItem.Click += new System.EventHandler(this.alltsmItem_Click);
            // 
            // audiotsmItem
            // 
            this.audiotsmItem.Name = "audiotsmItem";
            this.audiotsmItem.Size = new System.Drawing.Size(136, 22);
            this.audiotsmItem.Text = "音频";
            this.audiotsmItem.Click += new System.EventHandler(this.audiotsmItem_Click);
            // 
            // ziptsmItem
            // 
            this.ziptsmItem.Name = "ziptsmItem";
            this.ziptsmItem.Size = new System.Drawing.Size(136, 22);
            this.ziptsmItem.Text = "压缩文件";
            this.ziptsmItem.Click += new System.EventHandler(this.ziptsmItem_Click);
            // 
            // txttsmItem
            // 
            this.txttsmItem.Name = "txttsmItem";
            this.txttsmItem.Size = new System.Drawing.Size(136, 22);
            this.txttsmItem.Text = "文档";
            this.txttsmItem.Click += new System.EventHandler(this.txttsmItem_Click);
            // 
            // exetsmItem
            // 
            this.exetsmItem.Name = "exetsmItem";
            this.exetsmItem.Size = new System.Drawing.Size(136, 22);
            this.exetsmItem.Text = "可执行文件";
            this.exetsmItem.Click += new System.EventHandler(this.exetsmItem_Click);
            // 
            // imagetsmItem
            // 
            this.imagetsmItem.Name = "imagetsmItem";
            this.imagetsmItem.Size = new System.Drawing.Size(136, 22);
            this.imagetsmItem.Text = "图片";
            this.imagetsmItem.Click += new System.EventHandler(this.imagetsmItem_Click);
            // 
            // videotsmItem
            // 
            this.videotsmItem.Name = "videotsmItem";
            this.videotsmItem.Size = new System.Drawing.Size(136, 22);
            this.videotsmItem.Text = "视频";
            this.videotsmItem.Click += new System.EventHandler(this.videotsmItem_Click);
            // 
            // tsmItemArrange
            // 
            this.tsmItemArrange.Name = "tsmItemArrange";
            this.tsmItemArrange.Size = new System.Drawing.Size(68, 21);
            this.tsmItemArrange.Text = "一键整理";
            this.tsmItemArrange.Click += new System.EventHandler(this.tsmItemArrange_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 372);
            this.Controls.Add(this.mns);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.gvTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mns;
            this.Name = "FrmMain";
            this.Text = "一键文件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).EndInit();
            this.cmsOpenFileAddress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).EndInit();
            this.mns.ResumeLayout(false);
            this.mns.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowWindows;
        private System.Windows.Forms.DataGridView gvTable;
        private System.Windows.Forms.ContextMenuStrip cmsOpenFileAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.IO.FileSystemWatcher fsWatcher;
        private System.Windows.Forms.MenuStrip mns;
        private System.Windows.Forms.ToolStripMenuItem tsmItemSearch;
        private System.Windows.Forms.ToolStripMenuItem alltsmItem;
        private System.Windows.Forms.ToolStripMenuItem audiotsmItem;
        private System.Windows.Forms.ToolStripMenuItem ziptsmItem;
        private System.Windows.Forms.ToolStripMenuItem txttsmItem;
        private System.Windows.Forms.ToolStripMenuItem exetsmItem;
        private System.Windows.Forms.ToolStripMenuItem imagetsmItem;
        private System.Windows.Forms.ToolStripMenuItem videotsmItem;
        private System.Windows.Forms.ToolStripMenuItem tsmItemArrange;
    }
}

