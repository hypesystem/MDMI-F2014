namespace MillionSongsDataWrapper
{
    /*
     * Class representing one segment of a song, as documented by the echo nest framework. 
     */
    public class Segment
    {

        public readonly double Confidence;
        //the loudness at the beginning of the segment
        public readonly double LoudnessStart;
        //the time in ms from the start the max loudness occurs
        public readonly double LoudnessMaxTime;
        //max loudness
        public readonly double LoudnessMax;
        //duration of the segment in ms
        public readonly double Duration;
        //12 dimensional vector representing the chromatic pitch content of this segment
        public readonly double[] Pitches;
        //12 dimensional vector representing the sonic texture of this segment
        public readonly double[] Timbre;

        public Segment(double loudnessStart, double loudnessMaxTime, double loudnessMax, double duration, double[] pitches, double[] timbre, double confidence)
        {
            LoudnessMaxTime = loudnessMaxTime;
            LoudnessMax = loudnessMax;
            Duration = duration;
            Pitches = pitches;
            Timbre = timbre;
            Confidence = confidence;
            LoudnessStart = loudnessStart;
        }
    }
}
