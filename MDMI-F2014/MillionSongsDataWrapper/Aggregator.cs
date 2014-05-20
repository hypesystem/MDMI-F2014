using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace MillionSongsDataWrapper
{
    public static class Aggregator
    {
        public static SegmentStats AggregateSegments(IEnumerable<Segment> segments)
        {
            var enumerable = segments as Segment[] ?? segments.ToArray();
            var confidenceMean = enumerable.Average(s => s.Confidence);
            var loudnessStart = Aggregate(enumerable.Select(s => s.LoudnessStart));
            var loudnessmax = Aggregate(enumerable.Select(s => s.LoudnessMax));
            var loudnessMaxTime = Aggregate(enumerable.Select(s => s.LoudnessMaxTime));
            var duration = Aggregate(enumerable.Select(s => s.Duration));
            var pitch = Aggregate(enumerable.Select(s => s.Pitches));
            var timbre = Aggregate(enumerable.Select(s => s.Timbre));
            var result = new SegmentStats(confidenceMean, loudnessStart, loudnessMaxTime, loudnessmax, duration, pitch,
                timbre);
            return result;
        }

        public static SectionStats AggregateSections(IEnumerable<Section> sections)
        {
            var enumerable = sections as Section[] ?? sections.ToArray();
            var confidenceMean = enumerable.Average(s => s.Confidence);
            var duration = Aggregate(enumerable.Select(s => s.Duration));
            var result = new SectionStats(confidenceMean, duration,enumerable.Count());
            return result;
        }

        private static Aggregates[] Aggregate(IEnumerable<double[]> doubles)
        {
            var segments = doubles.ToArray();
            var num_cols = segments[0].Length;
            var result = new Aggregates[num_cols];

            for (int j = 0; j < num_cols; j++)
            {
                var col = new double[segments.Length];
                for (int i = 0; i < segments.Length; i++)
                {
                    col[i] = segments[i][j];
                }
                result[j] = Aggregate(col);
            }

            return result;
        }

        private static Aggregates Aggregate(IEnumerable<double> doubles)
        {
            var enumerable = doubles as double[] ?? doubles.ToArray();
            var stats = new DescriptiveStatistics(enumerable);
            var min = stats.Minimum;
            var max = stats.Maximum;
            var mean = stats.Mean;
            var median = enumerable.Median();
            var variance = stats.Variance;
            var skewness = stats.Skewness;
            var kurtosis = stats.Kurtosis;
            var valueRange = max - min;
            var result = new Aggregates(min, max, mean, median, variance, valueRange, skewness, kurtosis);
            return result;
        }

    }
}
