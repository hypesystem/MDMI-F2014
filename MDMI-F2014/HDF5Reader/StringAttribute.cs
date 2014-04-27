using System;
using HDF5DotNet;

namespace HDF5Reader
{
    class StringAttribute : Attribute
    {

        public StringAttribute(string name, long length) : base(name,length) { }

        public override object Parse(byte[] data)
        {
            return Encoding.GetString(data).Trim();
        }
    }
}
