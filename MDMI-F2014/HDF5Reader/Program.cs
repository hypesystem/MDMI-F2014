using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace HDF5Reader
{
    class Program
    {
        private static string file_path = @"C:\Users\hypesystem\Dropbox\Public\programmering\MDMI-F2014\data_sample\TRAXLZU12903D05F94.h5";

        static void Main(string[] args)
        {
            var info = SongReader.ReadSongFile(file_path);
        }
    }
}
