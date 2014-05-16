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

        IEnumerable<Song> ReadSongs()
        {
            using (var reader = new StreamReader(_filepath))
            {
                var rows = reader.ReadLine();
                var cols = reader.ReadLine();
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
            var fields = row_data.Split('\t');

            throw new NotImplementedException();
        }
    }
}
