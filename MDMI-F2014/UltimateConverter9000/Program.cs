using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5Reader;
using MillionSongsDataWrapper;
using LRNWriter;
using System.Diagnostics;

namespace UltimateConverter9000
{
    class Program
    {
        static string data_path = @"C:\Users\hypesystem\Downloads\millionsongsubset_full\MillionSongSubset\data";

        static int chunk_size = 10;

        static void Main(string[] args)
        {
            var trav = new FileTraverser(data_path);
            Console.ReadKey();

            var stopwatch = new Stopwatch();

            int i = 0;
            stopwatch.Start();
            var files_to_write = new List<Song>();
            foreach (var file in trav.Files)
            {
                if (i % chunk_size == 0 && i > 0)
                {
                    stopwatch.Stop();
                    Console.WriteLine("Read " + chunk_size + " (total "+i+") files in "+stopwatch.Elapsed);
                    stopwatch.Reset();
                    Console.ReadKey();

                    stopwatch.Start();
                    var writer = new LRNWriter.LRNWriter(files_to_write);
                    writer.WriteLRNToFile("" + (i / 10) + ".lrn");
                    files_to_write = new List<Song>();
                    //Consider force garbage collect
                    stopwatch.Stop();
                    Console.WriteLine("Written " + chunk_size + " (total "+i+") files in "+stopwatch.Elapsed);
                    stopwatch.Reset();
                    Console.ReadKey();
                    stopwatch.Start();
                }

                var song = SongReader.ReadSongFile(file);
                files_to_write.Add(song);
                
                i++;
            }
        }
    }
}
