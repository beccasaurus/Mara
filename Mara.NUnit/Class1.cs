using System;
using Mara;
using Mara.Drivers;
using NUnit.Framework;

namespace Mara {

    /*
     * In your own test suites, create a [SetUpFixture] in the namespace 
     * you want to use for your tests and inherit it from MaraSetUpFixture.
     *
     *     namespace MyTests {
     *         [SetUpFixture] public class Fixture : MaraSetUpFixture {}
     *     }
     */
    public class MaraSetUpFixture {

        public static Mara MaraInstance;

        [SetUp]
        public void MaraSetUp() {
            Console.WriteLine("IntegrationTests SetUp");
            if (MaraInstance == null) {
                MaraInstance = new Mara();
                MaraInstance.Initialize();
            }
        }

        [TearDown]
        public void MaraTearDown() {
            Console.WriteLine("IntegrationTests TearDown");
            MaraInstance.Shutdown();
        }
    }

    /*
     * If you use the IntegrationTests namespace, inheriting from 
     * IntegrationTest will give you a DSL with access to the global 
     * Mara instance
     */
    public class MaraTest : IDriver {
        public IDriver Page { get { return MaraSetUpFixture.MaraInstance; }}

        public void Close()            { Page.Close();        }
        public void ResetSession()     { Page.ResetSession(); }
        public void Visit(string path) { Page.Visit(path);    }
        public string Body        { get { return Page.Body;        }}
        public string CurrentUrl  { get { return Page.CurrentUrl;  }}
        public string CurrentPath { get { return Page.CurrentPath; }}
    }

    /*
     * If you inherit your NUnit [TestFixture] classes from 
     * MaraTestFixture, you get [SetUp] and [TearDown] methods 
     * defined that will handle the setup and teardown of 
     * a Mara.Driver and Mara.Server for you.
     * 
     * In ideal scenarios, you can simply inherit from MaraTestFixture 
     * and do no configuration what-so-ever!
     * 
     * NOTE: *All* this does is call Initialize() and Shutdown() for you.
     *
     * DEPRECATED ?????? Maybe?
     */
    public class MaraTestFixture : Mara {

        [SetUp]
        public void MaraSetUp() {
            Console.WriteLine("(INSTANCE ... deprecated?) MaraTearDown.SetUp");
            Initialize();
        }

        [TearDown]
        public void MaraTearDown() {
            Console.WriteLine("(INSTANCE ... deprecated?) MaraTearDown.TearDown");
            Shutdown();
        }
    }
}
