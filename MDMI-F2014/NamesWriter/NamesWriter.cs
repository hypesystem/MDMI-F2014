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

namespace NamesWriter
{
    public class NamesWriter
    {
        private static readonly char TABCHAR = '\t';
        private static readonly char METACHAR = '%';
        private int _currentID, _totalFiles;
        private SegmentStats _exampleSegmentStat;
        private Aggregates _exampleAggregate;
        private readonly List<Song> _writeList;
        private String _filePath;
        private Boolean _firstWrite;

        public NamesWriter(String filePath, int totalFiles)
        {
           
            _writeList = new List<Song>();
            _totalFiles = totalFiles;
            _filePath = filePath;
            _firstWrite = true;
        }



        public void AddFilesToWrite(List<Song> filesToWrite)
        {
            _writeList.AddRange(filesToWrite);
         
        }


        public void WriteSongsToFile()
        {
            if (_writeList.Count == 0) throw new ArgumentException("Add songs to file before writing");


            using (var writer = _firstWrite ? new StreamWriter(_filePath, false) : new StreamWriter(_filePath, true))
            {
                // String cmt = @"#LRN parsing 1" + "\n";
                // writer.Write(cmt);
                if (_firstWrite)
                {
                    WriteMetaData(writer);
                    _firstWrite = false;
                }

                foreach (var song in _writeList)
                {
                    writer.WriteLine(GenerateRowLine(song));
                }

                writer.Flush();

            }

            _writeList.Clear();

        }

        private String GenerateRowLine(Song song)
        {

            StringBuilder row = new StringBuilder();
            row.Append(_currentID++ + "" + TABCHAR);

            var dick = song.GetIgnoredAttributeMap();

            row.Append("\"");
            row.Append(dick["ArtistName"] + " , " + dick["TrackName"]);
            row.Append("\"" + TABCHAR);
            row.Append(dick["Genre"]);
            row.Append("\"");
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
            writer.WriteLine(METACHAR + " " + _totalFiles);
          
        }

      

    }
}
