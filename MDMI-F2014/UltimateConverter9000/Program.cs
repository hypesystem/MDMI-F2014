﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HDF5Reader;
using MillionSongsDataWrapper;
using LRNWriter;
using System.Diagnostics;

namespace UltimateConverter9000
{
    /// <summary>
    /// This program reads all of the hdf5 files in a directory, parsing them as songs. The songs are then written to
    /// an LRN file. It also times the conversion and writes out in chunks of 100 files to avoid memory overload (garbage
    /// collection will clean up the data during conversion).
    /// </summary>
    class Program
    {
        static string data_path = @"C:\Users\hypesystem\Downloads\millionsongsubset_full(1)\MillionSongSubset\data";

        static int chunk_size = 100;

        static void Main(string[] args)
        {
            var trav = new FileTraverser(data_path);

            var stopwatch = new Stopwatch();

            var writer = new LRNWriter.LRNWriter("data.lrn", trav.Files.Count());
            var writer2 = new NamesWriter.NamesWriter("data.names", trav.Files.Count());

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

                    stopwatch.Start();
                    writer.AddFilesToWrite(files_to_write);
                    writer.WriteSongsToFile();

                    writer2.AddFilesToWrite(files_to_write);
                    writer2.WriteSongsToFile();
                    

                    
                    files_to_write = new List<Song>();

                    stopwatch.Stop();
                    Console.WriteLine("Written " + chunk_size + " (total "+i+") files in "+stopwatch.Elapsed);
                    stopwatch.Reset();
                    stopwatch.Start();
                }

                try
                {
                    var song = SongReader.ReadSongFile(file);
                    files_to_write.Add(song);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred! Skipping file " + file);
                }
                
                i++;
            }
        }
    }
}
