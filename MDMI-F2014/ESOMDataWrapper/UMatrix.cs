using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ESOMDataWrapper
{
    public class UMatrix
    {
        public readonly double[,,] Heights;

        public UMatrix(double[,,] heights)
        {
            Heights = heights;
        }

        public static UMatrix FromFile(string fileName)
        {
            
                using (var sr = new StreamReader(fileName))
                {
                    const string klRegexStr = @"\% ?([0-9]+ ?){2}$";
                    var klRegex = new Regex(klRegexStr);
                    var headerRead = false;
                    var k = -1;
                    var l = -1;
                    while (!headerRead)
                    {
                        var line = sr.ReadLine();
                        if (!klRegex.IsMatch(line)) continue;
                        var kl = line.Replace("%", "").Split(new[] {' '});
                        k = int.Parse(kl[0]);
                        l = int.Parse(kl[1]);
                        headerRead = true;
                    }
                    var heights = new double[k, l, 1];
                    var data = sr.ReadToEnd();
                    var lines = data.Split(new[] {'\n'});
                    var i = 0;
                    var j = 0;
                    foreach (var height in lines.Select(line => line.Split(new[] {'\t'})))
                    {
                        j = 0;
                        foreach (var h in height)
                        {
                            if (h.Equals("")) continue;
                            heights[i, j, 0] = double.Parse(h);
                            j++;
                        }
                        i++;
                    }
                    return new UMatrix(heights);
                }
            
            
            return null;
        }
    }
}