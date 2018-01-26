using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace FileTest2
{
    public partial class FrmMain : Form
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("序号", Type.GetType("System.String"));
        DataColumn dc2 = new DataColumn("文件路径", Type.GetType("System.String"));
        DataColumn dc3 = new DataColumn("上次修改时间", Type.GetType("System.String"));
        DataColumn dc4 = new DataColumn("是否重要", Type.GetType("System.Boolean"));
        DataTable dtChanged = null;
        public FrmMain()
        {
            InitializeComponent();
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
        }
        private void btnGetProcess_Click(object sender, EventArgs e)
        {

            //foreach (var file in RecentFileHelper.GetRecentlyFiles())
            //{
            //    if (System.IO.File.Exists(file))
            //    {
            //        number++;
            //        var item = new ListViewItem();
            //        item.Text = number.ToString();
            //        FileInfo fi = new FileInfo(file);
            //        item.SubItems.Add(file);
            //        item.SubItems.Add(fi.Length / 1024 + "KB");
            //        item.SubItems.Add(fi.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));
            //        listView1.Items.Add(item);
            //        //写入excel
            //    }
            //}
            //var recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            //fileSystemWatcher1.Path = recentFolder;
            //fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Created);
        }
        void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            // listView1.Items.Add(RecentFileHelper.GetShortcutTargetFile(e.FullPath));
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)    //最小化到系统托盘
            {
                this.notifyIcon1.Visible = true;    //显示托盘图标
                this.Hide();    //隐藏窗口
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //注意判断关闭事件Reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;    //取消"关闭窗口"事件
                this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
                notifyIcon1.Visible = true;
                this.Hide();
                return;
            }

        }
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Focus();
        }
        //退出菜单
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("真的要退出程序吗？", "退出程序", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string filePath = Application.StartupPath + @"\FileDataBase.xlsx";
                //删除原excel 将dt 重新写入Excel
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    RecentFileHelper.ReWriteExcel(filePath, dt);

                }
                Application.Exit();
            }

        }

        private void tsmiShowWindows_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            string filePath = Application.StartupPath + @"\FileDataBase.xlsx";
            if (!File.Exists(filePath))
            {
                //创建FileDataBase.xlsx文件
                dt = RecentFileHelper.CreateExcel(filePath, dt);
                dtChanged = dt.Copy();
            }
            else
            {
                //读取FileDataBase.xlsx文件，并检核，如果有新的文件，写入excel
                dt = RecentFileHelper.ReadWriteExcel(filePath, dt);
                dtChanged = dt.Copy();

            }
            this.gvTable.DataSource = dt;
            WatchStart(filePath,"*.*");
            //  this.gvTable.Columns[1].ContextMenuStrip = this.cmsOpenFileAddress;//设定文件路径单元格的右键菜单
        }
        /// <summary>
        /// 设置文件是否重要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1 && e.ColumnIndex == 3)
            {
                if (this.gvTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "False")
                {
                    this.gvTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                    dt.Rows[e.RowIndex][e.ColumnIndex] = true;
                    this.gvTable.DataSource = dt;
                }
                else
                {
                    this.gvTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    dt.Rows[e.RowIndex][e.ColumnIndex] = false;
                    this.gvTable.DataSource = dt;

                }

            }

        }
        /// <summary>
        /// 打开文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenAddress_Click(object sender, EventArgs e)
        {
            string path = cell.Value.ToString();
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + path;
            System.Diagnostics.Process.Start(psi);
        }
        DataGridViewCell cell;

        private void gvTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // DataGridView.HitTestInfo gp = ((DataGridView)sender).HitTest(e.X, e.Y);
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    cell = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
                    this.cmsOpenFileAddress.Show(e.X, e.Y);
                }
            }
        }

        private void gvTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.gvTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected == false)
                {
                    this.gvTable.ClearSelection();
                    this.gvTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                }
                cell = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
                this.cmsOpenFileAddress.Show(MousePosition.X, MousePosition.Y);

            }
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            string path = cell.Value.ToString();
            System.Diagnostics.Process.Start(path);

        }
        public void WatchStart(string path, string filter)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Created += new FileSystemEventHandler(OnProcess);
            watcher.EnableRaisingEvents = true;
        }
        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = dt.Rows.Count + 1;
                string tmpPath = RecentFileHelper.GetShortcutTargetFile(e.FullPath);
                dr["文件路径"] = tmpPath;
                FileInfo fi = new FileInfo(tmpPath);
                dr["上次修改时间"] = fi.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss");
                dr["是否重要"] = "False";
                dt.Rows.Add(dr);
                this.gvTable.DataSource = dt;
            }
            if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                string tmpPath = RecentFileHelper.GetShortcutTargetFile(e.FullPath);
                DataRow[] findRow = dt.Select("文件路径='" + tmpPath + "'");
                foreach (DataRow row in findRow)
                {
                    dt.Rows.Remove(row);
                }
                DataTable ds = dt;
            }
        }

    }
}
