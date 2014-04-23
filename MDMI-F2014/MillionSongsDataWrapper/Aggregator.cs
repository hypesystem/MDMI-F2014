using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace MillionSongsDataWrapper
{
    static class Aggregator
    {
        static SegmentStats AggregateSegments(IEnumerable<Segment> segments)
        {
            var confidenceMean = segments.Average(s => s.Confidence);
            var loudnessStart = Aggregate(segments.Select(s => s.LoudnessStart));
            var loudnessmax = Aggregate(segments.Select(s => s.LoudnessMax));
            var loudnessMaxTime = Aggregate(segments.Select(s => s.LoudnessMaxTime));
            var duration = Aggregate(segments.Select(s => s.Duration));
            var pitch = Aggregate(segments.Select(s => s.Pitches));
            var timbre = Aggregate(segments.Select(s => s.Timbre));
            var result = new SegmentStats(confidenceMean, loudnessStart, loudnessMaxTime, loudnessmax, duration, pitch,
                timbre);
            return result;
        }

        static SectionStats AggregateSections(IEnumerable<Section> sections)
        {
            var confidenceMean = sections.Average(s => s.Confidence);
            var duration = Aggregate(sections.Select(s => s.Duration));
            var result = new SectionStats(confidenceMean, duration,sections.Count());
            return result;
        }

        private static Aggregates[] Aggregate(IEnumerable<double[]> doubles)
        {
            var result = new Aggregates[doubles.Count()];
            for (var i = 0; i < result.Count(); i++)
            {
                var i1 = i;
                result[i] = Aggregate(doubles.Select(d => d[i1]));
            }
            return result;
        }

        private static Aggregates Aggregate(IEnumerable<double> doubles)
        {
            var stats = new DescriptiveStatistics(doubles);
            var min = stats.Minimum;
            var max = stats.Maximum;
            var mean = stats.Mean;
            var median = doubles.Median();
            var variance = stats.Variance;
            var skewness = stats.Skewness;
            var kurtosis = stats.Kurtosis;
            var valueRange = max - min;
            var result = new Aggregates(min, max, mean, median, variance, valueRange, skewness, kurtosis);
            return result;
        }

    }
}
