using System;
using Castle.Core;
using ChargingStation.ChargeControl;
using ChargingStation.Door;
using ChargingStation.IdReader;
using ChargingStation.Logger;
using ChargingStation.UsbCharger;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.Routing.Handlers;
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
            // Clear subs
            _fakeDoor.ClearReceivedCalls();
            // Arrange
            _fakeChargeControl.Connected.Returns(true);
            _uut._state = StationControl.LadeskabState.Available;
            // Act
            _uut.RfidDetected(1);
            // Assert
            _fakeDoor.Received(1).LockDoor();
        }

        [Test]
        public void RdidDetected_IsAvailable_NotConnected_LockDoorNotCalled()
        {
            // Clear subs
            _fakeDoor.ClearReceivedCalls();
            // Arrange
            _fakeChargeControl.Connected.Returns(false);
            _uut._state = StationControl.LadeskabState.Available;
            // Act
            _uut.RfidDetected(1);
            // Assert
            _fakeDoor.Received(0).LockDoor();
        }

        [Test]
        public void RdidDetected_DoorOpen_StartChargeNotCalled()
        {
            // Clear subs
            _fakeChargeControl.ClearReceivedCalls();
            // Arrange
            _uut._state = StationControl.LadeskabState.DoorOpen;
            // Act
            _uut.RfidDetected(1);
            // Assert
            _fakeChargeControl.Received(0).StartCharge();
        }

        [Test]
        public void RdidDetected_Locked_IdMatch_UnlockDoorIsCalled()
        {
            // Clear subs
            _fakeDoor.ClearReceivedCalls();
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

        [Test]
        public void RdidDetected_Locked_IdMismatch_UnlockDoorNotCalled()
        {
            // Clear subs
            _fakeDoor.ClearReceivedCalls();
            // Arrange
            _uut._state = StationControl.LadeskabState.Available;
            _uut.RfidDetected(1);
            _uut._state = StationControl.LadeskabState.Locked;
            // Act
            _uut.RfidDetected(2);
            // Assert
            _fakeDoor.Received(0).UnlockDoor();
        }

        [Test]
        public void HandleDoorEvent_DoorOpened_Available_StateIsDoorOpen()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Available;
            DoorEventArgs evt = Substitute.For<DoorEventArgs>();
            evt.DoorOpened = true;
            // Act
            _uut.HandleDoorEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state == StationControl.LadeskabState.DoorOpen);
        }

        [Test]
        public void HandleDoorEvent_DoorOpened_DoorOpen_stateUnchanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.DoorOpen;
            DoorEventArgs evt = Substitute.For<DoorEventArgs>();
            evt.DoorOpened = true;
            // Act
            _uut.HandleDoorEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state == StationControl.LadeskabState.DoorOpen);
        }

        [Test]
        public void HandleDoorEvent_DoorOpened_Locked_stateUnchanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Locked;
            DoorEventArgs evt = Substitute.For<DoorEventArgs>();
            evt.DoorOpened = true;
            // Act
            _uut.HandleDoorEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state == StationControl.LadeskabState.Locked);
        }

        [Test]
        public void HandleDoorEvent_DoorClosed_Available_stateUnchanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Available;
            DoorEventArgs evt = Substitute.For<DoorEventArgs>();
            evt.DoorOpened = false;
            // Act
            _uut.HandleDoorEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state == StationControl.LadeskabState.Available);
        }

        [Test]
        public void HandleDoorEvent_DoorClosed_DoorOpen_StateIsAvailable()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.DoorOpen;
            DoorEventArgs evt = Substitute.For<DoorEventArgs>();
            evt.DoorOpened = false;
            // Act
            _uut.HandleDoorEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state == StationControl.LadeskabState.Available);
        }

        [Test]
        public void HandleDoorEvent_DoorClosed_Locked_stateUnchanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Locked;
            DoorEventArgs evt = Substitute.For<DoorEventArgs>();
            evt.DoorOpened = false;
            // Act
            _uut.HandleDoorEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state == StationControl.LadeskabState.Locked);
        }

        [Test]
        public void HandleReadEvent_IdIs5_CurrentIdIs5()
        {
            // Arrange
            IdReadEventArgs evt = Substitute.For<IdReadEventArgs>();
            evt.Id = 5;
            // Act
            _uut.HandleReadEvent(new object(), evt);
            // Assert
            Assert.That(_uut.CurrentId.Equals(5));
        }

        /// Boundary tests
        [Test]
        public void RfidDetected_IdIsNegative_StateUnchanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Available;
            _fakeChargeControl.Connected.Returns(true);
            IdReadEventArgs evt = Substitute.For<IdReadEventArgs>();
            evt.Id = -1;
            // Act
            _uut.HandleReadEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void RfidDetected_IdIs0_StateChanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Available;
            _fakeChargeControl.Connected.Returns(true);
            IdReadEventArgs evt = Substitute.For<IdReadEventArgs>();
            evt.Id = 0;
            // Act
            _uut.HandleReadEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
        }

        [Test]
        public void RfidDetected_IdIs1_StateChanged()
        {
            // Arrange
            _uut._state = StationControl.LadeskabState.Available;
            _fakeChargeControl.Connected.Returns(true);
            IdReadEventArgs evt = Substitute.For<IdReadEventArgs>();
            evt.Id = 1;
            // Act
            _uut.HandleReadEvent(new object(), evt);
            // Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
        }
        ///


    }
}