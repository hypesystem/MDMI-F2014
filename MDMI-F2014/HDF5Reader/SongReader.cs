using System;
using System.Collections.Generic;
using System.Linq;
using HDF5DotNet;
using MillionSongsDataWrapper;

namespace HDF5Reader
{
    public class SongReader
    {
        public static Song ReadSongFile(string filename)
        {
            var reader = new SongReader(filename);
            return reader.SongInfo;
        }

        public readonly Song SongInfo;

        private File _f;
        private SongBuilder _builder;

        private SongReader(string filename)
        {
            _f = new File(filename);
            _builder = new SongBuilder();

            ReadMetaData();
            ReadAnalysisData();

            SongInfo = _builder.Build();

            _f.Dispose();
        }

        void ReadMetaData()
        {
            using (var metadata = _f.GetGroup("metadata"))
            {
                using(var song = metadata.GetCompoundDataset("songs")) {
                    var row = song[0];

                    _builder.ArtistName = row.GetString("artist_name");
                    _builder.TrackName = row.GetString("title");
                    _builder.Familiarity = row.GetDouble("artist_familiarity");
                    _builder.Hotttnesss = row.GetDouble("song_hotttnesss");
                    _builder.ArtistLongtitude = row.GetDouble("artist_longitude");
                    _builder.ArtistLatitude = row.GetDouble("artist_latitude");
                }

                using (var terms = metadata.GetScalarDataset("artist_terms"))
                {
                    var row = terms[0];

                    _builder.Genre = row.GetString("0");
                }
            }
        }

        void ReadAnalysisData()
        {
            using (var analysis = _f.GetGroup("analysis"))
            {
                //Song info
                using (var song = analysis.GetCompoundDataset("songs"))
                {
                    var row = song[0];

                    _builder.Duration = row.GetDouble("duration");
                    _builder.Danceability = row.GetDouble("danceability");
                    _builder.Energy = row.GetDouble("energy");
                    _builder.Tempo = row.GetDouble("tempo");
                    _builder.Key = KeyExtensions.FromInt(row.GetInteger("key"), row.GetInteger("mode"));
                    _builder.TimeSignature = TimeSignatureExtension.FromInt(row.GetInteger("time_signature"));
                }


                //Segments
                var segments_confidence = analysis.GetScalarDataset("segments_confidence");
                var segments_loudness_start = analysis.GetScalarDataset("segments_loudness_start");
                var segments_loudness_max_time = analysis.GetScalarDataset("segments_loudness_max_time");
                var segments_loudness_max = analysis.GetScalarDataset("segments_loudness_max");
                var segments_start = analysis.GetScalarDataset("segments_start");
                var segments_pitches = analysis.GetScalarDataset("segments_pitches");
                var segments_timbre = analysis.GetScalarDataset("segments_timbre");

                var num_segments = segments_confidence.Count();
                var segments = new Segment[num_segments];
                for (int i = 0; i < num_segments; i++)
                {
                    var duration = i < (num_segments - 1) ?
                        segments_start[i + 1].GetDouble("0") - segments_start[i].GetDouble("0") :
                        _builder.Duration - segments_start[i].GetDouble("0");

                    var pitch_row = segments_pitches[i];
                    var pitches = new double[12];
                    for (int j = 0; j < 12; j++)
                        pitches[j] = pitch_row.GetDouble("" + j);

                    var timbre_row = segments_timbre[i];
                    var timbre = new double[12];
                    for (int j = 0; j < 12; j++)
                        timbre[j] = timbre_row.GetDouble("" + j);

                    segments[i] = new Segment(
                        segments_loudness_start[i].GetDouble("0"),
                        segments_loudness_max_time[i].GetDouble("0"),
                        segments_loudness_max[i].GetDouble("0"),
                        duration,
                        pitches,
                        timbre,
                        segments_confidence[i].GetDouble("0")
                    );
                }
                _builder.SegmentStats = Aggregator.AggregateSegments(segments);

                segments_confidence.Dispose();
                segments_loudness_start.Dispose();
                segments_loudness_max_time.Dispose();
                segments_loudness_max.Dispose();
                segments_start.Dispose();
                segments_pitches.Dispose();
                segments_timbre.Dispose();


                //Sections
                var sections_confidence = analysis.GetScalarDataset("sections_confidence");
                var sections_start = analysis.GetScalarDataset("sections_start");

                var num_sections = sections_confidence.Count();
                var sections = new Section[num_sections];
                for (int i = 0; i < num_sections; i++)
                {
                    var duration = i < (num_sections - 1) ?
                        sections_start[i + 1].GetDouble("0") - sections_start[i].GetDouble("0") :
                        _builder.Duration - sections_start[i].GetDouble("0");

                    sections[i] = new Section(
                        sections_confidence[i].GetDouble("0"),
                        duration
                    );
                }
                _builder.SectionsStats = Aggregator.AggregateSections(sections);

                sections_confidence.Dispose();
                sections_start.Dispose();
            }
        }
    }
}
