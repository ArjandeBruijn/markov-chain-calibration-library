using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkovCalibrationChain;

namespace TestModel
{
    class Program
    {
       
        static double GetLogProb(Parameters parameters)
        {
            //System.Console.WriteLine(Math.Round(parameters["A"].RunningValue,2));
            return Math.Log( 1.0 / Math.Pow(parameters["A"].RunningValue - 10,2));
        }
        static void Main(string[] args)
        {
             
            Parameters parameters = new Parameters();

            // (0.957, -0.684).
            //parameters.Add(new CalibrationLibrary.Parameter("noise_scale",1,1, 0.5));
            //parameters.Add(new CalibrationLibrary.Parameter("a1", -0.9, -0.9, 0.5));
            //parameters.Add(new CalibrationLibrary.Parameter("a2", 0.6, 0.6, 0.5));

            parameters.Add("A", 0, 20,0.0);

            MCMC mcmc = new MCMC(parameters, GetLogProb);

            mcmc.CreateOutputFile("MCMC_testmodel.txt");

            mcmc.RunMCMC(10000, 0.1);
          
             
            

        }
    }
}
