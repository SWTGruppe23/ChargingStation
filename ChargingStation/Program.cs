using System;
using ChargingStation.ChargeControl;
using ChargingStation.Door;
using ChargingStation.IdReader;
using ChargingStation.UsbCharger;

namespace ChargingStation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IIdReader rfidReader = new RfidReader();
            IDoor door = new Door.Door();
            IUsbCharger usbCharger = new UsbChargerSimulator();
            IChargeControl charger = new ChargeControl.ChargeControl(usbCharger);
            StationControl control = new StationControl(rfidReader, door, charger);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OpenDoor();
                        break;

                    case 'C':
                        door.CloseDoor();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.ReadId(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

