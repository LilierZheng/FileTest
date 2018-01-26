using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FileTest
{
    public class MFTScanner
    {
        private static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private const uint GENERIC_READ = 0x80000000;
        private const int FILE_SHARE_READ = 0x1;
        private const int FILE_SHARE_WRITE = 0x2;
        private const int OPEN_EXISTING = 3;
        private const int FILE_READ_ATTRIBUTES = 0x80;
        private const int FILE_NAME_IINFORMATION = 9;
        private const int FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;
        private const int FILE_OPEN_FOR_BACKUP_INTENT = 0x4000;
        private const int FILE_OPEN_BY_FILE_ID = 0x2000;
        private const int FILE_OPEN = 0x1;
        private const int OBJ_CASE_INSENSITIVE = 0x40;
        private const int FSCTL_ENUM_USN_DATA = 0x900b3;

        [StructLayout(LayoutKind.Sequential)]
        private struct MFT_ENUM_DATA
        {
            public long StartFileReferenceNumber;
            public long LowUsn;
            public long HighUsn;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct USN_RECORD
        {
            public int RecordLength;
            public short MajorVersion;
            public short MinorVersion;
            public long FileReferenceNumber;
            public long ParentFileReferenceNumber;
            public long Usn;
            public long TimeStamp;
            public int Reason;
            public int SourceInfo;
            public int SecurityId;
            public FileAttributes FileAttributes;
            public short FileNameLength;
            public short FileNameOffset;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IO_STATUS_BLOCK
        {
            public int Status;
            public int Information;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct UNICODE_STRING
        {
            public short Length;
            public short MaximumLength;
            public IntPtr Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OBJECT_ATTRIBUTES
        {
            public int Length;
            public IntPtr RootDirectory;
            public IntPtr ObjectName;
            public int Attributes;
            public int SecurityDescriptor;
            public int SecurityQualityOfService;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
        #region Process_Data
        [DllImport("KERNEL32.DLL ")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL ")]
        public static extern int CloseHandle(IntPtr handle);
        [DllImport("KERNEL32.DLL ")]
        public static extern int Process32First(IntPtr handle, ref   ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL ")]
        public static extern int Process32Next(IntPtr handle, ref   ProcessEntry32 pe);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, string lParam);
        #endregion
        #region Process_Data
        //public IntPtr GetHandleByProcessName(string ProcessName)
        //{
        //    List<ProcessEntry32> list = new List<ProcessEntry32>();
        //    IntPtr handle = CreateToolhelp32Snapshot(0x2, 0);
        //    IntPtr hh = IntPtr.Zero;
        //    if ((int)handle > 0)
        //    {
        //        ProcessEntry32 pe32 = new ProcessEntry32();
        //        pe32.dwSize = (uint)Marshal.SizeOf(pe32);
        //        int bMore = Process32First(handle, ref pe32);
        //        while (bMore == 1)
        //        {
        //            IntPtr temp = Marshal.AllocHGlobal((int)pe32.dwSize);
        //            Marshal.StructureToPtr(pe32, temp, true);
        //            ProcessEntry32 pe = (ProcessEntry32)Marshal.PtrToStructure(temp, typeof(ProcessEntry32));
        //            Marshal.FreeHGlobal(temp);
        //            list.Add(pe);
        //            if (pe.szExeFile == ProcessName)
        //            {
        //                bMore = 2;
        //                hh = GetCurrentWindowHandle(pe.th32ProcessID);
        //                break;
        //            }
        //            bMore = Process32Next(handle, ref pe32);
        //        }
        //    }
        //    return hh;
        //}
        //public static IntPtr GetCurrentWindowHandle(uint proid)
        //{
        //    IntPtr ptrWnd = IntPtr.Zero;
        //    uint uiPid = proid;
        //    object objWnd = processWnd[uiPid];
        //    if (objWnd != null)
        //    {
        //        ptrWnd = (IntPtr)objWnd;
        //        if (ptrWnd != IntPtr.Zero && IsWindow(ptrWnd))  // 从缓存中获取句柄
        //        {
        //            return ptrWnd;
        //        }
        //        else
        //        {
        //            ptrWnd = IntPtr.Zero;
        //        }
        //    }
        //    bool bResult = EnumWindows(new WNDENUMPROC(EnumWindowsProc), uiPid);
        //    // 枚举窗体返回 false 而且没有错误号时表明获取成功
        //    if (!bResult && Marshal.GetLastWin32Error() == 0)
        //    {
        //        objWnd = processWnd[uiPid];
        //        if (objWnd != null)
        //        {
        //            ptrWnd = (IntPtr)objWnd;
        //        }
        //    }
        //    return ptrWnd;
        //}


        //private static bool EnumWindowsProc(IntPtr hwnd, uint lParam)
        //{
        //    uint uiPid = 0;
        //    if (GetParent(hwnd) == IntPtr.Zero)
        //    {
        //        GetWindowThreadProcessId(hwnd, ref uiPid);
        //        if (uiPid == lParam)    // 找到进程相应的主窗体句柄
        //        {
        //            processWnd.Add(uiPid, hwnd);   // 把句柄缓存起来
        //            SetLastError(0);    // 设置无错误
        //            return false;   // 返回 false 以终止枚举窗体
        //        }
        //    }
        //    return true;
        //}
        #endregion
     
    }
}
