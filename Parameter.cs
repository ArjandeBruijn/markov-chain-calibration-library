using System;
using System.Collections.Generic;
using System.Text;


namespace MarkovCalibrationChain
{

    public class Parameter  
    {
        static Random random = new Random();
        internal UniversalDistribution distribution;
        List<double> values;
        List<double> probabilities;
        double runningvalue;
        double LastAcceptedValue;
        string label;
        public string Label
        {
            get
            {
                return label;
            }
        }
        public static void SetRandomSeed(int seed)
        {
            random = new Random(seed);
        }
        public double RunningValue
        {
            get
            {
                return runningvalue;
            }
        }
        double Min(List<double> v)
        {
            double min = double.MaxValue;
            foreach (double _v in v) if (_v <min ) min = _v;
            return min;
        }
        double Max(List<double> v)
        {
            double max = double.MinValue;
            foreach(double _v in v)if(_v > max)max = _v;
            return max;
        }
        
        public void UseLastAcceptedValue()
        {
            runningvalue = LastAcceptedValue;
        }
        public void AcceptRunningValue()
        {
            LastAcceptedValue = runningvalue;
        }
       
        public bool OutOfRange()
        {
            bool ToLarge = (runningvalue > distribution.Max ? true : false);
            bool ToSmall = (runningvalue < distribution.Min ? true : false);
            return (ToLarge || ToSmall ? true : false);
        }
        
        public void Jump(double FractionOfDomain)
        {
            runningvalue = LastAcceptedValue + (random.NextDouble() - 0.5F) * FractionOfDomain * distribution.Range;
        }
      
        public void AddValueAndProbability(double probability)
        {
            values.Add(runningvalue);
            probabilities.Add(probability);

           
        }
        internal Parameter(Parameter p)
        {
            this.label = p.label;
            distribution = new UniversalDistribution(p.distribution.Min, p.distribution.Max);
            values = new List<double>();
            probabilities = new List<double>();
        }
        internal Parameter(string label, double Min, double Max, double Initial)
        {
            this.label = label;
            distribution = new UniversalDistribution(Min, Max);

            values = new List<double>();
            probabilities = new List<double>();
 
            runningvalue = Initial;
            LastAcceptedValue = runningvalue;
        }
      
    }
}
