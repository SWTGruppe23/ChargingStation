using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.UsbCharger;
using NSubstitute;
using NUnit.Framework;

namespace ChargingStation.Test.Unit
{
    public class Test_ChargeControl
    {
        private ChargeControl.ChargeControl _uut;
        private IUsbCharger _fakeUsbCharger = Substitute.For<IUsbCharger>();

        [SetUp]
        public void Setup()
        {
            _fakeUsbCharger.Connected = false;
            _uut = new ChargeControl.ChargeControl(_fakeUsbCharger);
        }


        [Test]
        public void Charge_Started()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            _fakeUsbCharger.Connected.Returns(true);
            //Act
            _uut.StartCharge();
            //Assert
            _fakeUsbCharger.Received(1).StartCharge();
        }

        [Test]
        public void Charge_Stopped()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            _fakeUsbCharger.Connected.Returns(true);
            //Act
            _uut.StopCharge();
            //Assert
            _fakeUsbCharger.Received(1).StopCharge();
        }


        /// Boundary tests
        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIsNegative_NotConnected()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = -1;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            Assert.That(_uut.Connected, Is.False);
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs0_NotConnected()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 0;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            Assert.That(_uut.Connected, Is.False);
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs1_ChargingStopped()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 1;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(1).StopCharge();
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs4_ChargingStopped()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 4;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(1).StopCharge();
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs5_ChargingStopped()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 5;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(1).StopCharge();
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs6_StillCharging()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 6;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(0).StopCharge();
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs499_StillCharging()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 499;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(0).StopCharge();
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs500_StillCharging()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 500;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(0).StopCharge();
        }

        [Test]
        [Category("Boundary")]
        public void HandleChargerEvent_CurrentIs501_ChargingStopped()
        {
            //Clear subs
            _fakeUsbCharger.ClearReceivedCalls();
            //Arrange
            ChargerEventArgs evt = Substitute.For<ChargerEventArgs>();
            evt.Current = 501;
            //Act
            _uut.HandleChargerEvent(new object(), evt);
            //Assert
            _fakeUsbCharger.Received(1).StopCharge();
        }
        /// 

    }
}
