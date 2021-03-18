using System;
using ChargingStation.Door;
using ChargingStation.IdReader;
using ChargingStation.UsbCharger;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ChargingStation.Test.Unit
{
    public class Tests_StationControl
    {
        private StationControl _uut;
        private IIdReader _fakeIdReader = Substitute.For<IIdReader>();
        private IUsbCharger _usbChargerSimulator = new UsbChargerSimulator();
        private IDoor _fakeDoor = Substitute.For<IDoor>();

        [SetUp]
        public void Setup()
        {
            _uut = new StationControl(_fakeIdReader, _fakeDoor, _usbChargerSimulator);
        }

        
        [Test]
        public void IIdReader_ReadId_IdReadEvent()
        {
            _fakeIdReader.IdReadEvent += Raise.EventWith(new object(), new IdReadEventArgs());
            _fakeDoor.Received(1).LockDoor();
        }
    }
}