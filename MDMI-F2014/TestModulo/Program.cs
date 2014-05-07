using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModulo
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Out.WriteLine(Graph.NeuronGraph.translate(4,i));
            }
        }
    }
}
