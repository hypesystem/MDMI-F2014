using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MillionSongsDataWrapper;

namespace UltimateReader9000
{
    class LRNSongBuilder
    {
        public static Dictionary<Int32, Song> BuildSongsFromLRNAndNames(String LRNFile, String NamesFile)
        {
            Dictionary<Int32, Song> map = new Dictionary<int, Song>();

            using (var reader = new StreamReader(LRNFile))
            {
                for (int i = 0; i < 3; i++)
                {
                 reader.ReadLine();   
                }
                while (!reader.EndOfStream)
                {
                    String[] line = reader.ReadLine().Split('\t');
                    Song newSong = new Song();
                    int id = Int32.Parse(line[0]);
                    map[id] = newSong;


                    //_aggregatesList.Add(LoudnessStart);
                    //_aggregatesList.Add(LoudnessMax);
                    //_aggregatesList.Add(LoudnessMaxTime);
                    //_aggregatesList.Add(Duration);
                    //_aggregatesList.AddRange(Pitches);
                    //_aggregatesList.AddRange(Timbre);



                }
            }


            return map;

        }

        private static Aggregates BuildAggregate(int start, String[] line)
        {
            int copy = start;
            Aggregates a = new Aggregates(Int32.Parse(line[copy++]), Int32.Parse(line[copy++]), Int32.Parse(line[copy++]),
                Int32.Parse(line[copy++]), Int32.Parse(line[copy++]), Int32.Parse(line[copy++]), Int32.Parse(line[copy++]), Int32.Parse(line[copy++]));

            return a;
        }
    }

}
