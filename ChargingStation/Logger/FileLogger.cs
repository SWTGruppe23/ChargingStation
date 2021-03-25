using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Logger
{
    public class FileLogger : ILogger
    {
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        public void log(string logtext, int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + logtext, id);
            }
        }
    }
}
