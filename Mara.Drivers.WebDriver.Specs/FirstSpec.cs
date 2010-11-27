using System;
using NUnit.Framework;
using Mara;

using System.Reflection; // print version info

namespace Mara.Drivers.WebDriverSpecs {

    [TestFixture]
    public class FirstSpec : MaraTestFixture {

        public static void PrintVersionInfo() {
            Console.WriteLine(".NET Version: " + System.Environment.Version.ToString());
            var mono = Type.GetType("Mono.Runtime", false, true);
            if (mono == null)
                Console.WriteLine("Runtime: Microsoft .NET");
            else {
                Console.WriteLine("Runtime: Mono {0}",
                    mono.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null));
            }
        }

        [SetUp]
        public void Setup() {
            Console.WriteLine("SETUP called ... calling initialize ...");
            Initialize(); // Why doesn't the base class's SetUp get run???
            PrintVersionInfo();
        }

        [Test]
        public void CanOpenAPageOrWhatever() {
            Console.WriteLine("Visit /");
            Visit("/");
            Console.WriteLine("Page.Body assertions ...");
            Assert.That(Page.Body, Is.StringContaining("Mara test application"));
            Assert.That(Page.Body, Is.Not.StringContaining("About this site"));
            
            Console.WriteLine("Visit /About.aspx");
            Visit("/About.aspx");
            Console.WriteLine("Page.Body assertions ...");
            Assert.That(Page.Body, Is.StringContaining("About this site"));
            Assert.That(Page.Body, Is.Not.StringContaining("Mara test application"));
        }
    }
}
