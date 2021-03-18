using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Door
{
    class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorEvent;
        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }
    }
}
