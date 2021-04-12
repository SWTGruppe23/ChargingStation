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
using ChargingStation.Logger;

namespace ChargingStation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        public int CurrentId;
        private IDoor _door;
        private ILogger _logger;


        // Her mangler constructor
        public StationControl(IIdReader idReader, IDoor door, IChargeControl charger, ILogger log)
        {
            idReader.IdReadEvent += HandleReadEvent;
            door.DoorEvent += HandleDoorEvent;
            _charger = charger;
            _door = door;
            _state = LadeskabState.Available;
            _logger = log;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(int id)
        {
            if (id < 0)
            {
                _logger.log("Id was negative", id);
                return;
            } 
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _logger.log(": Skab låst op med RFID: {0}", id);

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
                        _logger.log(": Skab låst op med RFID: {0}", id);

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
        public void HandleReadEvent(object sender, IdReadEventArgs e)
        {
            CurrentId = e.Id;
            RfidDetected(CurrentId);
        }

        public void HandleDoorEvent(object sender, DoorEventArgs e)
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
