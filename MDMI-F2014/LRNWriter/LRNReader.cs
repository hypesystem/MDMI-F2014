using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MillionSongsDataWrapper;
using HDF5Reader;

namespace LRNWriter
{
    public class LRNReader
    {
        private string _filepath;

        public LRNReader(string filepath) {
            _filepath = filepath;
        }

        public IEnumerable<Song> ReadSongs()
        {
            using (var reader = new StreamReader(_filepath))
            {
                var num_rows = reader.ReadLine();
                var num_cols = reader.ReadLine();
                var types = reader.ReadLine();
                var names = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    yield return ParseRow(row);
                }
            }
        }

        Song ParseRow(string row_data)
        {
            var song_builder = new SongBuilder();
            string[] fields = row_data.Split('\t');

            //0 -> id; ignore
            int ptr = 1;
            //Segment stats
            //1-8 -> LoudnessStart
            var loudness_start = ReadAggregate(fields, ref ptr);
            //9-16 -> LoudnessMax
            var loudness_max = ReadAggregate(fields, ref ptr);
            //17-24 -> LoudnessMaxTime
            var loudness_max_time = ReadAggregate(fields, ref ptr);
            //25-32 -> Duration
            var seg_duration = ReadAggregate(fields, ref ptr);
            //Segment stats: Pitches
            var pitches = ReadAggregates(fields, ref ptr, 12);
            //Segment stats: Timbre
            var timbre = ReadAggregates(fields, ref ptr, 12);
            var segment_stats = new SegmentStats(0d, loudness_start, loudness_max_time, loudness_max, seg_duration, pitches, timbre);
            //Sections stats
            //?? -> Count
            var sec_count = ReadIntField(fields, ref ptr);
            //??+7 -> Duration
            var sec_duration = ReadAggregate(fields, ref ptr);
            var sections_stats = new SectionStats(0d, sec_duration, sec_count);

            //Single attrs
            //?? -> ArtistLatitude
            var artist_latitude = ReadDoubleField(fields, ref ptr);
            //?? -> ArtistLongitude
            var artist_longitude = ReadDoubleField(fields, ref ptr);
            //?? -> Danceability
            var danceability = ReadDoubleField(fields, ref ptr);
            //?? -> Duration
            var duration = ReadDoubleField(fields, ref ptr);
            //?? -> Energy
            var energy = ReadDoubleField(fields, ref ptr);
            //?? -> Tempo
            var tempo = ReadDoubleField(fields, ref ptr);
            //?? -> ArtistName
            var artist_name = ReadField(fields, ref ptr);
            //?? -> Genre
            var genre = ReadField(fields, ref ptr);
            //?? -> TrackName
            var track_name = ReadField(fields, ref ptr);

            return new Song(artist_name, track_name, segment_stats, sections_stats, 0d, 0d, artist_longitude, artist_latitude, danceability, duration, energy, tempo, Key.AflatMaj, new TimeSignature(), genre);
        }

        Aggregates ReadAggregate(string[] fields, ref int ptr)
        {
            var min = ReadDoubleField(fields, ref ptr);
            var max = ReadDoubleField(fields, ref ptr);
            var mean = ReadDoubleField(fields, ref ptr);
            var median = ReadDoubleField(fields, ref ptr);
            var skewness = ReadDoubleField(fields, ref ptr);
            var variance = ReadDoubleField(fields, ref ptr);
            var value_range = ReadDoubleField(fields, ref ptr);
            var kurtosis = ReadDoubleField(fields, ref ptr);

            return new Aggregates(min, max, mean, median, variance, value_range, skewness, kurtosis);
        }

        double ReadDoubleField(string[] fields, ref int ptr)
        {
            var str = ReadField(fields, ref ptr);
            double dbl;
            if (double.TryParse(str, out dbl))
                return dbl;

            throw new ArgumentException("Invalid double value '" + dbl + "'.", "fields");
        }

        string ReadField(string[] fields, ref int ptr)
        {
            return fields[ptr++];
        }

        Aggregates[] ReadAggregates(string[] fields, ref int ptr, int num_aggrs)
        {
            var pitches = new Aggregates[num_aggrs];

            for (int i = 0; i < num_aggrs; i++)
            {
                pitches[i] = ReadAggregate(fields, ref ptr);
            }

            return pitches;
        }

        int ReadIntField(string[] fields, ref int ptr)
        {
            var str = ReadField(fields, ref ptr);
            int intgr;
            if (int.TryParse(str, out intgr))
                return intgr;

            throw new ArgumentException("Invalid integer format '" + str + "'", "fields");
        }
    }
}
