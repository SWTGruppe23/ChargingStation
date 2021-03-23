using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingStation.IdReader
{
    class RfidReader : IIdReader
    {
        public event EventHandler<IdReadEventArgs> IdReadEvent;

        public void ReadId(int newId)
        {
            OnIdRead(new IdReadEventArgs{Id = newId});
        }

        protected virtual void OnIdRead(IdReadEventArgs e)
        {
            IdReadEvent?.Invoke(this, e);
        }
    }
}
