using System;
using System.Net;
using System.Net.Sockets;
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

            Console.Write("Cassini starting ... ");
            try {
                _server.Start();
            } catch (SocketException ex) {
                //throw new Exception(string.Format("Couldn't start Cassini ... do you already have something running on port {0}?\n{1}",
                //        Port, ex.Message));
                Console.WriteLine("Cassini complained about something: {0}", ex.Message);
            }
            // Mara.WaitForLocalPortToBecomeUnavailable(Port);
            System.Threading.Thread.Sleep(3000); // it's not happy, let's just keep sleeping for now FIXME
            Console.WriteLine("done");
        }

        public void Stop() {
            Console.Write("Cassini stopping ... ");
            _server.Stop();
            //Mara.WaitForLocalPortToBecomeAvailable(Port); // meh
            Console.WriteLine("done");
        }
    }
}
