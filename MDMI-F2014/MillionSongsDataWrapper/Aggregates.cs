namespace MillionSongsDataWrapper
{
    public class Aggregates
    {
        public readonly double Min;
        public readonly double Max;
        public readonly double Mean;
        public readonly double Median;
        public readonly double Variance;
        public readonly double Skewness;
        public readonly double ValueRange;
        public readonly double Kurtosis;

        public Aggregates(double min, double max, double mean, double median, 
            double variance, double valueRange, double skewness, double kurtosis)
        {
            Min = min;
            Max = max;
            Mean = mean;
            Median = median;
            Variance = variance;
            Skewness = skewness;
            ValueRange = valueRange;
            Kurtosis = kurtosis;
        }
    }
}
