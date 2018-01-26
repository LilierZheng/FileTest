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
            this.btnGetProcess = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.gvTable = new System.Windows.Forms.DataGridView();
            this.cmsOpenFileAddress = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).BeginInit();
            this.cmsOpenFileAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetProcess
            // 
            this.btnGetProcess.Location = new System.Drawing.Point(49, 12);
            this.btnGetProcess.Name = "btnGetProcess";
            this.btnGetProcess.Size = new System.Drawing.Size(75, 23);
            this.btnGetProcess.TabIndex = 0;
            this.btnGetProcess.Text = "一键整理";
            this.btnGetProcess.UseVisualStyleBackColor = true;
            this.btnGetProcess.Click += new System.EventHandler(this.btnGetProcess_Click);
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
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // gvTable
            // 
            this.gvTable.AllowUserToAddRows = false;
            this.gvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gvTable.Location = new System.Drawing.Point(0, 78);
            this.gvTable.Name = "gvTable";
            this.gvTable.RowHeadersVisible = false;
            this.gvTable.RowTemplate.Height = 23;
            this.gvTable.Size = new System.Drawing.Size(705, 310);
            this.gvTable.TabIndex = 3;
            this.gvTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTable_CellContentClick);
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
            this.tsmiOpenAddress.Size = new System.Drawing.Size(152, 22);
            this.tsmiOpenAddress.Text = "打开文件位置";
            this.tsmiOpenAddress.Click += new System.EventHandler(this.tsmiOpenAddress_Click);
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(152, 22);
            this.tsmiOpenFile.Text = "打开文件";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 388);
            this.Controls.Add(this.gvTable);
            this.Controls.Add(this.btnGetProcess);
            this.Name = "FrmMain";
            this.Text = "一键文件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).EndInit();
            this.cmsOpenFileAddress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetProcess;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowWindows;
        private System.Windows.Forms.DataGridView gvTable;
        private System.Windows.Forms.ContextMenuStrip cmsOpenFileAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.Timer timer1;
    }
}

