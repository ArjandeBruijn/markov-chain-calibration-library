using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovCalibrationChain
{
    public static class MCMC
    {
        static OutputFile output =null;
        static Random R = new Random();
        static bool accepted;
        
        public static double fractionofdomain;

        public delegate double GetLogProbability(Parameters parameters);

        public static void CreateOutputFile(string FileName)
        {
            output = new OutputFile(FileName);
        }

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

                LogP_last = logp;

                if (output!= null) output.AddLine(i, logp, LogMetropolisAlpha, accepted);
            }
            System.Console.WriteLine("ready");
            //System.Console.ReadLine();
        }

    }
}
