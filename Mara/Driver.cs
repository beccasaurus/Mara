using System;

namespace Mara.Drivers {

    /*
     * Baseclass for IDriver implementations that gives you ... (not sure yet)
     * 
     * You don't have to inherit from this, it's totally optional!
     */
    public abstract class Driver : IDriver {

        public void Close() {
            throw new NotImplementedException();
        }

        public void ResetSession() {
            throw new NotImplementedException();
        }

        public void Visit(string path) {
            throw new NotImplementedException();
        }

        public string Body {
            get { throw new NotImplementedException(); }
        }

        public string CurrentUrl {
            get { throw new NotImplementedException(); }
        }

        public string CurrentPath {
            get { throw new NotImplementedException(); }
        }
    }
}
