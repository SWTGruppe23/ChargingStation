using System;
using ChargingStation.Door;
using ChargingStation.IdReader;
using ChargingStation.UsbCharger;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ChargingStation.Test.Unit
{
    public class Tests
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
            //_fakeIdReader.ReadId(10); 
            _fakeIdReader.IdReadEvent += Raise.EventWith(new object(), new IdReadEventArgs());
            _fakeDoor.Received(1).LockDoor();
            
            //_uut.Received(1).HandleReadEvent(_fakeIdReader, new IdReadEventArgs { Id = 10 });
            

            //var wasCalled = false;
            //_fakeIdReader.IdReadEvent += (sender, args) => wasCalled = true;

            //_fakeIdReader.IdReadEvent += Raise.EventWith(new object(), new IdReadEventArgs());
            ////Assert.That(_uut.Received(1).);
            //Assert.True(wasCalled);
        }
    }
}//