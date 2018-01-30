using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileTest2
{
    public static class RecentFileHelper
    {
        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            var type = Type.GetTypeFromProgID("WScript.Shell");
            object instance = Activator.CreateInstance(type);
            var result = type.InvokeMember("CreateShortCut", BindingFlags.InvokeMethod, null, instance, new object[] { shortcutFilename });
            var targetFile = result.GetType().InvokeMember("TargetPath", BindingFlags.GetProperty, null, result, null) as string;
            return targetFile;
        }

        public static IEnumerable<string> GetRecentlyFiles()
        {
            var recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            var listFiles = from file in Directory.EnumerateFiles(recentFolder)
                            where Path.GetExtension(file) == ".lnk"
                            select GetShortcutTargetFile(file);
            var result = listFiles.Where(p => File.Exists(p));
            var sss = from file in Directory.EnumerateFiles(recentFolder) select file;
            var rrr = sss.ToList()[0];
            var r = GetShortcutTargetFile(rrr);
            var ss = GetShortcutTargetFile(@"C:\Users\jdz\AppData\Roaming\Microsoft\Windows\Recent\FileDataBase.lnk");
            return result;
        }
        /// <summary>
        /// 创建excel并返回dataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable CreateExcel(string filePath, DataTable dt)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            int k = 0;
            FileStream fs = null;

            // 2007版本  
            if (filePath.IndexOf(".xlsx") > 0)
                workbook = new XSSFWorkbook();
            if (workbook != null)
            {
                sheet = workbook.CreateSheet("sheet0");
                //设置列头  
                row = sheet.CreateRow(0);//excel第一行设为列头  
                row.CreateCell(0).SetCellValue("序号");
                row.CreateCell(1).SetCellValue("文件名");
                row.CreateCell(2).SetCellValue("文件路径");
                row.CreateCell(3).SetCellValue("上次修改时间");
                row.CreateCell(4).SetCellValue("是否重要");
                row.CreateCell(5).SetCellValue("文件备注");
                foreach (var file in RecentFileHelper.GetRecentlyFiles())
                {
                    if (File.Exists(file))
                    {
                        k++;
                        FileInfo fi = new FileInfo(file);
                        row = sheet.CreateRow(k);
                        DataRow dRow = dt.NewRow();
                        dRow["序号"] = k;
                        dRow["文件名"] = fi.Name;
                        dRow["文件路径"] = file;
                        dRow["上次修改时间"] = fi.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss");
                        dRow["是否重要"] = false;
                        dRow["文件备注"] = null;
                        dt.Rows.Add(dRow);
                        row.CreateCell(0).SetCellValue(k);
                        row.CreateCell(1).SetCellValue(fi.Name);
                        row.CreateCell(2).SetCellValue(file);
                        row.CreateCell(3).SetCellValue(fi.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        row.CreateCell(4).SetCellValue(false);
                        row.CreateCell(5).SetCellValue("");

                    }
                }
            }
            using (fs = File.OpenWrite(filePath))
            {
                workbook.Write(fs);//向打开的这个xls文件中写入数据  
            }
            return dt;
        }
        /// <summary>
        /// 读取excel，检核并写入 返回DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable ReadWriteExcel(string filePath, DataTable dt)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            FileStream fs = null;

            using (fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = new XSSFWorkbook(fs);
            }
            sheet = workbook.GetSheetAt(0);
            int rowCount = sheet.LastRowNum;
            int tempCount = rowCount;
            foreach (var file in RecentFileHelper.GetRecentlyFiles())
            {
                bool flag = false;
                for (int i = 1; i < rowCount + 1; i++)
                {
                    if (File.Exists(file) && sheet.GetRow(i).Cells[2].ToString() == file)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    if (File.Exists(file) && file != filePath)
                    {
                        tempCount++;
                        FileInfo fi = new FileInfo(file);
                        row = sheet.CreateRow(tempCount);
                        row.CreateCell(0).SetCellValue(sheet.LastRowNum);
                        row.CreateCell(1).SetCellValue(fi.Name);
                        row.CreateCell(2).SetCellValue(file);
                        row.CreateCell(3).SetCellValue(fi.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        row.CreateCell(4).SetCellValue(false);
                        row.CreateCell(5).SetCellValue("");

                    }
                }
            }
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = i;
                dr["文件名"] = sheet.GetRow(i).Cells[1].ToString();
                dr["文件路径"] = sheet.GetRow(i).Cells[2].ToString();
                FileInfo fi = new FileInfo(sheet.GetRow(i).Cells[2].ToString());
                dr["上次修改时间"] = sheet.GetRow(i).Cells[3].ToString();
                if(sheet.GetRow(i).Cells[4]==null)
                {
                  dr["是否重要"] = false;
                }else
                {
                    if (sheet.GetRow(i).Cells[4].ToString().ToLower() == "false")
                    {
                        dr["是否重要"] = false;
                    }
                    else
                    {
                        dr["是否重要"] = true;
                    }
                }
                dr["文件备注"] = string.IsNullOrEmpty(sheet.GetRow(i).Cells[5].ToString()) ? null : sheet.GetRow(i).Cells[5].ToString();

                dt.Rows.Add(dr);

            }
            DataView dv = dt.DefaultView;//文件备注 desc,
            dv.Sort = "是否重要 desc,文件备注 asc";
            using (fs = File.OpenWrite(filePath))
            {
                workbook.Write(fs);
            }
            return dv.ToTable();
        }
        /// <summary>
        /// 重新写入excel
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="dt">写入数据源</param>
        public static void ReWriteExcel(string filePath, DataTable dt)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            int k = 0;
            FileStream fs = null;
            // 2007版本  
            if (filePath.IndexOf(".xlsx") > 0)
                workbook = new XSSFWorkbook();
            if (workbook != null)
            {
                sheet = workbook.CreateSheet("sheet0");
                //设置列头  
                row = sheet.CreateRow(0);//excel第一行设为列头  
                row.CreateCell(0).SetCellValue("序号");
                row.CreateCell(1).SetCellValue("文件名");
                row.CreateCell(2).SetCellValue("文件路径");
                row.CreateCell(3).SetCellValue("上次修改时间");
                row.CreateCell(4).SetCellValue("是否重要");
                row.CreateCell(5).SetCellValue("文件备注");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (File.Exists(dt.Rows[i][2].ToString()))
                    {
                        row = sheet.CreateRow(i+1);
                        row.CreateCell(0).SetCellValue(dt.Rows[i][0].ToString());
                        row.CreateCell(1).SetCellValue(dt.Rows[i][1].ToString());
                        row.CreateCell(2).SetCellValue(dt.Rows[i][2].ToString());
                        row.CreateCell(3).SetCellValue(dt.Rows[i][3].ToString());
                        row.CreateCell(4).SetCellValue(dt.Rows[i][4].ToString());

                        row.CreateCell(5).SetCellValue(dt.Rows[i][5] == null ? "" : dt.Rows[i][5].ToString());

                    }
                }
              
            }
            using (fs = File.OpenWrite(filePath))
            {
                workbook.Write(fs);//向打开的这个xls文件中写入数据  
            }
        }


    }
}
