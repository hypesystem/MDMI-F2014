using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionSongsDataWrapper;

namespace HDF5Reader
{
    public class SongBuilder
    {
        public string ArtistName, TrackName;
        public double Familiarity, Hotttnesss;

        public double Danceability, Duration, Energy, Tempo, ArtistLongtitude, ArtistLatitude;
        public string Genre;
        public Key Key;
        public TimeSignature TimeSignature;

        public SegmentStats SegmentStats;
        public SectionStats SectionsStats;

        public Song Build()
        {
            return new Song(
                artistName: ArtistName,
                trackName: TrackName,
                familiarity: Familiarity,
                hotttnesss: Hotttnesss,
                danceability: Danceability,
                duration: Duration,
                energy: Energy,
                tempo: Tempo,
                artistLongtitude: ArtistLongtitude,
                artistLatitude: ArtistLatitude,
                key: Key,
                timeSignature: TimeSignature,
                segmentStats: SegmentStats,
                sectionsStats: SectionsStats,
                genre: Genre
            );
        }
    }
}
