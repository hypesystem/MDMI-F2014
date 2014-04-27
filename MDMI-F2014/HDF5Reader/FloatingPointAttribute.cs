using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    class FloatingPointAttribute : Attribute
    {
        public FloatingPointAttribute(string name, int length) : base(name, length) { }

        public override object Parse(byte[] data)
        {
            if (Length == 32) //float
                throw new NotImplementedException();
            else if (Length == 64) //double
                throw new NotImplementedException();
            throw new NotImplementedException();
        }
    }
}
