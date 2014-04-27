using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    class Row
    {
        private Dictionary<string, object> _data;

        public Row(Dictionary<string, object> data)
        {
            _data = data;
        }

        public string GetString(string fieldname)
        {
            object obj;
            if (_data.TryGetValue(fieldname, out obj))
            {
                if(obj is string) {
                    return (string)obj;
                }
                throw new ArgumentException("The requested field is not a string.", "fieldname");
            }
            throw new ArgumentException("The requested field does not exist.", "fieldname");
        }
    }
}
