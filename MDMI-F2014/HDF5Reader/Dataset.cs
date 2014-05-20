using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace HDF5Reader
{
    /// <summary>
    /// Represents a dataset, allowing it to be read.
    /// </summary>
    abstract class Dataset : IDisposable
    {
        private string _datasetname;
        private H5DataSetId _dataset_id;
        private List<Row> _rows;

        public readonly Encoding Encoding = new ASCIIEncoding();

        public Dataset(Container container, string datasetname)
        {
            _datasetname = datasetname;
            _dataset_id = H5D.open(container.Id, datasetname);

            _rows = new List<Row>();

            LoadData();
        }

        public H5DataSetId Id
        {
            get
            {
                return _dataset_id;
            }
        }

        protected abstract void LoadData();

        protected long FindNumberOfRows()
        {
            var num_rows_data = H5S.getSimpleExtentDims(H5D.getSpace(Id));
            return num_rows_data[0];
        }

        protected void AddRow(Row r)
        {
            _rows.Add(r);
        }

        public Row this[int i]
        {
            get
            {
                return _rows[i];
            }
        }

        public void Dispose() {
            H5D.close(_dataset_id);
        }
    }
}
