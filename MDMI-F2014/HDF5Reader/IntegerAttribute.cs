using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    class IntegerAttribute : Attribute
    {
        public IntegerAttribute(string name, int length) : base(name, length) {
            if(length != 64 && length != 32)
                throw new ArgumentException("Unsupported length of integer: " + Length + " bits", "Length");
        }

        public override object Parse(byte[] data)
        {
            if (Length == 64)
                return BitConverter.ToInt64(data,0);
            else if (Length == 32)
                return BitConverter.ToInt32(data,0);
            throw new NotImplementedException();
        }
    }
}
