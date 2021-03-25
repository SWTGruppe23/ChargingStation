using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.UsbCharger;

namespace ChargingStation.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger charger)
        {
            _usbCharger = charger;
            charger.ChargerEvent += HandleChargerEvent;
        }

        public bool Connected { get; }
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        // Event handlers
        private void HandleChargerEvent(object? sender, ChargerEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
