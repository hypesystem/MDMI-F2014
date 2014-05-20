using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using LRNWriter;
using MillionSongsDataWrapper;

namespace BestMatchKNN
{
    public class Program
    {
        static void Main(String[] args)
        {
            var reader = new LRNReader("data.lrn");
            var songs = reader.ReadSongs().ToArray();
            Console.WriteLine("LRN file read...");
            var bestMatches = ESOMDataWrapper.BestMatches.FromFile("bigmap.bm");
            Console.WriteLine("BM file read...");
            // var ESOM = ESOMDataWrapper.ESOM.Fromfile("bigmap.wts");
            var UMat = ESOMDataWrapper.UMatrix.FromFile("bigmap.umx");
            Console.WriteLine("UMatrix file read...");
            var graph = new NeuronGraph(UMat.Heights, 1);
            Console.WriteLine("Built read...");
            int from = 23;
            var fromCoord = bestMatches.Index2BestMatch[from];
            int translatefrom = graph.translate(fromCoord);

           // var shortestPath = new ShortestNeuronPath(graph, translatefrom);
            var shortestPath = new BellmanFordSP(graph, translatefrom);
            Console.Write("Bellman Ford completed...");
            var tocoord = bestMatches.Index2BestMatch[296];
            var to = graph.translate(tocoord);
            // Console.Write(shortestPath.HasPathTo(to));
            var result = shortestPath.HasPathTo(to) ? shortestPath.pathTo(to) : null;

            Song source = songs[from];

            double accumulatedDistance = 0.0;

            List<Song> resultSongs = new List<Song>();

            foreach (var directedEdge in result)
            {
                var coord = graph.translate(directedEdge.@from());
                // Console.Out.Write(directedEdge.@from());
                if (bestMatches.BestMatch2Index.ContainsKey(coord))
                {

                    //var resultint = bestMatches.BestMatch2Index[coord].First();
                    //Console.Out.WriteLine("Length on BM list:" + bestMatches.BestMatch2Index[coord].Count);
                    var next = KNNBestMatches.FindKBestMatches(songs, bestMatches.BestMatch2Index[coord], source, 1);
                    source = next.FirstOrDefault() ?? source;
                    resultSongs.Add(source);
                    if (next.Count >= 1)
                    {
                        foreach (var resultint in bestMatches.BestMatch2Index[coord])
                        {
                            Console.Out.WriteLine("ID: {0} and song name: {1}, artist: {2}", resultint,
                            songs[resultint].TrackName, songs[resultint].ArtistName);
                        }
                    }

                }
                
            }
            for (int i = 0; i < resultSongs.Count - 1; i++)
            {
                accumulatedDistance += Utilities.EuclideanDistance.Distance(resultSongs[i], resultSongs[i + 1]);
            }
            var avgdist = accumulatedDistance / resultSongs.Count;
            Console.Out.WriteLine("Total euclidean distance: {0} and average distance: {1}", accumulatedDistance, avgdist);
            
            

        }
    }
}
