using System;
using System.Net;
using System.Net.Sockets;
using Mono.WebServer;
using Mara;

namespace Mara.Servers {

    /*
     * ...
     * ...
     */
    public class XSP : Server, IServer {
        ApplicationServer _server;
        bool _started = false;

        public XSP() { Console.WriteLine("Constructed a new XSP server"); }

        public void Start() {
            if (_started == true) return;
            _started = true;

            Console.WriteLine("XSP.Start()");
            _server = new ApplicationServer(new XSPWebSource(IPAddress.Any, Port));
			_server.AddApplicationsFromCommandLine(string.Format("{0}:/:{1}", Port, App));

            Console.Write("XSP2 starting ... ");
            try {
                _server.Start(true);
            } catch (SocketException) {}
            Mara.WaitForLocalPortToBecomeUnavailable(Port);
            Console.WriteLine("done");
        }

        public void Stop() {
            Console.Write("XSP2 stopping ... ");
            _server.Stop();
            // Mara.WaitForLocalPortToBecomeAvailable(Port); // meh, just kill this process ... it doesn't like to stop ...
            Console.WriteLine("done");
        }
    }
}
