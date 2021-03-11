using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingStation.IdReader
{
    public class IdReadEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    
    public interface IIdReader
    {
        event EventHandler<IdReadEventArgs> IdReadEvent;
    }
}
