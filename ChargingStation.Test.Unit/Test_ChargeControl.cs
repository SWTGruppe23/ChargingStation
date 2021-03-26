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
            _uut = new ChargeControl.ChargeControl(_fakeUsbCharger);
        }

        // Test af connected - duer ikke

        //[Test]
        //public void IsConnected()
        //{
        //    //Clear subs
        //    _fakeUsbCharger.ClearReceivedCalls();
        //    //Arrange
        //    _fakeUsbCharger.Connected.Returns(true);
        //    //Act

        //    //Assert
        //    Assert.That(_uut.Connected, Is.True);
        //}

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


        [Test]
        public void HandleEvent_StopChargeOver500()
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

    }
}
