using System;
using System.Collections.Generic;
using System.Text;

namespace MarkovCalibrationChain
{
    public delegate bool ValidParameterVector(Parameters parameters);

    public class Parameters // : IEnumerable<Parameter>
    {
        ValidParameterVector checkparameters;

        List<Parameter> modelparameters;

        public bool checkoutofrange(Parameters parameters)
        {
            foreach (Parameter p in parameters.ModelParameters)
            {
                if (p.OutOfRange()) return false;
            }
            return true;
        }

        public List<Parameter> ModelParameters
        {
            get
            {
                return modelparameters ;
            }
        }

        public Parameter this[string label]
        {
            get
            {
                return Get(label);
            }
        }
        public void Add(string label, double Min, double Max, double Initial)
        {
            modelparameters.Add(new Parameter(label, Min, Max, Initial));
        }
        public string Values(int decimals)
        {
            string line = "";
            foreach (Parameter p in ModelParameters)
            {
                line += p.Label + "\t" + Math.Round(p.RunningValue,2) + "\t";
            }
            return line;
        }
        private Parameter Get(string label)
        {
            foreach (Parameter p in ModelParameters)
            {
                if (p.Label == label) return p;
            }
            return null;          
        }
        internal void UseLastAcceptedValues()
        {
            foreach (Parameter p in ModelParameters)
            {
                p.UseLastAcceptedValue();
            }
        }
        internal void AcceptRunningValues()
        {
            foreach (Parameter p in ModelParameters)
            {
                p.AcceptRunningValue();
            }
        }

        internal void Jump(double FractionOfDomain)
        {
            
            do
            {
                foreach (Parameter p in ModelParameters)
                {
                    p.Jump(FractionOfDomain);
                }
            }
            while (checkparameters(this) == false);
        }

        public Parameters(ValidParameterVector Checkparameters)
        {
            checkparameters = Checkparameters;
            modelparameters = new List<Parameter>();
        }
        public Parameters()
        {
            checkparameters = checkoutofrange;
            modelparameters = new List<Parameter>();
        }
        //public GetEnumerator
    }
}
