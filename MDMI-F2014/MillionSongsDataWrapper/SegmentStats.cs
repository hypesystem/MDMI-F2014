using System.Collections.Generic;

namespace MillionSongsDataWrapper
{
    public class SegmentStats
    {
        public readonly double ConfidenceMean;
        public readonly Aggregates LoudnessStart;
        public readonly Aggregates LoudnessMaxTime;
        public readonly Aggregates LoudnessMax;
        public readonly Aggregates Duration;
        public readonly Aggregates[] Pitches;
        public readonly Aggregates[] Timbre;

        public SegmentStats(double confidenceMean, Aggregates loudnessStart, Aggregates loudnessMaxTime, 
            Aggregates loudnessMax, Aggregates duration, Aggregates[] pitches, Aggregates[] timbre)
        {
            ConfidenceMean = confidenceMean;
            LoudnessStart = loudnessStart;
            LoudnessMaxTime = loudnessMaxTime;
            LoudnessMax = loudnessMax;
            Duration = duration;
            Pitches = pitches;
            Timbre = timbre;
        }

        private List<Aggregates> _aggregatesList;

        public List<Aggregates> AggregatesList
        {
            get
            {
                if (_aggregatesList == null)
                {
                _aggregatesList = new List<Aggregates>();
                _aggregatesList.Add(LoudnessStart);
                _aggregatesList.Add(LoudnessMax);
                _aggregatesList.Add(LoudnessMaxTime);
                _aggregatesList.Add(Duration);
                _aggregatesList.AddRange(Pitches);
                _aggregatesList.AddRange(Timbre);
                }
                return _aggregatesList;
            }
        }


    }
}
