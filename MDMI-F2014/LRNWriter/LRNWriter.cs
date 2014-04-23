using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MillionSongsDataWrapper;

namespace LRNWriter
{
    public class LRNWriter
    {
        private static readonly char TABCHAR = '\t';
        private static readonly char METACHAR = '%';
        private int _columnCount;
        private StringBuilder columnNameBuilder, columnTypeBuilder;
        private SegmentStats _exampleSegmentStat;
        private Aggregates _exampleAggregate;
        private Song _exampleSong;
        private readonly List<Song> writeList;
        private char _columnPrefix;
        private int _currentID;

        public LRNWriter(List<Song> writeList)
        {
            _currentID = 0;
            _columnCount = 0;
            _columnPrefix = '0';
            columnNameBuilder = new StringBuilder();
            columnTypeBuilder = new StringBuilder();
            this.writeList = writeList;
        }

        public void WriteLRNToFile(String filePath)
        {
            _exampleSong = writeList.First();
            _exampleSegmentStat = _exampleSong.SegmentStats;
            _exampleAggregate = _exampleSegmentStat.LoudnessMax;

            using (var writer = new StreamWriter(filePath))
            {
                String cmt = @"#LRN parsing 1" + "\n";
                writer.Write(cmt);
                
                WriteMetaData(writer);
                foreach (var song in writeList)
                {
                    writer.WriteLine(GenerateRowLine(song));
                }

                writer.Flush();

            }

           
        }
        
        private String GenerateRowLine(Song song)
        {

            StringBuilder row = new StringBuilder();
            row.Append(_currentID++ + TABCHAR);
            // Write segmentstats
            foreach (var aggregate in song.SegmentStats.AggregatesList)
            {
                AppendAggregateValues(row, aggregate);
            }

            // Write section stats
            row.Append(song.SectionsStats.Count + TABCHAR);
            AppendAggregateValues(row, song.SectionsStats.Duration);

            // Write lone attributes
            var map = song.GetESOMAttributeMap();
            foreach (var key in map.Keys)
            {
                row.Append(map[key] + TABCHAR);
            }
            return row.ToString();
        }

        private void AppendAggregateValues(StringBuilder builder, Aggregates aggregate)
        {
            foreach (var value in aggregate.PropertyValuesList)
            {
                builder.Append(value + "" + TABCHAR);
            }
        }

        private void WriteMetaData(StreamWriter writer)
        {
            GenerateColumns();
            writer.WriteLine(METACHAR + " " + writeList.Count);
            writer.WriteLine(METACHAR + " " + _columnCount);
            writer.WriteLine(METACHAR + " " + columnTypeBuilder);
            writer.WriteLine(METACHAR + " " + columnNameBuilder);
        }

        private void GenerateColumns()
        {
           
            columnTypeBuilder.Append(METACHAR +  " 9" + TABCHAR);
            columnNameBuilder.Append(METACHAR +  " id" + TABCHAR);
            _columnCount++;

            _columnCount++;
            WriteSegmentStats(_exampleSong.SegmentStats);
            WriteSectionStats();
            foreach (var moreNames in _exampleSong.GetESOMAttributeMap().Keys)
            {
                _columnCount++;
                columnTypeBuilder.Append('1' + TABCHAR);
                columnNameBuilder.Append(moreNames + TABCHAR);

            }

        }

        private void WriteSectionStats()
        {
            columnNameBuilder.Append("Count" + TABCHAR);
            columnTypeBuilder.Append('1' + TABCHAR);
            WriteAggregate(_columnPrefix++);
        }

        private void WriteSegmentStats(SegmentStats example)
        {
            
            // TODO fix Hardcoded aggregate count
            const int aggregateCount = 4;
            for (int i = 0; i < aggregateCount; i++)
            {
                WriteAggregate(_columnPrefix++);
            }
            
            WriteAggregateArray(_columnPrefix++, example.Pitches.Length);
            WriteAggregateArray(_columnPrefix++, example.Timbre.Length);
        }

        private void WriteAggregateArray(char prefix, int length)
        {
            char extraPrefix = 'a';
            for (int i = 0; i < length; i++)
            {
                WriteAggregate(prefix, extraPrefix);
            }
        }

      
        private void WriteAggregate(params char[] prefix)
        {
            String fullPrefix = "";
            foreach (var chars in prefix)
            {
                fullPrefix += chars;
            }
            foreach (var names in Aggregates.GetPropertyNameList())
            {
                _columnCount++;
                columnNameBuilder.Append(fullPrefix + names + TABCHAR);
                columnTypeBuilder.Append("1" + TABCHAR);
            }

        }
    }
}
