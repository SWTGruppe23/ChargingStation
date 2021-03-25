using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Logger
{
    public interface ILogger
    {
        public void log(string logtext, int id);
    }
}
