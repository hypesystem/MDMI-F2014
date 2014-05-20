using System;
using System.Collections.Generic;

namespace MillionSongsDataWrapper
{
    /// <summary>
    /// Represents a single song. Contains aggregated data.
    /// </summary>
    public class Song
    {
        public readonly string ArtistName;
        public readonly string TrackName;
        public readonly string Genre;
        // ESOM Clustering attributes
       
        public readonly SegmentStats SegmentStats;
        public readonly SectionStats SectionsStats;
        public readonly double Danceability;
        public readonly double Duration;
        public readonly double Energy;
        public readonly double Tempo;
        public readonly double ArtistLongtitude;
        public readonly double ArtistLatitude;
          
        // ESOM Clustering attributes stop

        public readonly double Familiarity;
        public readonly double Hotttnesss;
      
        public readonly Key Key;
        public readonly TimeSignature TimeSignature;

        public Song(string artistName, string trackName, SegmentStats segmentStats, SectionStats sectionsStats, 
            double familiarity, double hotttnesss, double artistLongtitude, double artistLatitude, 
            double danceability, double duration, double energy, double tempo, Key key, TimeSignature timeSignature, String genre = null)
        {
            ArtistName = artistName;
            TrackName = trackName;
            SegmentStats = segmentStats;
            SectionsStats = sectionsStats;
            Familiarity = familiarity;
            Hotttnesss = hotttnesss;
            ArtistLongtitude = artistLongtitude;
            ArtistLatitude = artistLatitude;
            Danceability = danceability;
            Duration = duration;
            Energy = energy;
            Tempo = tempo;
            Key = key;
            TimeSignature = timeSignature;
            Genre = genre;
        }

        public Song()
        {
            
        }

        public double[] DistanceArray()
        {
            List<double> list = new List<double>();
            list.AddRange(SegmentStats.DistanceArray());
            list.AddRange(SectionsStats.DistanceArray());
            return list.ToArray();
        }

        public SortedDictionary<string, double> GetESOMAttributeMap()
        {
            var map = new SortedDictionary<string, double >();

            map.Add("Danceability", Danceability);
            map.Add("Duration", Duration);
            map.Add("Energy", Energy);
            map.Add("Tempo", Tempo);
            map.Add("ArtistLongtitude", ArtistLongtitude);
            map.Add("ArtistLatitude", ArtistLatitude);

            return map;
        }

        public SortedDictionary<string, object> GetIgnoredAttributeMap()
        {
            var map = new SortedDictionary<string, object>();
            map.Add("ArtistName", ArtistName);
            map.Add("TrackName", TrackName);
            map.Add("Genre", Genre);
            return map;
        }
    }
}
