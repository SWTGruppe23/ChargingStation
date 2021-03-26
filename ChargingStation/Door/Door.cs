using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Door
{
    public class Door : IDoor
    {
        //private bool doorLocked_;

        public event EventHandler<DoorEventArgs> DoorEvent;
        public void LockDoor()
        {
            //dummy metode
        }

        public void UnlockDoor()
        {
            //dummy metode
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
