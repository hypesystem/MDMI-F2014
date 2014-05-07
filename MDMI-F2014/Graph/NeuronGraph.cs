using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Graph
{
    public class NeuronGraph
    {
       

        private List<DirectedEdge>[] _adjacencyList;
        private Dictionary<Int32, double[]> _prototypeVectors;
        private int _V; // No of Vertices
        private int _rowLength;

        public NeuronGraph(double[,,] inputGrid)
        {
            MapNeuronToVertex(inputGrid);
            ConstructEdgesFromVertices();
        }

        public List<DirectedEdge> adj(int V)
        {
            return _adjacencyList[V];
        }

        public int V()
        {
            return _V;
        }

        private void ConstructEdgesFromVertices()
        {
            _adjacencyList = new List<DirectedEdge>[_V];

            for (int i = 0; i < _V; i++)
            {
                _adjacencyList[i] = new List<DirectedEdge>(4);
            }

            for (int i = 0; i < _V; i++)
            {
                int left = i - 1;
                int right = i + 1;
                int top = i - _rowLength;
                int bot = i + _rowLength;

                if (left%_rowLength == 0) left += _rowLength;
                if (right%_rowLength != 0) right -= _rowLength;
                if (top < 0) top += _V;
                if (bot < _V) bot -= _V;

                addEdge(i, left);
                addEdge(i, right);
                addEdge(i, top);
                addEdge(i, bot);
            }
        }

        private void addEdge(int one, int other)
        {
            double dist = EuclideanDistance.Distance(_prototypeVectors[one], _prototypeVectors[other]);
            _adjacencyList[one].Add(new DirectedEdge(one, other, dist));
            _adjacencyList[other].Add(new DirectedEdge(other,one, dist));
        }


        private void MapNeuronToVertex(double[,,] inputGrid)
        {
            _prototypeVectors = new Dictionary<int, double[]>();
            _rowLength = inputGrid.GetLength(0);

            for (int i = 0; i < _rowLength; i++)
            {
                for (int j = 0; j < inputGrid.GetLength(1); j++)
                {
                    double[] _vector = new double[inputGrid.GetLength(2)];
                    for (int k = 0; k < _vector.Length; k++)
                    {
                        _vector[k] = inputGrid[i, j, k];
                    }
                    
                    _prototypeVectors[_V++] = _vector;

                }
            }
        }

        /// <summary>
        /// Rows and cols and KV is the k'th vertex
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="kv"></param>
        /// <returns></returns>
        public static Tuple<int,int> translate(int col, int kv)
        {
            int x = kv/col;
            int y = kv - (x*col); 

            return new Tuple<int, int>(x, y);
        }

        public static int translate(Tuple<int, int> xy, int col)
        {
            return (xy.Item1*col) + xy.Item2;
        }
    }
}
