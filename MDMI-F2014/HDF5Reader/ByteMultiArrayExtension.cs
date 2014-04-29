using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDF5Reader
{
    public static class ByteMultiArrayExtension
    {
        public static byte[] Field(this byte[,] arr, int row)
        {
            var result = new byte[arr.GetLongLength(1)];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arr[row, i];
            }
            return result;
        }

        public static byte[] Field(this byte[,,] arr, int row, int col)
        {
            var result = new byte[arr.GetLongLength(2)];
            for (int i = 0; i < arr.GetLongLength(2); i++)
            {
                    result[i] = arr[row, col, i];
            }
            return result;
        }
    }
}
