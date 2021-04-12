using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Door
{
    public class Door : IDoor
    {
        public bool doorLocked_;

        public event EventHandler<DoorEventArgs> DoorEvent;
        public void LockDoor()
        {
            doorLocked_ = true;
        }

        public void UnlockDoor()
        {
            doorLocked_ = false;
        }

        public void OpenDoor()
        {
            OnDoorOpen(new DoorEventArgs{ DoorOpened = true });
        }

        public void CloseDoor()
        {
            OnDoorClose(new DoorEventArgs{ DoorOpened = false });
        }

        private void OnDoorOpen(DoorEventArgs e)   
        {
            DoorEvent?.Invoke(this, e);
        }
        private void OnDoorClose(DoorEventArgs e)    
        {
            DoorEvent?.Invoke(this, e);
        }
    }
}
