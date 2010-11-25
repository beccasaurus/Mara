using System;

using NUnit.Framework;

namespace Mara {

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
     */
    public class MaraTestFixture : Mara {

        [SetUp]
        public void MaraSetUp() {
            Initialize();
        }

        [TearDown]
        public void MaraTearDown() {
            Shutdown();
        }
    }
}