using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ESOMDataWrapper
{
    public class PlaylistPathFinder
    {
        //index -> row,col
        public readonly Dictionary<int, Tuple<int, int>> Index2BestMatch;
        //row,col -> index
        public readonly Dictionary<Tuple<int, int>, int> BestMatch2Index;

        public PlaylistPathFinder(Dictionary<int, Tuple<int, int>> index2Bestmatches, Dictionary<Tuple<int, int>, int> bestMatch2Index)
        {
            Index2BestMatch = index2Bestmatches;
            BestMatch2Index = bestMatch2Index;
        }

        public static PlaylistPathFinder FromFile(string fileName)
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    const string klRegexStr = @"\% ?([0-9]+ ?){2}$";
                    const string mRegexStr = @"\% ?([0-9]+)$";
                    var l = sr.ReadLine();
                    while (l != null && (l.Contains("#") || l.Contains("%")))
                    {
                        l = sr.ReadLine();
                    }

                    var i2bm = new Dictionary<int, Tuple<int, int>>();
                    var bm2i = new Dictionary<Tuple<int, int>, int>();

                    var data = sr.ReadToEnd();
                    var lines = data.Split(new[] {'\n'});
                    foreach (var line in lines)
                    {
                        var d = line.Split(new[] {'\t'});
                        var n = int.Parse(d[0]);
                        var row = int.Parse(d[1]);
                        var col = int.Parse(d[2]);
                        i2bm.Add(n,new Tuple<int, int>(row,col));
                        bm2i.Add(new Tuple<int, int>(row,col),n);
                    }
                    return new PlaylistPathFinder(i2bm,bm2i);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File could not be read");
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
