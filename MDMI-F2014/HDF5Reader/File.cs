using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace HDF5Reader
{
    class File : Container, IDisposable
    {
        private static int? _h5_lib_id = null;

        private string _filename;
        private H5F.OpenMode _mode;
        private H5FileId _file_id;

        public File(string filename, H5F.OpenMode mode = H5F.OpenMode.ACC_RDONLY)
        {
            _filename = filename;
            _mode = mode;

            EnsureH5Lib();
            OpenFile();
        }

        void EnsureH5Lib()
        {
            if (_h5_lib_id == null)
            {
                _h5_lib_id = H5.Open();
            }
        }

        void OpenFile()
        {
            _file_id = H5F.open(_filename, _mode);
        }

        public override H5FileOrGroupId Id
        {
            get
            {
                return _file_id;
            }
        }

        public void Dispose()
        {
            H5F.close(_file_id);
            H5.Close();
        }
    }
}
