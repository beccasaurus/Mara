using System;
using System.Collections.Generic;
using System.Text;

namespace Mara {

    // Selenium-WebDriver implementation of IMara
    public class WebDriver : IMara {

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