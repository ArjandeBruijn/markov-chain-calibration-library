using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChain
{
    public class MCMC
    {
        static Random R = new Random();
        static bool accepted;
        
        public static double fractionofdomain;

        public delegate double GetLogProbability(Parameters parameters);


        public static void RunMCMC(Parameters parameters, GetLogProbability getlogmodelprobability, int Iterations, double JumpSize)
        {
            fractionofdomain = JumpSize;

            double LogP_last = double.MinValue;

            for (int i = 0; i < Iterations; i++)
            {
                double logp = getlogmodelprobability(parameters);

                double LogMetropolisAlpha = logp - LogP_last;

                accepted = LogMetropolisAlpha > Math.Log(R.NextDouble());

                if (accepted)
                {
                    parameters.AcceptRunningValues();
                    parameters.Jump(fractionofdomain);
                }
                else
                {
                    parameters.UseLastAcceptedValues();
                }

                foreach (Parameter p in parameters.ModelParameters)
                {
                    if (p.OutOfRange())
                    {
                        throw new System.Exception("Parameter " + p.Label + " should not be out of range here...");
                    }
                    p.AcceptRunningValue();
                }

                LogP_last = logp;
            }
            System.Console.WriteLine("ready");
            //System.Console.ReadLine();
        }

    }
}
