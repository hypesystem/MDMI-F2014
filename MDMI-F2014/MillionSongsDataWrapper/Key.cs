using System;
using MillionSongsDataWrapper;

namespace MillionSongsDataWrapper
{
    public enum Key
    {
        Cmaj, Cmin, CsharpMaj, CsharpMin,
        Dmaj, Dmin, EflatMaj, EflatMin,
        Emaj, Emin, Fmaj, Fmin,
        FsharpMaj, FsharpMin, Gmaj,
        Gmin, AflatMaj, AflatMin, Amaj,
        Amin, BflatMaj, Bflatmin, Bmaj, Bmin
    }

    public static class KeyExtensions
    {

        private static bool Compare<T1, T2>(this Tuple<T1, T2> value, T1 first, T2 second)
        {
            return value.Item1.Equals(first) && value.Item2.Equals(second);
        }

        public static Key FromInt(this Key k, int key, int mode)
        {
            var t = new Tuple<int,int>(key, mode);
            if (t.Compare(0, 0)) return Key.Cmin;
            if (t.Compare(0, 1)) return Key.Cmaj;
            if (t.Compare(1, 0)) return Key.CsharpMin;
            if (t.Compare(1, 1)) return Key.CsharpMaj;
            if (t.Compare(2, 0)) return Key.Dmin;
            if (t.Compare(2, 1)) return Key.Dmaj;
            if (t.Compare(3, 0)) return Key.EflatMin;
            if (t.Compare(3, 1)) return Key.EflatMaj;
            if (t.Compare(4, 0)) return Key.Emin;
            if (t.Compare(4, 1)) return Key.Emaj;
            if (t.Compare(5, 0)) return Key.Fmin;
            if (t.Compare(5, 1)) return Key.Fmaj;
            if (t.Compare(6, 0)) return Key.FsharpMin;
            if (t.Compare(6, 1)) return Key.FsharpMaj;
            if (t.Compare(7, 0)) return Key.Gmin;
            if (t.Compare(7, 1)) return Key.Gmaj;
            if (t.Compare(8, 0)) return Key.AflatMin;
            if (t.Compare(8, 1)) return Key.AflatMaj;
            if (t.Compare(9, 0)) return Key.Amin;
            if (t.Compare(9, 1)) return Key.Amaj;
            if (t.Compare(10, 0)) return Key.Bflatmin;
            if (t.Compare(10, 1)) return Key.BflatMaj;
            if (t.Compare(11, 0)) return Key.Bmin;
            if (t.Compare(11, 1)) return Key.Bmaj;
            else
            {
                throw new ArgumentException("key-mode combination not recognized");
            }
        }
    }
}
