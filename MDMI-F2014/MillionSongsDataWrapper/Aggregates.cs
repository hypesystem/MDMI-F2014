using System;
using System.Collections.Generic;
using System.Linq;

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

        public Aggregates(double min = 0d, double max = 0d, double mean = 0d, double median = 0d,
            double variance = 0d, double valueRange = 0d, double skewness = 0d, double kurtosis = 0d)
        {
            Min = min;
            Max = max;
            Mean = mean;
            Median = median;
            Variance = variance;
            Skewness = skewness;
            ValueRange = valueRange;
            Kurtosis = kurtosis;

            AggregateCount = 7;
        }

        public int AggregateCount { get; private set; }

        private static List<String> _propertyList;

        public static List<String> GetPropertyNameList()
        {
            if (_propertyList == null)
            {
                _propertyList = new List<string>();

                _propertyList.Add("Min");
                _propertyList.Add("Max");
                _propertyList.Add("Mean");
                _propertyList.Add("Median");
                _propertyList.Add("Skewness");
                _propertyList.Add("Variance");
                _propertyList.Add("ValueRange");
                _propertyList.Add("Kurtosis");
            }
            return _propertyList;
        }

        private List<Double> _doublesList;

        public List<double> PropertyValuesList
        {
            get
            {
                if (_doublesList == null)
                {
                    _doublesList = new List<double>();
                    _doublesList.Add(Min);
                _doublesList.Add(Max);
                _doublesList.Add(Mean);
                _doublesList.Add(Median);
                _doublesList.Add(Skewness);
                _doublesList.Add(Variance);
                _doublesList.Add(ValueRange);
                _doublesList.Add(Kurtosis);
                }
                  return
            _doublesList;
            }
        }

    }
}
