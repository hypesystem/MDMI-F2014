using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5Reader;

namespace UltimateConverter9000
{
    class Program
    {
        static string data_path = @"C:\Users\hypesystem\Downloads\millionsongsubset_full\MillionSongSubset\data";

        static void Main(string[] args)
        {
            var trav = new FileTraverser(data_path);
            Console.ReadKey();

            int i = 0;
            foreach (var file in trav.Files)
            {
                if (i % 100 == 0 && i > 0)
                {
                    Console.WriteLine("Read " + i + " files");
                    Console.ReadKey();
                }

                var song = SongReader.ReadSongFile(file);
                
                i++;
            }
        }
    }
}
