using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using MillionSongsDataWrapper;
using Utilities;

namespace BestMatchKNN
{
    public class KNNBestMatches
    {
        public static List<Song> FindKBestMatches(Song[] songs, List<int> dataSet, Song source, int k)
        {
       
            var pq = new SortedList<double, int>();
            List<Song> returnList = new List<Song>();
     
            foreach (var dataInt in dataSet)
            {
                double dist = EuclideanDistance.Distance(source, songs[dataInt]);
                pq.Add(dist, dataInt);
            }

            foreach (var demInts in pq.Values.Take(k).ToList())
            {
                returnList.Add(songs[demInts]);
            }

            return returnList;


        }
    }
}
