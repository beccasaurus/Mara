using System;
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

        public void Close()            { Page.Close();        }
        public void ResetSession()     { Page.ResetSession(); }
        public void Visit(string path) { Page.Visit(path);    }

        public string Body        { get { return Page.Body;        }}
        public string CurrentUrl  { get { return Page.CurrentUrl;  }}
        public string CurrentPath { get { return Page.CurrentPath; }}
    }
}
