using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.ChargeControl;
using ChargingStation.Door;
using ChargingStation.UsbCharger;
using ChargingStation.IdReader;

namespace ChargingStation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private int CurrentId;
        private IDoor _door;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IIdReader idReader, IDoor door, IChargeControl charger)
        {
            idReader.IdReadEvent += HandleReadEvent;
            door.DoorEvent += HandleDoorEvent;
            _charger = charger;
            _door = door;
            _state = LadeskabState.Available;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleReadEvent(object sender, IdReadEventArgs e)
        {
            CurrentId = e.Id;
            RfidDetected(CurrentId);
        }

        private void HandleDoorEvent(object sender, DoorEventArgs e)
        {
            switch (e.DoorOpened)
            {
                case true:
                    DoorOpened();
                    break;
                case false:
                    DoorClosed();
                    break;
            }
        }


        // Flere funktioner

        private void DoorOpened()
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    Console.WriteLine("Tilslut Telefon");
                    _state = LadeskabState.DoorOpen;
                    break;

                case LadeskabState.DoorOpen:
                    //ignorrer
                    break;

                case LadeskabState.Locked:
                    //ignorer
                    break;

            }
        }

        private void DoorClosed()
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    Console.WriteLine("Indlæs RFID");
                    _state = LadeskabState.Available;
                    break;

                case LadeskabState.Available:
                    //ignorrer
                    break;

                case LadeskabState.Locked:
                    //ignorer
                    break;

            }
        }
    }
}
