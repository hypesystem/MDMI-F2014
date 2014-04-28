using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    public static class ByteMultiArrayExtension
    {
        public static byte[] Row(this byte[,] arr, int row)
        {
            var result = new byte[arr.GetLongLength(1)];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arr[row, i];
            }
            return result;
        }

        public static byte[] Column(this byte[,] arr, int col)
        {
            var result = new byte[arr.GetLongLength(0)];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arr[i, col];
            }
            return result;
        }
    }
}
