using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    class ScalarDataset : Dataset, IEnumerable<Row>
    {
        public ScalarDataset(Container container, string datasetname) : base(container, datasetname) { }

        protected override void LoadData()
        {
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
