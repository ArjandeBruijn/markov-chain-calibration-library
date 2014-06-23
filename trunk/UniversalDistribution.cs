using System;
using System.Collections.Generic;
using System.Text;

namespace MarkovCalibrationChain
{
    internal class UniversalDistribution
    {
        internal double Min;
        internal double Max;

        internal bool InDomain(double v)
        {
            if (v > Min && v < Max) return true;
            return false;
        }
        internal double Range
        {
            get
            {
                return Max - Min;
            }
        }
        internal UniversalDistribution(double Min, double Max)
        {
            this.Min = Min;
            this.Max = Max;
        }
    }
}
