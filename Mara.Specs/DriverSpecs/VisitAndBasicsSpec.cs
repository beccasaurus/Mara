using System;
using NUnit.Framework;
using Mara;

namespace Mara.DriverSpecs {

    /*
     * This spec covers some of the very basics:
     *
     *   Visit()
     *   Page.Body
     *   CurrentUrl
     *   CurrentPath
     */
    [TestFixture]
    public class VisitAndBasicsSpec : MaraTest {

        [Test]
        public void CanVisitRelativePath() {
            Visit("/");
            Assert.That(Page.Body, Is.StringContaining("Mara test application"));
            Assert.That(Page.Body, Is.Not.StringContaining("About this site"));
            
            Visit("/About.aspx");
            Assert.That(Page.Body, Is.StringContaining("About this site"));
            Assert.That(Page.Body, Is.Not.StringContaining("Mara test application"));
        }

        [Test][Ignore]
        public void CanVisitAbsolutePath() { }

        [Test][Ignore]
        public void CanGetCurrentPath() { }

        [Test][Ignore]
        public void CanGetCurrentUrl() { }
    }
}
