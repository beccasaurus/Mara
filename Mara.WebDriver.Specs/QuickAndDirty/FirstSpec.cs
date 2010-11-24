using System;
using NUnit.Framework;
using Mara;

namespace Mara.WebDriver.Specs {

    [TestFixture]
    public class FirstSpec {

        IMara page;

        [SetUp]
        public void Setup() {
            page = Mara.Driver;
        }

        [Test]
        public void CanOpenAPageOrWhatever() {
            
        }
    }
}