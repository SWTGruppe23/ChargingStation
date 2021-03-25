using System;
using Castle.Core;
using ChargingStation.ChargeControl;
using ChargingStation.Door;
using ChargingStation.IdReader;
using ChargingStation.Logger;
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
        private IChargeControl _fakeChargeControl = Substitute.For<IChargeControl>();
        private IDoor _fakeDoor = Substitute.For<IDoor>();
        private ILogger _fakeLogger = Substitute.For<ILogger>();

        [SetUp]
        public void Setup()
        {
            _uut = new StationControl(_fakeIdReader, _fakeDoor, _fakeChargeControl, _fakeLogger);
        }

        [Test]
        public void ctor_IsAvailable()
        {
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available) );
        }

        [Test]
        public void RdidDetected_IsAvailable_IsConnected_LockDoorIsCalled()
        {
            // Arrange
            _fakeChargeControl.Connected.Returns(true);
            _uut._state = StationControl.LadeskabState.Available;
            // Act
            _uut.RfidDetected(1);
            // Assert
            _fakeDoor.Received(1).LockDoor();
        }

        //[Test]
        //public void RdidDetected_IsAvailable_NotConnected_LockDoorIsCalled()
        //{
        //    // Arrange
        //    _fakeChargeControl.Connected.Returns(false);
        //    _uut._state = StationControl.LadeskabState.Available;
        //    // Act
        //    _uut.RfidDetected(1);
        //    // Assert
        //    ;
        //}

        [Test]
        public void RdidDetected_Locked_UnlockDoorIsCalled()
        {
            // Arrange
            _fakeChargeControl.Connected.Returns(true);
            _uut._state = StationControl.LadeskabState.Available;
            _uut.RfidDetected(1);
            _uut._state = StationControl.LadeskabState.Locked;
            // Act
            _uut.RfidDetected(1);
            // Assert
            _fakeDoor.Received(1).UnlockDoor();
        }

        //[Test]
        //public void IIdReader_ReadId_IdReadEvent()
        //{
        //    _fakeIdReader.IdReadEvent += Raise.EventWith(new object(), new IdReadEventArgs());
        //    _fakeDoor.Received(1).LockDoor();
        //}
    }
}