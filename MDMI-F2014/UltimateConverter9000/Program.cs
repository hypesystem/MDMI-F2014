using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5Reader;
using MillionSongsDataWrapper;
using LRNWriter;

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
            var files_to_write = new List<Song>();
            foreach (var file in trav.Files)
            {
                if (i % 10 == 0 && i > 0)
                {
                    Console.WriteLine("Read " + i + " files");
                    Console.ReadKey();
                    var writer = new LRNWriter.LRNWriter(files_to_write);
                    writer.WriteLRNToFile("" + (i / 10) + ".lrn");
                    files_to_write = new List<Song>();
                    //Consider force garbage collect
                    Console.WriteLine("Written " + i + " files");
                    Console.ReadKey();
                }

                var song = SongReader.ReadSongFile(file);
                files_to_write.Add(song);
                
                i++;
            }
        }
    }
}
