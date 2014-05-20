using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class BellmanFordSP {

    private decimal[] distTo;               // distTo[v] = distance  of shortest s->v path
    private DirectedEdge[] edgeTo;         // edgeTo[v] = last edge on shortest s->v path
    private Boolean[] onQueue;             // onQueue[v] = is v currently on the queue?
    private Queue<Int32> queue;          // queue of vertices to relax
    private int cost;                      // number of calls to relax()
    private IEnumerable<DirectedEdge> cycle;  // negative cycle (or null if no such cycle)
        private static bool first = true;

    public BellmanFordSP(NeuronGraph G, int s) {
        distTo  = new decimal[G.V()];
        edgeTo  = new DirectedEdge[G.V()];
        onQueue = new Boolean[G.V()];
        for (int v = 0; v < G.V(); v++)
            distTo[v] = decimal.MaxValue;
        distTo[s] = 0.0M;

        // Bellman-Ford algorithm
        queue = new Queue<Int32>();
        queue.Enqueue(s);
        onQueue[s] = true;
        while (queue.Count != 0) {
            int v = queue.Dequeue();
           // Console.WriteLine("Relaxing edge {0}", v);

            onQueue[v] = false;
            relax(G, v);
        }

     
    }

    // relax vertex v and put other endpoints on queue if changed
    private void relax(NeuronGraph G, int v) {
        foreach(DirectedEdge e in G.adj(v)) {
            int w = e.to();
            decimal wdist = distTo[w];
            decimal vdist = distTo[v];
            if (distTo[w] > distTo[v] + e.weight()) {
             //   Console.WriteLine("LOL");
                distTo[w] = distTo[v] + e.weight();
                edgeTo[w] = e;
                if (w == 2000)
                {
                    Console.WriteLine(distTo[w]);
                }
                if (!onQueue[w]) {
                    queue.Enqueue(w);
                    onQueue[w] = true;
                }
            }
            if (cost++%G.V() == 0)
            {
               // Console.Write("Cost is {0} and we're hitting this, finding negative cycle", cost);
                findNegativeCycle();
            }
        }
    }

    // by finding a cycle in predecessor graph
    private void findNegativeCycle()
    {
        int V = edgeTo.Length;
        EdgeWeightedDigraph spt = new EdgeWeightedDigraph(V);
        for (int v = 0; v < V; v++)
            if (edgeTo[v] != null)
                spt.addEdge(edgeTo[v]);

        // EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(spt);
        //cycle = finder.cycle();
        if (first)
        {
            cycle = null;
            first = !first;
        }
        else
            cycle = new List<DirectedEdge>();
    }
    /**
     * Is there a negative cycle reachable from the source vertex <tt>s</tt>?
     * @return <tt>true</tt> if there is a negative cycle reachable from the
     *    source vertex <tt>s</tt>, and <tt>false</tt> otherwise
     */
    public Boolean hasNegativeCycle()
    {
        return cycle != null;
    }

    /**
     * Returns a negative cycle reachable from the source vertex <tt>s</tt>, or <tt>null</tt>
     * if there is no such cycle.
     * @return a negative cycle reachable from the soruce vertex <tt>s</tt> 
     *    as an iterable of edges, and <tt>null</tt> if there is no such cycle
     */
    public IEnumerable<DirectedEdge> negativeCycle() {
        return cycle;
    }

    /**
     * Returns the length of a shortest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>.
     * @param v the destination vertex
     * @return the length of a shortest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>;
     *    <tt>decimal.POSITIVE_INFINITY</tt> if no such path
     * @throws UnsupportedOperationException if there is a negative cost cycle reachable
     *    from the source vertex <tt>s</tt>
     */
    public decimal DistTo(int v) {
        if (hasNegativeCycle())
            throw new Exception("Negative cost cycle exists");
        return distTo[v];
    }

    /**
     * Is there a path from the source <tt>s</tt> to vertex <tt>v</tt>?
     * @param v the destination vertex
     * @return <tt>true</tt> if there is a path from the source vertex
     *    <tt>s</tt> to vertex <tt>v</tt>, and <tt>false</tt> otherwise
     */
    public Boolean HasPathTo(int v)
    {
        return distTo[v] < decimal.MaxValue;
    }

    /**
     * Returns a shortest path from the source <tt>s</tt> to vertex <tt>v</tt>.
     * @param v the destination vertex
     * @return a shortest path from the source <tt>s</tt> to vertex <tt>v</tt>
     *    as an iterable of edges, and <tt>null</tt> if no such path
     * @throws UnsupportedOperationException if there is a negative cost cycle reachable
     *    from the source vertex <tt>s</tt>
     */
    public IEnumerable<DirectedEdge> pathTo(int v) {
        if (hasNegativeCycle())
            throw new Exception("Negative cost cycle exists");
        if (!HasPathTo(v)) return null;
        Stack<DirectedEdge> path = new Stack<DirectedEdge>();
        for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.from()]) {
            path.Push(e);
        }
        return path;
    }


            
        }
    }


