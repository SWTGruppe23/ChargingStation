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
            Connected = charger.Connected;
            charger.ChargerEvent += HandleChargerEvent;
        }

        public bool Connected { get; }
        public void StartCharge()
        {
            Console.WriteLine("Ladning påbegyndt");
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            Console.WriteLine("Ladning afsluttet");
            _usbCharger.StopCharge();
        }

        // Event handlers
        private void HandleChargerEvent(object? sender, ChargerEventArgs e)
        {
            if (e.Current ==0) { }
            else if (e.Current <= 5)
            {
                Console.WriteLine("Telefon opladt");
            }
            else if (e.Current <= 500)
            {
                Console.WriteLine("Telefon lader");
            }
            else if (e.Current > 500)
            {
                Console.WriteLine("Kortslutning! Ladning stoppes");
                StopCharge();
            }
            
        }
    }
}
