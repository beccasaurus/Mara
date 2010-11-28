using System;
using NUnit.Framework;
using Mara;

using System.Reflection; // print version info

namespace IntegrationTests {

    [SetUpFixture] public class Fixture : MaraSetUpFixture {}

    [TestFixture]
    public class FirstSpec : MaraTest {

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

        //[SetUp]
        //public void Setup() {
        //    Console.WriteLine("SETUP called ... calling initialize ...");
        //    Initialize(); // Why doesn't the base class's SetUp get run???
        //    PrintVersionInfo();
        //}

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

        [Test]
        public void CanFindElementUsingXPath_ReturnsNullIfNotFound() {
             Visit("/");

             Assert.That(Find("//h2").Text, Is.EqualTo("Why is this styled?"));
             Assert.Null(Find("//h5"));
        }

        [Test][Ignore]
        public void CanFindElementUsingXPath_BlowsUpIfNotFound() {
             //Visit("/");
            
             //... should not raise ...
             //Find("//h2")

             //... should raise ...
             //Find("//h5")
        }

        [Test]
        public void CanFindElementsUsingXPath_ReturnsEmptyIfNotFound() {
            Visit("/");

            Assert.That(All("//p").Count, Is.EqualTo(5));
            Assert.That(All("//p")[0].Text, Is.EqualTo("If you see this text, you're running our test suite!"));

            Assert.IsEmpty(All("//strong"));
            Assert.That(All("//strong").Count, Is.EqualTo(0));
        }

        // TODO there should be a spec for elements to make sure they work properly
        
        [Test][Ignore]
        public void CanGetTheValueOfAnElement() {}

        [Test][Ignore]
        public void CanGetTheTextOfAnElement() {}

        [Test][Ignore]
        public void CanGetTheValueOfAnElementsAttribute() {}

        [Test][Ignore]
        public void CanFindLinkByText_ReturnsNullIfNotFound() {
        }

        [Test][Ignore]
        public void CanClickLink_BlowsUpIfNotFound() {
        }
    }
}
