using System;
using System.Net;

using Microsoft.VisualStudio.WebHost;

using Mara;

namespace Mara.Servers {

    /*
     * ...
     * ...
     */
    public class Cassini : IServer {
        int _port = 8090; // TODO use something global like Mara.ServerPort ?
        Server _server;

        public void Start() {
            _server = new Server(Port, "/", App);
            _server.Start();

            // TODO write code that'll wait for the server to respond instead of sleeping
            Console.WriteLine("Cassini2 started");
            System.Threading.Thread.Sleep(1000);
        }

        public void Stop() {
            Console.WriteLine("Cassini STOP");
            _server.Stop();
        }

        public string App  { get; set; }

        public int Port {
            get { return _port;  }
            set { _port = value; }
        }

        public string Host {
            get { return "localhost"; }
            set { }
        }

        public string AppHost {
            get { return string.Format("http://{0}:{1}", Host, Port); }
        }
    }
}
