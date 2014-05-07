using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class DirectedEdge
    {
        readonly int _from, _to;
        readonly double _weight;

        public DirectedEdge(int from, int to, double weight)
        {
            _from = from;
            _to = to;
            _weight = weight;

        }

        public int from()
        {
            return _from;
        }

        public int to()
        {
            return _to;

        }

        public double weight()
        {
            return _weight;
        }
    }
}
