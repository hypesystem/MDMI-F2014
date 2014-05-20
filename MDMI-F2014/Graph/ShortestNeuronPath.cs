using System;
using System.Collections.Generic;

namespace Graph
{
    public class ShortestNeuronPath
    {
        private readonly decimal[] _distTo; // distTo[v] = distance  of shortest s->v path
        private readonly DirectedEdge[] edgeTo; // edgeTo[v] = last edge on shortest s->v path
        private readonly IndexMinPQ<decimal> pq; // priority queue of vertices

        /**
     * Computes a shortest paths tree from <tt>s</tt> to every other vertex in
     * the edge-weighted digraph <tt>G</tt>.
     * @param G the edge-weighted digraph
     * @param s the source vertex
     * @throws IllegalArgumentException if an edge weight is negative
     * @throws IllegalArgumentException unless 0 &le; <tt>s</tt> &le; <tt>V</tt> - 1
     */

        public ShortestNeuronPath(NeuronGraph G, int s)
        {
            _distTo = new decimal[G.V()];
            edgeTo = new DirectedEdge[G.V()];
            for (int v = 0; v < G.V(); v++)
                _distTo[v] = decimal.MaxValue;
            _distTo[s] = 0.0M;

            // relax vertices in order of distance from s
            pq = new IndexMinPQ<decimal>(G.V());
            pq.Insert(s, _distTo[s]);
            while (!pq.IsEmpty())
            {
                int v = pq.DelMin();
                foreach (DirectedEdge e in G.adj(v))
                    relax(e);
            }
        }

        // relax edge e and update pq if changed
        private void relax(DirectedEdge e)
        {
            int v = e.from(), w = e.to();
            if (_distTo[w] > _distTo[v] + e.weight())
            {
                _distTo[w] = _distTo[v] + e.weight();
                edgeTo[w] = e;
                if (pq.Contains(w)) pq.DecreaseKey(w, _distTo[w]);
                else pq.Insert(w, _distTo[w]);
            }
        }

        public IEnumerable<DirectedEdge> pathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<DirectedEdge>();
            for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.from()])
            {
                path.Push(e);
            }
            return path;
        }


        public bool HasPathTo(int V)
        {
            return _distTo[V] < decimal.MaxValue;
        }
    }
}