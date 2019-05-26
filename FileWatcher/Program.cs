using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Program
    {

        private static string path;
        private static FileAttributes attr;
        static void Main(string[] args)
        {

            path = @"D:\folderToWatch";
            
            MonitorDirectory(path);
            Console.ReadKey();
        }

        private static void MonitorDirectory(string path)
        {
            FileSystemWatcher fw = new FileSystemWatcher();

            fw.Path = path;
            fw.Created += fsw_created;
            fw.Renamed += fsw_renamed;
            fw.Deleted += fsw_deleted;

            fw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fw.EnableRaisingEvents = true;
        }

        public static void fsw_created(object sender, FileSystemEventArgs e)
        {
            attr = File.GetAttributes(path + "\\" + e.Name);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("Directory created: {0}", e.Name);
            }
            else
            {
                Console.WriteLine("File created: {0}", e.Name);
            }    
        }

        public static void fsw_renamed(object sender, FileSystemEventArgs e)
        {
            attr = File.GetAttributes(path + "\\" + e.Name);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("Directory renamed: {0}", e.Name);
            }
            else
            {
                Console.WriteLine("File renamed: {0}", e.Name);
            }
        }

        public static void fsw_deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Directory/File deleted: {0}", e.Name);
        }

        public static void fsw_changed(object sender, FileSystemEventArgs e)
        {
            attr = File.GetAttributes(path + "\\" + e.Name);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("Director modified: {0}", e.Name);
            }
            else
            {
                Console.WriteLine("File modified: {0}", e.Name);
            }
        }
    }
}
