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
using System.Security.Permissions;
using System.Data.SqlClient;

namespace FileTest2
{
    public partial class FrmMain : Form
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("序号", Type.GetType("System.String"));
        DataColumn dc2 = new DataColumn("文件名", Type.GetType("System.String"));
        DataColumn dc3 = new DataColumn("文件路径", Type.GetType("System.String"));
        DataColumn dc4 = new DataColumn("上次修改时间", Type.GetType("System.String"));
        DataColumn dc5 = new DataColumn("是否重要", Type.GetType("System.Boolean"));
        DataColumn dc6 = new DataColumn("文件备注", Type.GetType("System.String"));
        DataTable dtChanged = null;
        public FrmMain()
        {
            InitializeComponent();
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);

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
            this.gvTable.Columns[5].ReadOnly = false;
            fsWatcher.Path = path;
            fsWatcher.EnableRaisingEvents = true;
        }
        /// <summary>
        /// 设置文件是否重要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1 && e.ColumnIndex == 4)
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
            if(rowIndex>-1)
            {
                DataTable dtt = (DataTable)this.gvTable.DataSource;
                string path = dtt.Rows[rowIndex]["文件路径"].ToString();
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                psi.Arguments = "/e,/select," + path;
                System.Diagnostics.Process.Start(psi);
            }
        }
        DataGridViewCell cell;
        int rowIndex = -1;
        private void gvTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // DataGridView.HitTestInfo gp = ((DataGridView)sender).HitTest(e.X, e.Y);
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    rowIndex = e.RowIndex;
                    cell = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
                    this.gvTable.Rows[e.RowIndex].Selected = true;
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
                rowIndex = e.RowIndex;
                this.gvTable.Rows[e.RowIndex].Selected = true;
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
            if (rowIndex > -1)
            {
                DataTable dtt = (DataTable)this.gvTable.DataSource;
                string path = dtt.Rows[rowIndex]["文件路径"].ToString();
                System.Diagnostics.Process.Start(path);
            }
        }
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public void WatchStart(string path, string filter)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
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
                dr["是否重要"] = "false";
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

        private void fsWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            DataTable newDt = dt.Clone(); 
            int k=0;
            //查询有无数据，有则删掉
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string filename=dt.Rows[i]["文件名"].ToString();
                if (filename.Substring(0, filename.LastIndexOf(".") + 1)!= e.Name.Substring(0, e.Name.LastIndexOf(".")+1))//如果截取的文件名不相等
                {
                    DataRow drNew = newDt.NewRow();
                    drNew["序号"] = k + 1;
                    drNew["文件名"] = filename;
                    drNew["文件路径"] = dt.Rows[i]["文件路径"].ToString();
                    drNew["上次修改时间"] = dt.Rows[i]["上次修改时间"].ToString();
                    drNew["是否重要"] =(bool) dt.Rows[i]["是否重要"];
                    drNew["文件备注"] = dt.Rows[i]["文件备注"];
                    k++;
                    newDt.Rows.Add(drNew);
                 }
            }
            this.gvTable.DataSource = newDt;
        }

        /// <summary>
        /// 文件搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            string Text = this.txtSearch.Text;
            DataRow[] dr = dt.Select("文件路径 like '%" + Text + "%'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 文本框文件搜素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void gvTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dt = (DataTable)this.gvTable.DataSource;
        }

        /// <summary>
        /// 分类搜索（所有）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsWatcher_Created(object sender, FileSystemEventArgs e)
        {
            DataTable newDt = dt.Copy();
            DataRow newRow = newDt.NewRow();
            newRow["序号"] = newDt.Rows.Count + 1;
            string filepath = RecentFileHelper.GetShortcutTargetFile(e.FullPath);
            FileInfo fi=new FileInfo (filepath);
            newRow["文件名"] = fi.Name;
            newRow["文件路径"] =filepath;
            newRow["上次修改时间"] = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            newRow["是否重要"] = false;
            newRow["文件备注"] = null;
            newDt.Rows.Add(newRow);
            this.gvTable.DataSource = newDt;
            dt =(DataTable) this.gvTable.DataSource;
        }
        private void alltsmItem_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        /// <summary>
        /// 分类搜索（音频）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void audiotsmItem_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select("文件路径 like '%mp3' or 文件路径 like '%wma' or 文件路径 like '%wav' or 文件路径 like '%ogg'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 分类搜索（压缩文件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ziptsmItem_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select("文件路径 like '%rar' or 文件路径 like '%zip' or 文件路径 like '%7z' or 文件路径 like '%gz' or 文件路径 like '%bz' or 文件路径 like '%ace' or 文件路径 like '%uha' or 文件路径 like '%uda' or 文件路径 like '%zpaq'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 分类搜索（文档）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txttsmItem_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select("文件路径 like '%txt' or 文件路径 like '%doc' or 文件路径 like '%docx' or 文件路径 like '%xls' or 文件路径 like '%xlsx' or 文件路径 like '%ppt' or 文件路径 like '%pptx' or 文件路径 like '%wps' or 文件路径 like '%rtf' or 文件路径 like '%html' or 文件路径 like '%pdf' or 文件路径 like '%hlp'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 分类搜索（可执行文件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exetsmItem_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select("文件路径 like '%exe' or 文件路径 like '%dll' or 文件路径 like '%com' or 文件路径 like '%bat'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 分类搜索（图片）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imagetsmItem_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select("文件路径 like '%bmp' or 文件路径 like '%jpg' or 文件路径 like '%jpeg' or 文件路径 like '%png' or 文件路径 like '%gif' or 文件路径 like '%psd'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 分类搜索（视频）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videotsmItem_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select("文件路径 like '%avi' or 文件路径 like '%mpg' or 文件路径 like '%mpeg' or 文件路径 like '%rm' or 文件路径 like '%rmvb' or 文件路径 like '%dat' or 文件路径 like '%wmv' or 文件路径 like '%mov'");
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            this.gvTable.DataSource = newdt;
        }

        /// <summary>
        /// 一键整理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmItemArrange_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请指定一键整理文件存放路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;//整理文件夹路径
                //文件路径
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //文件类型的判断
                    string fileExtension = Path.GetExtension(dt.Rows[i]["文件路径"].ToString());
                    string wfilePath = null;
                    //文本类
                    if (fileExtension.ToLower() == ".xlsx" || fileExtension.ToLower() == ".xls" || fileExtension.ToLower() == ".doc"  || fileExtension.ToLower() == ".txt" || fileExtension.ToLower() == ".docx"
                        || fileExtension.ToLower() == ".ppt" || fileExtension.ToLower() == ".pptx" || fileExtension.ToLower() == ".wps" || fileExtension.ToLower() == ".rtf" || fileExtension.ToLower() == ".html"
                        || fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".hlp")
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\文本类";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }
                    //图片类
                    else if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".psd")
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\图片";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }//视频
                    else if (fileExtension.ToLower() == ".avi" || fileExtension.ToLower() == ".mpg" || fileExtension.ToLower() == ".mpeg" || fileExtension.ToLower() == ".rm" || fileExtension.ToLower() == ".rmvb" 
                        || fileExtension.ToLower() == ".dat" || fileExtension.ToLower() == ".wmv" || fileExtension.ToLower() == ".mov" )
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\视频";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }//音频
                    else if (fileExtension.ToLower() == ".wma" || fileExtension.ToLower() == ".mp3" || fileExtension.ToLower() == ".wav" || fileExtension.ToLower() == ".ogg")
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\音频类";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }//可执行文件
                    else if (fileExtension.ToLower() == ".exe" || fileExtension.ToLower() == ".bat" || fileExtension.ToLower() == ".dll" || fileExtension.ToLower() == ".com")
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\可执行文件";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }//压缩文件
                    else if (fileExtension.ToLower() == ".rar" || fileExtension.ToLower() == ".zip" || fileExtension.ToLower() == ".7z" || fileExtension.ToLower() == ".gz" || fileExtension.ToLower() == ".bz"
                        || fileExtension.ToLower() == ".ace" || fileExtension.ToLower() == ".zpaq" || fileExtension.ToLower() == ".uda" || fileExtension.ToLower() == ".uha")
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\压缩文件";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }
                    else
                    {
                        //判断文本文件是否存在
                        wfilePath = foldPath + @"\其他";
                        if (System.IO.Directory.Exists(wfilePath) == false)
                        {
                            //不存在文件
                            Directory.CreateDirectory(wfilePath);
                        }
                    }
                    //文件存在时复制文件
                    string NewFilePath = wfilePath + @"\" + Path.GetFileName(dt.Rows[i]["文件路径"].ToString());
                    string OldFilePath = Path.GetFullPath(dt.Rows[i]["文件路径"].ToString());
                    File.Copy(OldFilePath, NewFilePath, true);//允许覆盖目的地的同名文件
                }
                MessageBox.Show("文件整理成功！");

            }
        }

    }
}
