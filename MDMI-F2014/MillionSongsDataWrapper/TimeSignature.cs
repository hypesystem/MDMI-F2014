namespace MillionSongsDataWrapper
{
    public enum TimeSignature
    {
        OneFourth, TwoFourths, ThreeFourths, FourFourths, FiveFourths, SixFourths, SevenFourths, Complex
    }

    public static class TimeSignatureExtension
    {
        public static TimeSignature FromInt(int timeSignature)
        {
            switch (timeSignature)
            {
                case 1:
                    return TimeSignature.OneFourth;
                case 2:
                    return TimeSignature.TwoFourths;
                case 3:
                    return TimeSignature.ThreeFourths;
                case 4:
                    return TimeSignature.FourFourths;
                case 5:
                    return TimeSignature.FiveFourths;
                case 6:
                    return TimeSignature.SixFourths;
                case 7:
                    return TimeSignature.SevenFourths;
            }
            return TimeSignature.Complex;
        }
    }
}
