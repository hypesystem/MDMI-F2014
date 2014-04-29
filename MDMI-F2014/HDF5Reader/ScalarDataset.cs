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
            //Determine type and size of data
            var dtype = H5D.getType(Id);
            var size = H5T.getSize(dtype);

            var space = H5D.getSpace(Id);
            var dims = H5S.getSimpleExtentDims(space);

            var num_dimensions = dims.Length;

            if (num_dimensions != 1 && num_dimensions != 2)
                throw new ArgumentException("Scalar dataset has more than 2 dimensions! Cannot be handled.", "datasetname");

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

            if (num_dimensions == 1)
            {
                var _row_data = new byte[dims[0], size];
                H5D.read(Id, dtype, new H5Array<byte>(_row_data));

                //Parse rows
                for (int i = 0; i < dims[0]; i++)
                {
                    var dat = new Dictionary<string, object>();
                    dat["0"] = parser.Parse(_row_data.Field(i));
                    AddRow(new Row(dat));
                }
            }
            else if (num_dimensions == 2)
            {
                var _row_data = new byte[dims[0], dims[1], size];
                H5D.read(Id, dtype, new H5Array<byte>(_row_data));

                //Parse rows
                for (int i = 0; i < dims[0]; i++)
                {
                    var dat = new Dictionary<string, object>();
                    for (int j = 0; j < dims[1]; j++)
                        dat[""+i] = parser.Parse(_row_data.Field(i, j));
                    AddRow(new Row(dat));
                }
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
