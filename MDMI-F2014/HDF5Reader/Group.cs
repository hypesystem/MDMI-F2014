using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace HDF5Reader
{
    class Group : Container, IDisposable
    {
        private string _groupname;
        private H5GroupId _group_id;

        public Group(Container container, string groupname)
        {
            _groupname = groupname;
            _group_id = H5G.open((H5LocId)container.Id, groupname);
        }

        public override H5FileOrGroupId Id
        {
            get
            {
                return _group_id;
            }
        }

        public void Dispose()
        {
            H5G.close(_group_id);
        }
    }
}
