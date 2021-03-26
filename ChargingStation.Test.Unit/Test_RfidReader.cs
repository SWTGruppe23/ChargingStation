using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.IdReader;
using NSubstitute;
using NUnit.Framework;

namespace ChargingStation.Test.Unit
{
    public class Test_RfidReader
    {
        RfidReader _uut = new RfidReader();

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ReadId_eventRaised()
        {
            // Arrange
            EventHandler<IdReadEventArgs> evtHandler = Substitute.For <EventHandler<IdReadEventArgs>>();
            _uut.IdReadEvent += evtHandler;
            // Act
            _uut.ReadId(5);
            // Assert
            evtHandler.Received(1);
        }
    }
}
