using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Door
{
    public class DoorEventArgs : EventArgs
    {
        public bool DoorOpened { get; set; }
    }
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorEvent;
        public void LockDoor();
        public void UnlockDoor();
        public void OpenDoor();
        public void CloseDoor();
    }
}
