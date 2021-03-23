using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Door
{
    class Door : IDoor
    {
        private bool doorLocked_;

        public event EventHandler<DoorEventArgs> DoorEvent;
        public void LockDoor()
        {
            doorLocked_ = true;
        }

        public void UnlockDoor()
        {
            doorLocked_ = false;
        }

        public void openDoor()
        {
            onDoorOpend(new DoorEventArgs{DoorOpened = true});
        }

        public void closeDoor()
        {
            onDoorClosed(new DoorEventArgs { DoorOpened = false });
        }

        protected virtual void onDoorOpend(DoorEventArgs e)
        {
            DoorEvent?.Invoke(this, e);
        }
        protected virtual void onDoorClosed(DoorEventArgs e)
        {
            DoorEvent?.Invoke(this, e);
        }
    }
}
