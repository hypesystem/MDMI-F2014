using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace ESOMDataWrapper
{
    
    public class ESOM
    {
        
        //Data[i,j,k] is the k'th property of the neuron at position (i,j) in the SOM
        public readonly double[,,] Data;

        public ESOM(double[,,] attributes)
        {
            Data = attributes;
        }

        public static ESOM Fromfile(string fileName)
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    const string klRegexStr = @"\% ?([0-9]+ ?){2}$";
                    const string mRegexStr = @"\% ?([0-9]+)$";
                    var klRegex = new Regex(klRegexStr);
                    var headerRead = false;
                    var mRegex = new Regex(mRegexStr);
                    var k = -1;
                    var l = -1;
                    var m = -1;
                    while (!headerRead)
                    {
                        var line = sr.ReadLine();
                        if (line.Contains("#")) continue;
                        if (klRegex.IsMatch(line))
                        {
                            var kl = line.Replace("%", "").Split(new[] {' '});
                            k = int.Parse(kl[0]);
                            l = int.Parse(kl[1]);
                            continue;
                        }
                        if (!mRegex.IsMatch(line)) continue;
                        m = int.Parse(line.Replace("%", "").Split(new[] {' '})[0]);
                        headerRead = true;
                    }
                    var data = sr.ReadToEnd();
                    var som = new double[k,l,m];
                    var lines = data.Split(new[] {'\n'});
                    var i = 0;
                    var j = 0;
                    foreach (var line in lines)
                    {
                        var n = 0;
                        var attributes = line.Split(new[] {'\t'});
                        foreach (var attribute in attributes)
                        {
                            som[i, j, n] = double.Parse(attribute);
                            n++;
                        }
                        j++;
                        if (j == l) i++;
                    }
                    return new ESOM(som);
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
