using System;
using System.Collections.Generic;
using System.Linq;
using HDF5DotNet;

namespace HDF5Reader
{
    class CompoundDataset : Dataset
    {
        private List<Row> _rows;
        private Attribute[] _attributes;
        private byte[] _row_data;

        public CompoundDataset(Container container, string datasetname) : base(container, datasetname) { }

        protected override void LoadData()
        {
            _rows = new List<Row>();

            FindAllAttributes();
            LoadAllRows();
        }

        void FindAllAttributes()
        {
            var num_attrs = FindNumberOfAttributes();
            _attributes = new Attribute[num_attrs];

            for (long i = 0; i < num_attrs; i++)
            {
                FindAttribute(i);
            }
        }

        long FindNumberOfAttributes()
        {
            //return 20;
            //TODO: Make this actually work!
            throw new NotImplementedException();
        }

        void FindAttribute(long i)
        {
            var attr_field = "FIELD_" + i + "_NAME";

            var attr = H5A.open(Id, attr_field);
            var dtype = H5A.getType(attr);
            var size = H5T.getSize(dtype);

            var mtype = H5T.create(H5T.CreateClass.STRING, size);
            var buffer = new byte[size];
            H5A.read(attr, mtype, new H5Array<byte>(buffer));

            var attr_name = Encoding.GetString(buffer);

            _attributes[i] = new StringAttribute(attr_name, 32);
            //TODO: Figure out which type of attribute it should be!
            throw new NotImplementedException();
            Console.WriteLine("Attribute: " + _attributes[i]);
        }

        void LoadAllRows()
        {
            var num_rows = FindNumberOfRows();
            LoadRowData();

            for (long i = 0; i < num_rows; i++)
            {
                LoadRow(i);
            }
        }

        void LoadRowData()
        {
            var dtype = H5D.getType(Id);
            var size = H5T.getSize(dtype);

            _row_data = new byte[size];
            H5D.read(Id, dtype, new H5Array<byte>(_row_data));
            
            //TODO: Does this work with more than one row? Dunno. Probs not.
        }

        void LoadRow(long i)
        {
            //TODO: Make work with several rows!

            int ptr = 0;

            var row_data = new Dictionary<string, object>();

            foreach (var attr in _attributes)
            {
                row_data[attr.Name] = attr.Parse(_row_data.Skip(ptr).Take(attr.Length).ToArray());
                ptr += attr.Length;
            }

            _rows.Add(new Row(row_data));
        }

        public Row this[int i] {
            get
            {
                return _rows[i];
            }
        }
    }
}
