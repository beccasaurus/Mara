using System;
using Mara;

namespace Mara.Servers {

    /*
     * ...
     * ...
     */
    public class XSP : IServer {
        public void Start() {
            Console.WriteLine("XSP Start!");
        }

        public void Stop() {
            Console.WriteLine("XSP STOP");
        }

        public int Port {
            get {
                return 1234;
            }
            set {
            }
        }

        public string Host {
            get {
                return "HOSTname";
            }
            set {
            }
        }

        public string AppHost {
            get { return "app host go here"; }
        }
    }
}
