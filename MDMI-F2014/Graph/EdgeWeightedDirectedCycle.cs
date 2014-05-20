using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class EdgeWeightedDirectedCycle
    {
        private Boolean[] marked;             // marked[v] = has vertex v been marked?
        private DirectedEdge[] edgeTo;        // edgeTo[v] = previous edge on path to v
        private Boolean[] onStack;            // onStack[v] = is vertex on the stack?
        private Stack<DirectedEdge> _cycle;    // directed cycle (or null if no such cycle)
        private int cost = 0;
        /**
         * Determines whether the edge-weighted digraph <tt>G</tt> has a directed cycle and,
         * if so, finds such a cycle.
         * @param G the edge-weighted digraph
         */
        public EdgeWeightedDirectedCycle(EdgeWeightedDigraph G) {
            Console.Write("Graph size cycle finder {0}", G.V());
        marked  = new Boolean[G.V()];
        onStack = new Boolean[G.V()];
        edgeTo  = new DirectedEdge[G.V()];
        for (int v = 0; v < G.V(); v++)
            if (!marked[v]) dfs(G, v);

    }

        // check that algorithm computes either the topological order or finds a directed cycle
        private void dfs(EdgeWeightedDigraph G, int v) {
        // Console.Out.WriteLine(cost++);
        onStack[v] = true;
        marked[v] = true;
        foreach(DirectedEdge e in G.adj(v))
        {
            DirectedEdge resultEdge = e;
            int w = resultEdge.to();

            // short circuit if directed cycle found
            if (_cycle != null) return;

            //found new vertex, so recur
            else if (!marked[w]) {
                edgeTo[w] = resultEdge;
                dfs(G, w);
            }

            // trace back directed cycle
            else if (onStack[w]) {
                _cycle = new Stack<DirectedEdge>();
                while (e.from() != w) {
                    _cycle.Push(resultEdge);
                    resultEdge = edgeTo[e.from()];
                }
                _cycle.Push(resultEdge);
            }
        }

        onStack[v] = false;
    }

        /**
         * Does the edge-weighted digraph have a directed cycle?
         * @return <tt>true</tt> if the edge-weighted digraph has a directed cycle,
         * <tt>false</tt> otherwise
         */
        public Boolean hasCycle()
        {
            return _cycle != null;
        }

        /**
         * Returns a directed cycle if the edge-weighted digraph has a directed cycle,
         * and <tt>null</tt> otherwise.
         * @return a directed cycle (as an iterable) if the edge-weighted digraph
         *    has a directed cycle, and <tt>null</tt> otherwise
         */
        public IEnumerable<DirectedEdge> cycle()
        {
            return _cycle;
        }


        // certify that digraph is either acyclic or has a directed cycle
        private Boolean check(EdgeWeightedDigraph G) {

        // edge-weighted digraph is cyclic
        if (hasCycle()) {
            // verify cycle
            DirectedEdge first = null, last = null;
            foreach (DirectedEdge e in cycle()) {
                if (first == null) first = e;
                if (last != null) {
                    if (last.to() != e.from()) {
                        return false;
                    }
                }
                last = e;
            }

            if (last.to() != first.from()) {
                return false;
            }
        }


        return true;
    }
    }

}
