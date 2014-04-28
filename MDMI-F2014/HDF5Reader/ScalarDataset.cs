using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace HDF5Reader
{
    class ScalarDataset : Dataset, IEnumerable<Row>
    {
        public ScalarDataset(Container container, string datasetname) : base(container, datasetname) { }

        protected override void LoadData()
        {
            var dtype = H5D.getType(Id);
            var size = H5T.getSize(dtype);

            var space = H5D.getSpace(Id);
            var new_size = H5S.getSimpleExtentDims(space);

            foreach (var dim in new_size)
            {
                Console.WriteLine("Dim: " + dim);
            }
            Console.WriteLine("Size: " + size);

            byte[,] _row_data = new byte[new_size[0],size];
            H5D.read(Id, dtype, new H5Array<byte>(_row_data));

            Console.WriteLine("Row Data: " + _row_data.GetLongLength(0) + "," + _row_data.GetLongLength(1));

            var _row = new byte[size];
            for (int i = 0; i < size; i++ )
            {
                _row[i] = _row_data[0, i];
            }

            var _row2 = new byte[size];
            for (int i = 0; i < size; i++)
            {
                _row2[i] = _row_data[1, i];
            }

            Console.WriteLine(Encoding.GetString(_row));
            Console.WriteLine(Encoding.GetString(_row2));

            throw new NotImplementedException();
        }

        public IEnumerator<Row> GetEnumerator()
        {
            return new DatasetRowEnumerator(this, (int)FindNumberOfRows());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class DatasetRowEnumerator : IEnumerator<Row>
        {
            ScalarDataset _set;
            int _num_rows, position = -1;

            public DatasetRowEnumerator(ScalarDataset set, int num_rows)
            {
                _set = set;
                _num_rows = num_rows;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _num_rows);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Row Current
            {
                get
                {
                    try
                    {
                        return _set[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public void Dispose()
            {
                //do nutn.
            }
        }
    }
}
