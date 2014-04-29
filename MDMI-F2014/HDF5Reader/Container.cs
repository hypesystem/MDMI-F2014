using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace HDF5Reader
{
    abstract class Container
    {
        public abstract H5FileOrGroupId Id {
            get;
        }

        public Group GetGroup(string groupname)
        {
            return new Group(this, groupname);
        }

        public CompoundDataset GetCompoundDataset(string datasetname)
        {
            return new CompoundDataset(this, datasetname);
        }

        public ScalarDataset GetScalarDataset(string datasetname)
        {
            return new ScalarDataset(this, datasetname);
        }
    }
}
