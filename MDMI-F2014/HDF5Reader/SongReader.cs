﻿using System;
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
        }

        void ReadMetaData()
        {
            var metadata = _f.GetGroup("metadata");
            var song = metadata.GetCompoundDataset("songs")[0];

            _builder.ArtistName = song.GetString("artist_name");
            _builder.TrackName = song.GetString("title");
            _builder.Familiarity = song.GetDouble("artist_familiarity");
            _builder.Hotttnesss = song.GetDouble("song_hotttnesss");

            //throw new NotImplementedException();
            //Missing some info!
        }

        void ReadAnalysisData()
        {
            var analysis = _f.GetGroup("analysis");

            //Song info
            var song = analysis.GetCompoundDataset("songs")[0];
            _builder.Duration = song.GetDouble("duration");
            _builder.Danceability = song.GetDouble("danceability");
            _builder.Energy = song.GetDouble("energy");
            _builder.Tempo = song.GetDouble("tempo");
            //Key:
            //TimeSignature:
            //throw new NotImplementedException();

            //Segments
            var segments_confidence = analysis.GetScalarDataset("segments_confidence");
            var segments_loudness_start = analysis.GetScalarDataset("segments_loudness_start");
            var segments_loudness_max_time = analysis.GetScalarDataset("segments_loudness_max_time");
            var segments_loudness_max = analysis.GetScalarDataset("segments_loudness_max");
            var segments_start = analysis.GetScalarDataset("segments_start");
            var segments_timbre = analysis.GetScalarDataset("segments_timbre");

            //throw new NotImplementedException();
            //Missing timbre and pitches!

            var num_segments = segments_confidence.Count();
            Console.WriteLine("Segments: " + num_segments);
            var segments = new Segment[num_segments];
            for (int i = 0; i < num_segments; i++)
            {
                var duration = i < (num_segments - 1) ?
                    segments_start[i + 1].GetDouble("data") - segments_start[i].GetDouble("data") :
                    _builder.Duration - segments_start[i].GetDouble("data");

                var pitches = new double[12];
                var timbre = new double[12];
                //throw new NotImplementedException();
                //Support timbre and pitches!

                segments[i] = new Segment(
                    segments_loudness_start[i].GetDouble("data"),
                    segments_loudness_max_time[i].GetDouble("data"),
                    segments_loudness_max[i].GetDouble("data"),
                    duration,
                    pitches,
                    timbre,
                    segments_confidence[i].GetDouble("data")
                );
            }
            _builder.SegmentStats = Aggregator.AggregateSegments(segments);

            //Sections
            var sections_confidence = analysis.GetScalarDataset("sections_confidence");
            var sections_start = analysis.GetScalarDataset("sections_start");

            var num_sections = sections_confidence.Count();
            Console.WriteLine("Sections: "+num_sections);
            var sections = new Section[num_sections];
            for(int i = 0; i < num_sections; i++) {
                var duration = i < (num_sections - 1) ?
                    sections_start[i + 1].GetDouble("data") - sections_start[i].GetDouble("data") :
                    _builder.Duration - sections_start[i].GetDouble("data");

                sections[i] = new Section(
                    sections_confidence[i].GetDouble("data"),
                    duration
                );
            }
            _builder.SectionsStats = Aggregator.AggregateSections(sections);
        }
    }
}