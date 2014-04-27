using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    abstract class Attribute
    {
        public readonly Encoding Encoding = new ASCIIEncoding();

        public readonly string Name;
        public readonly int Length;

        public Attribute(string name, int length)
        {
            Name = name;
            Length = length;
        }

        public abstract object Parse(byte[] data);
    }
}
