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

            var bestMatches = ESOMDataWrapper.BestMatches.FromFile("bestmatches1.bm");
            var ESOM = ESOMDataWrapper.ESOM.Fromfile("esom1.wts");

            var graph = new NeuronGraph(ESOM.Data);

            int from = 23;
            var fromCoord = bestMatches.Index2BestMatch[from];
            int translatefrom = graph.translate(fromCoord);

            var shortestPath = new ShortestNeuronPath(graph, translatefrom);

            var tocoord = bestMatches.Index2BestMatch[296];
            var to = graph.translate(tocoord);

            var result = shortestPath.HasPathTo(to) ? shortestPath.pathTo(to) : null;

            Song source = songs[from];

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
                    
                
                    if(next.Count >= 1)
                    {
                    foreach (var resultint in bestMatches.BestMatch2Index[coord])
                    {
                        Console.Out.WriteLine("ID: {0} and song name: {1}, artist: {2}", resultint,
                        songs[resultint].TrackName, songs[resultint].ArtistName);
                    }
                    }

                }
                else Console.Out.WriteLine("Skipping a neuron");
            }
            
            
            

        }
    }
}
