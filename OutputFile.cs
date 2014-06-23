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
        public void AddLine(int iteration, double LogProbability, double LogAlpha, bool Accepted)
        {
            sw = new System.IO.StreamWriter(filename, true);
            string Line = iteration + "\t" + LogProbability + "\t" + LogAlpha + "\t" + Accepted;
            sw.WriteLine(Line);
            sw.Close();
        }
        public OutputFile(string FileName)
        {
            if (System.IO.File.Exists(FileName)) System.IO.File.Delete(FileName);

            filename = FileName;
            sw = new System.IO.StreamWriter(FileName);

            string hdr = "Iteration\tLogProbability\tLogAlpha\tAccepted";

            sw.WriteLine(hdr);
            sw.Close();
        }
    }
}
