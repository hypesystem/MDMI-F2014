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
            H5.Open();
            var is_hdf5 = H5F.is_hdf5(file_path);
            Console.WriteLine("Is hdf5? " + is_hdf5);
            Console.ReadKey();
            var file_id = H5F.open(file_path, H5F.OpenMode.ACC_RDONLY);
        }
    }
}
