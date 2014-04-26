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

        private int _h5_id;
        private H5FileId _file_id;
        private H5GroupId _meta_data_grp_id;
        private string _filename;
        private SongBuilder _builder;

        public readonly Song SongInfo;

        private SongReader(string filename)
        {
            _filename = filename;
            _builder = new SongBuilder();

            _h5_id = H5.Open();

            if (FileInvalid())
            {
                throw new ArgumentException("File '" + filename + "' is not valid HDF5 file.", "filename");
            }

            OpenAndReadFile();

            H5.Close();
        }

        private bool FileInvalid()
        {
            return !H5F.is_hdf5(_filename);
        }

        private void OpenAndReadFile()
        {
            OpenFile();
            ReadFile();
        }

        private void OpenFile()
        {
            _file_id = H5F.open(_filename, H5F.OpenMode.ACC_RDONLY);
        }

        private void ReadFile()
        {
            ReadMetaData();
            //ReadSegmentData();
            //ReadSectionData();
        }

        private void ReadMetaData()
        {
            OpenMetaDataGroup();
            ReadSongsDataSet();
            //...
        }

        private void OpenMetaDataGroup()
        {
            _meta_data_grp_id = H5G.open(_file_id, "metadata");
        }

        private void ReadSongsDataSet()
        {
            var songs_data_id = H5D.open(_meta_data_grp_id, "songs");

            //H5DataTypeId hotttnesss_data_type = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);
            //double[] houtArr = new double[1];
            //H5Array<double> hotttnesss_data = new H5Array<double>(houtArr);
            //H5D.read<double>(songs_data_id, hotttnesss_data_type, hotttnesss_data);

            //H5DataTypeId artist_name_data_type = H5T.copy(H5T.H5Type.C_S1);
            //char[][] outArr = new char[][] { new char[1024] };
            //H5Array<char[]> artist_name_data = new H5Array<char[]>(outArr);
            //H5D.read<char[]>(songs_data_id, artist_name_data_type, artist_name_data);

            var enc = new System.Text.ASCIIEncoding();

            var sdtype = H5D.getType(songs_data_id);
            var ssize = H5T.getSize(sdtype);
            var smtype = H5T.create(H5T.CreateClass.STRING, ssize);
            var sbuffer = new byte[ssize];
            H5D.read(songs_data_id, sdtype, new H5Array<byte>(sbuffer));

            string output = enc.GetString(sbuffer);
            var splitteded = SplitIntoValues(output).ToArray();

            PrintArray(splitteded);

            Console.WriteLine("ArtistName: " + splitteded[9]);
            Console.ReadKey();



            var attr = H5A.open(songs_data_id, "FIELD_9_NAME");
            var dtype = H5A.getType(attr);
            var size = H5T.getSize(dtype);

            var mtype = H5T.create(H5T.CreateClass.STRING, size);
            var buffer = new byte[size];
            H5A.read(attr,mtype, new H5Array<byte>(buffer));

            

            Console.WriteLine("ArtistName: " + enc.GetString(buffer));
            Console.ReadKey();

            H5T.close(mtype);
            H5T.close(dtype);
            H5A.close(attr);
            H5D.close(songs_data_id);
        }

        public static IEnumerable<string> SplitIntoValues(string str)
        {
            bool string_builder_empty = true;
            string string_builder = "";
            int skip = 0;

            foreach (char c in str)
            {
                if (c == '\0' && string_builder_empty)
                {
                    skip++;
                    continue;
                }
                if (c == '\0')
                {
                    skip++;
                    yield return string_builder;
                    string_builder_empty = true;
                    string_builder = "";
                    continue;
                }

                if(string_builder_empty) Console.WriteLine("Skipped " + skip);
                skip = 0;
                string_builder_empty = false;
                string_builder += c;
            }
            yield return string_builder;
        }

        public static void PrintArray(IEnumerable<string> arr)
        {
            var index = 0;
            Console.WriteLine("[");
            var first = true;
            foreach (var i in arr)
            {
                if (first) first = false;
                Console.WriteLine("    "+index+" " + i);
                index++;
            }
            Console.WriteLine("]");
        }
    }
}
