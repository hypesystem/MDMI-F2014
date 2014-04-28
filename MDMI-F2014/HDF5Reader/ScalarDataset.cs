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
            //Load data
            var dtype = H5D.getType(Id);
            var size = H5T.getSize(dtype);

            byte[,] _row_data = new byte[FindNumberOfRows(),size];
            H5D.read(Id, dtype, new H5Array<byte>(_row_data));

            //Find data type+size
            var member_type = H5T.getClass(dtype).ToString();
            var member_size = size;

            //Setup parser
            Attribute parser = null;

            switch (member_type)
            {
                case "STRING":
                    parser = new StringAttribute("data", member_size);
                    break;
                case "INTEGER":
                    parser = new IntegerAttribute("data", member_size);
                    break;
                case "FLOAT":
                    parser = new FloatingPointAttribute("data", member_size);
                    break;
                default:
                    throw new ArgumentException("Unsupported member type " + member_type, "member_type");
            }

            //Parse rows
            for (int i = 0; i < FindNumberOfRows(); i++)
            {
                var dat = new Dictionary<string, object>();
                dat[parser.Name] = parser.Parse(_row_data.Row(i));
                AddRow(new Row(dat));
            }
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
