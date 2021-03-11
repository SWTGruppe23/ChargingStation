using System;
using System.Collections.Generic;
using System.Text;
using ChargingStation.IdReader;

namespace ChargingStation
{
    class StationControl
    {
        // Enumeration for keeping track of state

        private int CurrentId;
        // Constructor for injecting dependency
        public StationControl(IIdReader idReader)
        {
            idReader.IdReadEvent += HandleReadEvent;
        }

        private void HandleReadEvent(object sender, IdReadEventArgs e)
        {
            CurrentId = e.Id;
            // Handle Id
        }
    }
}
