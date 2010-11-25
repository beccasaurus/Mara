using System;

namespace Mara.Servers {

    /*
     * Baseclass for IServer implementations that gives you the properties you need and whatnot.
     * 
     * You don't have to inherit from this, it's totally optional!
     */
    public abstract class Server : IServer {
        public int    Port    { get; set; }
        public string Host    { get; set; }
        public void   Start() { throw new NotImplementedException(); }
        public void   Stop()  { throw new NotImplementedException(); }

        public string AppHost {
            get { return string.Format("http://{0}:{1}", Host, Port); }
        }
    }
}
