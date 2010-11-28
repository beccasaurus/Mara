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

        [Test]
        public void CanGetCurrentUrl() {
            Visit("/");
            Assert.True(CurrentUrl == Mara.Server.AppHost + "/");

            Visit("/About.aspx");
            Assert.True(CurrentUrl == Mara.Server.AppHost + "/About.aspx"); // FIXME Don't use a Global Mara.Server
        }

        [Test]
        public void CanGetCurrentPath() {
            Visit("/");
            Assert.True(CurrentPath == "/");

            Visit("/About.aspx");
            Assert.True(CurrentPath == "/About.aspx");
        }

        // This should go in a group of specs that are ONLY for Drivers that support JavaScript
        // We could use NUnit categories to specify this?
        [Test][Ignore]
        public void CanGetCurrentPathAfterBeingRedirectedViaJavaScript() { }
    }
}
