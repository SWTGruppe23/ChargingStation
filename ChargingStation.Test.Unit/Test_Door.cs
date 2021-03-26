using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.Door;
using NSubstitute;
using NUnit.Framework;

namespace ChargingStation.Test.Unit
{
    public class Test_Door
    {
        private Door.Door _uut = new Door.Door();

        [SetUp]
        public void SetUp()
        {
            _uut = new Door.Door();
        }

        [Test]
        public void IsDoorOpen()
        {
            //Arrange
            EventHandler<DoorEventArgs> evtOpen = Substitute.For<EventHandler<DoorEventArgs>>();
            _uut.DoorEvent += evtOpen;
            //Act
            _uut.OpenDoor();
            //Assert
            evtOpen.Received(1);
        }

        [Test]
        public void IsDoorClosed()
        {
            //Arrange
            EventHandler<DoorEventArgs> evtClose = Substitute.For<EventHandler<DoorEventArgs>>();
            _uut.DoorEvent += evtClose;
            //Act
            _uut.CloseDoor();
            //Assert
            evtClose.Received(1);
        }
    }
}
