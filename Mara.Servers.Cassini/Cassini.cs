using System;
using System.Net;
using Mara;

namespace Mara.Servers {

    /*
     * ...
     * ...
     */
    public class Cassini : Server, IServer {
        Microsoft.VisualStudio.WebHost.Server _server;

        public void Start() {
            _server = new Microsoft.VisualStudio.WebHost.Server(Port, "/", App);
            _server.Start();

            // TODO write code that'll wait for the server to respond instead of sleeping
            Console.WriteLine("Cassini started");
            System.Threading.Thread.Sleep(1000);
        }

        public void Stop() {
            Console.WriteLine("Cassini STOP");
            _server.Stop();
        }
    }
}
