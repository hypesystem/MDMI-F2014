using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class EuclideanDistance
    {

        public static double Distance(double[] x, double[] y)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += Math.Pow((x[i] - y[i]), 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
