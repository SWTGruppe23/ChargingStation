using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.UsbCharger;

namespace ChargingStation.ChargeControl
{
    public interface IChargeControl
    {
        public bool Connected { get; }
        public void StartCharge();
        public void StopCharge();
    }
}
