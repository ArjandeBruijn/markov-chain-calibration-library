using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovCalibrationChain
{
    public class MCMC
    {
        public delegate double GetLogProbability(Parameters parameters);
        public delegate void AcceptedEventHandler();

        private static Parameters parameters;
        private static GetLogProbability getlogprobability;
        public static AcceptedEventHandler accepted_event_handler =null;

        static OutputFile output =null;
        static Random R = new Random();
        static bool accepted;
        private static double jumpsize;
        

        public void CreateOutputFile(string FileName)
        {
            output = new OutputFile(FileName, parameters);
        }

        public void RunMCMC(int Iterations, double JumpSize)
        {

            jumpsize = JumpSize;

            double LogP_last = double.MinValue;

            for (int i = 0; i < Iterations; i++)
            {
                double logp = getlogprobability(parameters);

                double LogMetropolisAlpha = logp - LogP_last;
                LogP_last = logp;

                accepted = LogMetropolisAlpha > Math.Log(R.NextDouble());

                if (output != null) output.AddLine(i, parameters, logp, LogMetropolisAlpha, accepted);

                if (accepted)
                {
                    if (accepted_event_handler != null)
                    {
                        accepted_event_handler();
                    }
                    parameters.AcceptRunningValues();
                    parameters.Jump(jumpsize);
                }
                else
                {
                    parameters.UseLastAcceptedValues();
                }

                
            }
            System.Console.WriteLine("ready");
            //System.Console.ReadLine();
        }
        public MCMC(Parameters Parameters, GetLogProbability GetLogModelProbability)
        {
            parameters = Parameters;
            getlogprobability = GetLogModelProbability;
        }
    }
}
