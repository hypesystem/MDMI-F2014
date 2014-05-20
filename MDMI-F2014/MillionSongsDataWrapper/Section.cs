using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionSongsDataWrapper
{
    public class Section
    {
        public readonly double Confidence;
        public readonly double Duration;

        public Section(double confidence, double duration)
        {
            Confidence = confidence;
            Duration = duration;
        }
    }
}
