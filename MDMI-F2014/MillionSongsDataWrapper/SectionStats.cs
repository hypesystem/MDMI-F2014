namespace MillionSongsDataWrapper
{
    public class SectionStats
    {
        public readonly double ConfidenceMean;
        public readonly Aggregates Duration;
        public readonly int Count;

        public SectionStats()
        {
            Duration = new Aggregates();
        }

        public double[] DistanceArray()
        {
            return Duration.PropertyValuesList.ToArray();
        }

        public SectionStats(double confidenceMean, Aggregates duration, int count)
        {
            ConfidenceMean = confidenceMean;
            Duration = duration;
            Count = count;
        }
    }
}
