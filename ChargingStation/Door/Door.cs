using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Door
{
    class Door : IDoor
    {
        //private bool doorLocked_;

        public event EventHandler<DoorEventArgs> DoorEvent;
        public void LockDoor()
        {
            //doorLocked_ = true;
        }

        public void UnlockDoor()
        {
            //doorLocked_ = false;
        }

        public void OpenDoor()
        {
            OnDoorOpen(new DoorEventArgs{ DoorOpened = true });
        }

        public void CloseDoor()
        {
            OnDoorClose(new DoorEventArgs { DoorOpened = false });
        }

        protected void OnDoorOpen(DoorEventArgs e)      // Var virtual, men kunne ikke se hvorfor
        {
            DoorEvent?.Invoke(this, e);
        }
        protected void OnDoorClose(DoorEventArgs e)     // var virtual 
        {
            DoorEvent?.Invoke(this, e);
        }
    }
}
