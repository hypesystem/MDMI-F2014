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
            var file = new File(file_path);
            var metadata = file.GetGroup("metadata");
            var songs_data = metadata.GetDataset("songs");


            //SongReader.ReadSongFile(file_path);

            /*
            H5.Open();
            var is_hdf5 = H5F.is_hdf5(file_path);
            Console.WriteLine("Is hdf5? " + is_hdf5);
            Console.ReadKey();

            var file_id = H5F.open(file_path, H5F.OpenMode.ACC_RDONLY);
            Console.WriteLine("Opened file.");

            var grp_id = H5G.open(file_id, "analysis");

            var set_info = H5D.open(grp_id, "segments_loudness_max");
            Console.Write("LoudnessMax # entries: ");
            PrintArray(H5S.getSimpleExtentDims(H5D.getSpace(set_info)));
            Console.ReadKey();

            Console.WriteLine("LoudnessMax entries: ");

            //Shutdown.
            H5F.close(file_id);
            H5.Close();
            Console.WriteLine("Everything closed.");
            Console.ReadKey();
            */
        }

        public static void PrintArray(Int64[] arr)
        {
            Console.Write("(" + arr.Length + ") [");
            var first = true;
            foreach (var i in arr)
            {
                if (first) first = false;
                else Console.Write(",");
                Console.Write(i);
            }
            Console.WriteLine("]");
        }
    }
}
