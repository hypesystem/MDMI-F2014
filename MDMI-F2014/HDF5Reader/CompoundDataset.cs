using System;
using System.Collections.Generic;
using System.Linq;
using HDF5DotNet;

namespace HDF5Reader
{
    class CompoundDataset : Dataset
    {
        private Attribute[] _attributes;
        private byte[,] _row_data;

        public CompoundDataset(Container container, string datasetname) : base(container, datasetname) { }

        protected override void LoadData()
        {
            FindAllAttributes();
            LoadAllRows();
        }

        void FindAllAttributes()
        {
            var num_attrs = FindNumberOfAttributes();
            _attributes = new Attribute[num_attrs];

            for (int i = 0; i < num_attrs; i++)
            {
                FindAttribute(i);
            }
        }

        long FindNumberOfAttributes()
        {
            return H5T.getNMembers(H5D.getType(Id));
        }

        void FindAttribute(int i)
        {
            var attr_field = "FIELD_" + i + "_NAME";

            var attr = H5A.open(Id, attr_field);
            var dtype = H5A.getType(attr);
            var size = H5T.getSize(dtype);

            var mtype = H5T.create(H5T.CreateClass.STRING, size);
            var buffer = new byte[size];
            H5A.read(attr, mtype, new H5Array<byte>(buffer));

            var attr_datatype = H5T.getMemberType(H5D.getType(Id), i);
            var attr_size = H5T.getSize(attr_datatype);

            var attr_class = H5T.getMemberClass(H5D.getType(Id), i).ToString();
            var attr_name = Encoding.GetString(buffer).Replace('\0', ' ').Trim();

            switch (attr_class)
            {
                case "STRING":
                    _attributes[i] = new StringAttribute(attr_name, attr_size);
                    break;
                case "INTEGER":
                    _attributes[i] = new IntegerAttribute(attr_name, attr_size);
                    break;
                case "FLOAT":
                    _attributes[i] = new FloatingPointAttribute(attr_name, attr_size);
                    break;
                default:
                    throw new ArgumentException("Unknown attribute type " + attr_class, "attr_type");
            }
        }

        void LoadAllRows()
        {
            var num_rows = FindNumberOfRows();
            LoadRowData();

            for (int i = 0; i < num_rows; i++)
            {
                LoadRow(i);
            }
        }

        void LoadRowData()
        {
            var dtype = H5D.getType(Id);
            var size = H5T.getSize(dtype);

            _row_data = new byte[FindNumberOfRows(),size];
            H5D.read(Id, dtype, new H5Array<byte>(_row_data));
            
            //TODO: Does this work with more than one row? Dunno. Probs not.
        }

        void LoadRow(int i)
        {
            //TODO: Make work with several rows!

            int ptr = 0;

            var row_data = new Dictionary<string, object>();

            foreach (var attr in _attributes)
            {
                row_data[attr.Name] = attr.Parse(_row_data.Field(0).Skip(ptr).Take(attr.Length).ToArray());
                ptr += attr.Length;
            }

            AddRow(new Row(row_data));
        }
    }
}
