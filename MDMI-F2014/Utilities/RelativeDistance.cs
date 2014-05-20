using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class RelativeDistance
    {
        public static double Distance(double[] from, double[] to)
        {
            double acc = 0.0;
            
            for (int i = 0; i < from.Length; i++)
            {
                acc += (to[i] - from[i]);
            }
            return acc;
        }
    }
}
