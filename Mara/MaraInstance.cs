using System;
using System.Collections.Generic;
using Mara.Drivers;
using Mara.Servers;

namespace Mara {

    /*
     * This contains the instance methods for Mara.
     *
     * If your class inherits from Mara, it inherits Mara Magic!
     *
     * You'll be able to automagically Visit() your web application 
     * and Find() elements on the page, etc etc.
     *
     * This basically just wraps a Mara.Driver.  Actually, it *is* an IDriver!
     *
     * You definitely don't have to use this, but it makes things much easier.
     *
     */
    public partial class Mara : IDriver { // TODO ... inherit from Mara.Driver?  Maybe?  Dunno what to use Mara.Driver for *yet*

        IDriver _page;

        // This can be used in the [SetUp] part of your testing framework to setup Mara
        public void Initialize() {
            Console.WriteLine("Mara.Initialize()");
            Mara.Server.Start();
        }

        // This can be used in the [TearDown] part of your testing framework to teardown Mara
        public void Shutdown() {
            Console.WriteLine("Mara.Shutdown()");
            Console.WriteLine("  Server.Stop ...");
            Mara.Server.Stop();
            Console.WriteLine("  Close() driver ...");
            Close();
        }

        // This is the actual IDriver that Mara uses in the background
        // You can set this to use a different driver.
        public IDriver Page {
            get {
                if (_page == null) _page = Mara.Driver;
                return _page;
            }
            set {
                if (_page != null) _page.Close();
                _page = value;
            }
        }

        public IElement       Find(string a)         { return Page.Find(a);    }
        public IElement       Find(string a, bool b) { return Page.Find(a, b); }
        public List<IElement> All(string  a)         { return Page.All(a);     }

        public void Close()            { Page.Close();        }
        public void ResetSession()     { Page.ResetSession(); }
        public void Visit(string path) { Page.Visit(path);    }

        public string Body        { get { return Page.Body;        }}
        public string CurrentUrl  { get { return Page.CurrentUrl;  }}
        public string CurrentPath { get { return Page.CurrentPath; }}
    }
}
