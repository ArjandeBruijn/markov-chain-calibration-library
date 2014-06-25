using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovCalibrationChain
{
    internal class OutputFile
    {
        static System.IO.StreamWriter sw;
        static string filename;
        public void AddLine(int iteration, Parameters parameters, double LogProbability, double LogAlpha, bool Accepted)
        {
            sw = new System.IO.StreamWriter(filename, true);
            string Line = iteration + "\t";
            foreach(Parameter p in  parameters.ModelParameters)Line+= p.RunningValue +"\t";
            Line += LogProbability + "\t" + LogAlpha + "\t" + Accepted;
            sw.WriteLine(Line);
            sw.Close();
        }
        public OutputFile(string FileName, Parameters parameters)
        {
            if (System.IO.File.Exists(FileName)) System.IO.File.Delete(FileName);

            filename = FileName;
            sw = new System.IO.StreamWriter(FileName);

            string hdr = "Iteration\t";
            foreach (Parameter p in parameters.ModelParameters) hdr += p.Label + "\t";
            hdr += "LogProbability\tLogAlpha\tAccepted";

            sw.WriteLine(hdr);
            sw.Close();
        }
    }
}
