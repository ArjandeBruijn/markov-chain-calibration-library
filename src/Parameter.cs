using System;
using System.Collections.Generic;
using System.Text;


namespace MarkovCalibrationChain
{

    public class Parameter  
    {
        static Random random = new Random();
        internal UniversalDistribution distribution;
        
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
        
        internal void UseLastAcceptedValue()
        {
            runningvalue = LastAcceptedValue;
        }
        internal void AcceptRunningValue()
        {
            LastAcceptedValue = runningvalue;
        }

        public  bool OutOfRange()
        {
            bool ToLarge = (runningvalue > distribution.Max ? true : false);
            bool ToSmall = (runningvalue < distribution.Min ? true : false);
            return (ToLarge || ToSmall ? true : false);
        }

        internal void Jump(double FractionOfDomain)
        {
            runningvalue = LastAcceptedValue + (random.NextDouble() - 0.5F) * FractionOfDomain * distribution.Range;
        }

        
        internal Parameter(Parameter p)
        {
            this.label = p.label;
            distribution = new UniversalDistribution(p.distribution.Min, p.distribution.Max);
         
        }
        internal Parameter(string label, double Min, double Max, double Initial)
        {
            this.label = label;
            distribution = new UniversalDistribution(Min, Max);

           
            runningvalue = Initial;
            LastAcceptedValue = runningvalue;
        }
      
    }
}
