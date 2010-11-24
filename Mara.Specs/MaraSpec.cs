using System;
using System.IO;
using NUnit.Framework;
using Mara;

namespace Mara.Specs {

    // We'll probably break this up into more specs as it grows ...
    [TestFixture]
    public class MaraSpec {

        [SetUp]
        public void Setup() {
            // reset defaults
            Mara.App = null;
        }

        [Test]
        public void Mara_App_LooksForDirectoryWithWebConfig() {
            // we *know* where our WebApp is ... let's just make sure Mara.App finds it
            // Mara.Specs/bin/Debug/*.dll ... app is up at ../../../../WebApp/
            var ourAppPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../WebApp"));

            Assert.That(Mara.App, Is.EqualTo(ourAppPath));
        }

        [Test]
        public void Mara_App_CanBeConfigured() {
            Mara.App = ".";
            Assert.That(Mara.App, Is.EqualTo(Path.GetFullPath(Directory.GetCurrentDirectory())));

            Mara.App = "..";
            Assert.That(Mara.App, Is.EqualTo(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."))));
            Assert.That(Mara.App, Is.Not.EqualTo(Path.GetFullPath(Directory.GetCurrentDirectory())));
        }

        [Test][Ignore("Pending")]
        public void Mara_AppHost_DefaultsToLocalhost() {
        }

        [Test][Ignore("Pending")]
        public void Mara_AppHost_CanBeConfigured() {
        }

        [Test][Ignore("Pending")]
        public void Mara_RunServer_DefaultsToTrue() {
        }

        [Test][Ignore("Pending")]
        public void Mara_RunServer_CanBeConfigured() {
        }

        [Test][Ignore("Pending")]
        public void Mara_RunServer_ActuallyDoesntRunIfSetToFalse() {
        }
    }
}
