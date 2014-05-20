using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class EdgeWeightedDigraph {

    private int _V;
    private int _E;
    private List<DirectedEdge>[] _adj;
    
    /**
     * Initializes an empty edge-weighted digraph with <tt>V</tt> vertices and 0 edges.
     * param V the number of vertices
     * @throws java.lang.IllegalArgumentException if <tt>V</tt> < 0
     */
    public EdgeWeightedDigraph(int V) {
        if (V < 0) throw new Exception("Number of vertices in a Digraph must be nonnegative");
        _V = V;
        _E = 0;
        _adj = (List<DirectedEdge>[]) new List<DirectedEdge>[V];
        for (int v = 0; v < V; v++)
            _adj[v] = new List<DirectedEdge>();
    }


    /**
     * Returns the number of vertices in the edge-weighted digraph.
     * @return the number of vertices in the edge-weighted digraph
     */
    public Int32 V() {
        return _V;
    }

    /**
     * Returns the number of edges in the edge-weighted digraph.
     * @return the number of edges in the edge-weighted digraph
     */
    public Int32 E() {
        return _E;
    }

    /**
     * Adds the directed edge <tt>e</tt> to the edge-weighted digraph.
     * @param e the edge
     */
    public void addEdge(DirectedEdge e) {
        int v = e.from();
        _adj[v].Add(e);
        _E++;
    }


    /**
     * Returns the directed edges incident from vertex <tt>v</tt>.
     * @return the directed edges incident from vertex <tt>v</tt> as an Iterable
     * @param v the vertex
     * @throws java.lang.IndexOutOfBoundsException unless 0 <= v < V
     */
    public IEnumerable<DirectedEdge> adj(int v) {
        if (v < 0 || v >= _V) throw new Exception("vertex " + v + " is not between 0 and " + (_V-1));
        return _adj[v];
    }

    /**
     * Returns all directed edges in the edge-weighted digraph.
     * To iterate over the edges in the edge-weighted graph, use foreach notation:
     * <tt>for (DirectedEdge e : G.edges())</tt>.
     * @return all edges in the edge-weighted graph as an Iterable.
     */
    public IEnumerable<DirectedEdge> edges() {
        List<DirectedEdge> list = new List<DirectedEdge>();
        for (int v = 0; v < _V; v++) {
            foreach(DirectedEdge e in adj(v)) {
                list.Add(e);
            }
        }
        return list;
    } 

    /**
     * Returns the number of directed edges incident from vertex <tt>v</tt>.
     * This is known as the <em>outdegree</em> of vertex <tt>v</tt>.
     * @return the number of directed edges incident from vertex <tt>v</tt>
     * @param v the vertex
     * @throws java.lang.IndexOutOfBoundsException unless 0 <= v < V
     */
    public int outdegree(int v) {
        if (v < 0 || v >= _V) throw new Exception("vertex " + v + " is not between 0 and " + (_V-1));
        return _adj[v].Count;
    }

}



}
