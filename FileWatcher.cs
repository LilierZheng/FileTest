using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTest2
{
    ///
    /// 文件监控类，用于监控指定目录下文件以及文件夹的变化
    ///
    public class FileWatcher
    {
        //private FileSystemWatcher _watcher = null;
        //private string _path = string.Empty;
        //private string _filter = string.Empty;
        //private bool _isWatch = false;
        //private CustomQueue _queue = null;

        /////
        ///// 监控是否正在运行
        /////
        //public bool IsWatch
        //{
        //    get
        //    {
        //        return _isWatch;
        //    }
        //}

        /////
        ///// 文件变更信息队列
        /////
        //public CustomQueue FileChangeQueue
        //{
        //    get
        //    {
        //        return _queue;
        //    }
        //}

        /////
        ///// 初始化FileWatcher类
        /////
        ///// 监控路径
        //public FileWatcher(string path)
        //{
        //    _path = path;
        //    _queue = new CustomQueue();
        //}
        /////
        ///// 初始化FileWatcher类，并指定是否持久化文件变更消息
        /////
        ///// 监控路径
        ///// 是否持久化变更消息
        ///// 持久化保存路径
        //public FileWatcher(string path, bool isPersistence, string persistenceFilePath)
        //{
        //    _path = path;
        //    _queue = new CustomQueue(isPersistence, persistenceFilePath);
        //}

        /////
        ///// 初始化FileWatcher类，并指定是否监控指定类型文件
        /////
        ///// 监控路径
        ///// 指定类型文件，格式如:*.txt,*.doc,*.rar
        //public FileWatcher(string path, string filter)
        //{
        //    _path = path;
        //    _filter = filter;
        //    _queue = new CustomQueue();
        //}

        /////
        ///// 初始化FileWatcher类，并指定是否监控指定类型文件，是否持久化文件变更消息
        /////
        ///// 监控路径
        ///// 指定类型文件，格式如:*.txt,*.doc,*.rar
        ///// 是否持久化变更消息
        ///// 持久化保存路径
        //public FileWatcher(string path, string filter, bool isPersistence, string persistenceFilePath)
        //{
        //    _path = path;
        //    _filter = filter;
        //    _queue = new CustomQueue(isPersistence, persistenceFilePath);
        //}

        /////
        ///// 打开文件监听器
        /////
        //public void Open()
        //{
        //    if (!Directory.Exists(_path))
        //    {
        //        Directory.CreateDirectory(_path);
        //    }

        //    if (string.IsNullOrEmpty(_filter))
        //    {
        //        _watcher = new FileSystemWatcher(_path);
        //    }
        //    else
        //    {
        //        _watcher = new FileSystemWatcher(_path, _filter);
        //    }
        //    //注册监听事件
        //    _watcher.Created += new FileSystemEventHandler(OnProcess);
        //    _watcher.Changed += new FileSystemEventHandler(OnProcess);
        //    _watcher.Deleted += new FileSystemEventHandler(OnProcess);
        //    _watcher.Renamed += new RenamedEventHandler(OnFileRenamed);
        //    _watcher.IncludeSubdirectories = true;
        //    _watcher.EnableRaisingEvents = true;
        //    _isWatch = true;
        //}

        /////
        ///// 关闭监听器
        /////
        //public void Close()
        //{
        //    _isWatch = false;
        //    _watcher.Created -= new FileSystemEventHandler(OnProcess);
        //    _watcher.Changed -= new FileSystemEventHandler(OnProcess);
        //    _watcher.Deleted -= new FileSystemEventHandler(OnProcess);
        //    _watcher.Renamed -= new RenamedEventHandler(OnFileRenamed);
        //    _watcher.EnableRaisingEvents = false;
        //    _watcher = null;
        //}

        /////
        ///// 获取一条文件变更消息
        /////
        /////
        //public FileChangeInformation Get()
        //{
        //    FileChangeInformation info = null;
        //    if (_queue.Count > 0)
        //    {
        //        lock (_queue)
        //        {
        //            info = _queue.Dequeue();
        //        }
        //    }
        //    return info;
        //}

        /////
        ///// 监听事件触发的方法
        /////
        /////
        /////
        //private void OnProcess(object sender, FileSystemEventArgs e)
        //{
        //    try
        //    {
        //        FileChangeType changeType = FileChangeType.Unknow;
        //        if (e.ChangeType == WatcherChangeTypes.Created)
        //        {
        //            if (File.GetAttributes(e.FullPath) == FileAttributes.Directory)
        //            {
        //                changeType = FileChangeType.NewFolder;
        //            }
        //            else
        //            {
        //                changeType = FileChangeType.NewFile;
        //            }
        //        }
        //        else if (e.ChangeType == WatcherChangeTypes.Changed)
        //        {
        //            //部分文件创建时同样触发文件变化事件，此时记录变化操作没有意义
        //            //如果
        //            if (_queue.SelectAll(
        //                delegate(FileChangeInformation fcm)
        //                {
        //                    return fcm.NewPath == e.FullPath && fcm.ChangeType == FileChangeType.Change;
        //                }).Count() > 0)
        //            {
        //                return;
        //            }

        //            //文件夹的变化，只针对创建，重命名和删除动作，修改不做任何操作。
        //            //因为文件夹下任何变化同样会触发文件的修改操作，没有任何意义.
        //            if (File.GetAttributes(e.FullPath) == FileAttributes.Directory)
        //            {
        //                return;
        //            }

        //            changeType = FileChangeType.Change;
        //        }
        //        else if (e.ChangeType == WatcherChangeTypes.Deleted)
        //        {
        //            changeType = FileChangeType.Delete;
        //        }

        //        //创建消息，并压入队列中
        //        FileChangeInformation info = new FileChangeInformation(Guid.NewGuid().ToString(), changeType, e.FullPath, e.FullPath, e.Name, e.Name);
        //        _queue.Enqueue(info);
        //    }
        //    catch
        //    {
        //        Close();
        //    }
        //}

        /////
        ///// 文件或目录重命名时触发的事件
        /////
        /////
        /////
        //private void OnFileRenamed(object sender, RenamedEventArgs e)
        //{
        //    try
        //    {
        //        //创建消息，并压入队列中
        //        FileChangeInformation info = new FileChangeInformation(Guid.NewGuid().ToString(), FileChangeType.Rename, e.OldFullPath, e.FullPath, e.OldName, e.Name);
        //        _queue.Enqueue(info);
        //    }
        //    catch
        //    {
        //        Close();
        //    }
        //}
    }
}
