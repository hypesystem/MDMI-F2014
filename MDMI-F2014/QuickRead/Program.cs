using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LRNWriter;

namespace QuickRead
{
    class Program
    {
        static void Main(string[] args)
        {
            var total_runtime = Stopwatch.StartNew();

            var reader = new LRNReader("data.lrn");
            var songs = reader.ReadSongs();

            int i = 0;
            var stopwatch = Stopwatch.StartNew();
            foreach (var s in songs)
            {
                i++;
                if (i % 100 == 0)
                {
                    stopwatch.Stop();
                    Console.WriteLine("Read 100 songs in "+stopwatch.Elapsed+" (total " + i + ").");
                    stopwatch.Restart();
                }
            }

            total_runtime.Stop();
            Console.WriteLine("Concluded in "+total_runtime.Elapsed);
            Console.ReadKey();
        }
    }
}
