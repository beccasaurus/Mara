using System;
using System.IO;
using NUnit.Framework;
using Mara;
using Mara.Servers;

namespace Mara.Specs {

    class TestServer : IServer {
        public void Start() { }
        public void Stop() { }
        public string App { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string AppHost { get { return null; } }
    }

    // We'll probably break this up into more specs as it grows ...
    [TestFixture]
    public class MaraSpec {

        [SetUp]
        public void Setup() {
            // reset defaults
            Mara.App       = null;
            Mara.AppHost   = null;
            Mara.RunServer = true;
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

        [Test]
        public void Mara_AppHost_DefaultsToAppHostFromServer() {
            Mara.Server = null;
            Assert.Null(Mara.AppHost);

            Mara.Server = new TestServer { Port = 1234, Host = "localhost" };
            Assert.That(Mara.AppHost, Is.EqualTo("http://localhost:1234"));

            Mara.Server = new TestServer { Port = 5678, Host = "localhost" };
            Assert.That(Mara.AppHost, Is.EqualTo("http://localhost:5678"));
        }

        [Test]
        public void Mara_AppHost_CanBeConfigured() {
            Mara.Server = null;
            Assert.Null(Mara.AppHost);

            Mara.AppHost = "http://www.google.com";
            Assert.That(Mara.AppHost, Is.EqualTo("http://www.google.com"));
        }

        [Test]
        public void Mara_RunServer_DefaultsToTrue() {
            Assert.True(Mara.RunServer);
        }

        [Test]
        public void Mara_RunServer_CanBeConfigured() {
            Assert.True(Mara.RunServer);

            Mara.RunServer = false;

            Assert.False(Mara.RunServer);
        }

        [Test][Ignore("Pending")]
        public void Mara_RunServer_ActuallyDoesntRunIfSetToFalse() { }
    }
}
