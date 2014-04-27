using System;

namespace HDF5Reader
{
    class FloatingPointAttribute : Attribute
    {
        public FloatingPointAttribute(string name, int length) : base(name, length) { }

        public override object Parse(byte[] data)
        {
            if (Length == 64) //double
                return BitConverter.ToDouble(data,0);
            throw new NotImplementedException();
        }
    }
}
