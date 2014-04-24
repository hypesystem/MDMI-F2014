using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ESOMDataWrapper
{
    internal class UMatrix
    {
        public readonly double[,] Heights;

        public UMatrix(double[,] heights)
        {
            Heights = heights;
        }

        public static UMatrix FromFile(string fileName)
        {
            try
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
                    var heights = new double[k, l];
                    var data = sr.ReadToEnd();
                    var lines = data.Split(new[] {'\n'});
                    var i = 0;
                    var j = 0;
                    foreach (var height in lines.Select(line => line.Split(new[] {'\t'})))
                    {
                        foreach (var h in height)
                        {
                            heights[i, j] = double.Parse(h);
                        }
                        j++;
                        if (j == l) i++;
                    }
                    return new UMatrix(heights);
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