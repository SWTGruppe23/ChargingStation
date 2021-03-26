using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.Logger;
using NUnit.Framework;

namespace ChargingStation.Test.Unit
{
    class Test_FileLogger
    {
        private FileLogger _uut = new FileLogger();

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Log()
        {
            _uut.log("test", 1);

            System.IO.StreamReader file = new StreamReader(@"logfile.txt");
            string line = file.ReadLine();

            Assert.That(line.Contains("test"));
        }
    }
}
