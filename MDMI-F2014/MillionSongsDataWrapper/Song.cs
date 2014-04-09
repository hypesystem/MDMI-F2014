namespace MillionSongsDataWrapper
{
    public class Song
    {
        public readonly string ArtistName;
        public readonly string TrackName;
        public readonly SegmentStats SegmentStats;
        public readonly SectionStats SectionsStats;
        public readonly double Familiarity;
        public readonly double Hotttnesss;
        public readonly double ArtistLongtitude;
        public readonly double ArtistLatitude;
        public readonly double Danceability;
        public readonly double Duration;
        public readonly double Energy;
        public readonly double Tempo;
        public readonly Key Key;
        public readonly TimeSignature TimeSignature;

        public Song(string artistName, string trackName, SegmentStats segmentStats, SectionStats sectionsStats, 
            double familiarity, double hotttnesss, double artistLongtitude, double artistLatitude, 
            double danceability, double duration, double energy, double tempo, Key key, TimeSignature timeSignature)
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
        }
    }
}
