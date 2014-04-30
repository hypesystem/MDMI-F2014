using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace UltimateConverter9000
{
    class FileTraverser
    {
        List<string> _file_paths = new List<string>();

        public FileTraverser(string root_folder)
        {
            var time = new Stopwatch();
            time.Start();
            FindAllH5FilesRecursively(root_folder);
            time.Stop();
            Console.WriteLine("Found " + _file_paths.Count + " h5 paths in " + time.Elapsed);
        }

        void FindAllH5FilesRecursively(string folder)
        {
            if (!Directory.Exists(folder))
                throw new ArgumentException("Path is not a folder.", "folder");

            FindAllH5Files(folder);

            foreach (var subfolder in GetAllFolders(folder))
                FindAllH5FilesRecursively(subfolder);
        }

        void FindAllH5Files(string folder) {
            foreach (var file in Directory.GetFiles(folder))
            {
                if (file.Substring(file.Length - 3) == ".h5")
                {
                    _file_paths.Add(file);
                }
            }
        }

        IEnumerable<string> GetAllFolders(string folder)
        {
            return Directory.GetDirectories(folder);
        }

        public IEnumerable<string> Files
        {
            get
            {
                return _file_paths;
            }
        }
    }
}
