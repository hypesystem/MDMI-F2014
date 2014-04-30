using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MillionSongsDataWrapper;

namespace LRNWriterTest
{
    public class TestLRNWriter
    {

        static void Main(string[] args)
        {
            Aggregates[] arr = new Aggregates[12];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Aggregates();
            }
            var segmentStats = new SegmentStats(0, new Aggregates(), new Aggregates(), new Aggregates(),
                new Aggregates(), arr, arr);

            var section = new SectionStats(0, new Aggregates(), 0);

            Song testSong = new Song("John", "Ok", segmentStats,section, 0,0,0,0,0,0,0,0, Key.Cmaj, TimeSignature.Complex);

            List<Song> testList = new List<Song>();
            testList.Add(testSong);

            var testSubject = new LRNWriter.LRNWriter(testList);

            String filePath = @"test.lrn";
            testSubject.WriteLRNToFile(filePath);

        }
    }
}
