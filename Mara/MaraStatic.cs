using System;
using System.IO;
using System.Reflection;
using Mara.Drivers;
using Mara.Servers;

namespace Mara {

    /*
     * This contains the static methods for Mara
     *
     * This offers the main API for working with Mara configuration 
     */
    public partial class Mara {

        public static string DefaultDriverName = "Mara.Drivers.WebDriver";
        public static string DefaultServerName = "Mara.Servers.XSP"; // <--- should be Cassini on Windows

        static string  _app;
        static string  _appHost;
        static IDriver _driver;
        static IServer _server;

        // The path to an ASP.NET (MVC) web application that Mara.Server 
        // will boot up if Mara.RunServer isn't set to false
        public static string App {
            get {
                if (_app == null) return TryToFindWebAppDirectory();
                else              return _app;
            }
            set {
                if (value != null) _app = Path.GetFullPath(value);
                else               _app = null;
            }
        }

        public static bool RunServer = true;

        public static IServer Server {
            get {
                if (_server == null) _server = InstantiateDefaultServer();
                return _server;
            }
            set {
                if (_server != null) _server.Stop();
                _server = value;
            }
        }

        public static string AppHost {
            get {
                if (_appHost != null) return _appHost;
                else                  return (Server == null) ? null : Server.AppHost;
            }
            set { _appHost = value; }
        }

        public static IDriver Driver {
            get {
                if (_driver == null) _driver = InstantiateDefaultDriver();
                return _driver;
            }
            set {
                if (_driver != null) _driver.Close();
                _driver = value;
            }
        }

        // Private Helper Methods

        static string TryToFindWebAppDirectory() {
            DirectoryInfo parent = null;
            string        match  = null;
            var           dir    = Directory.GetCurrentDirectory();

            while (match == null && dir != null) {
                match  = FindDirectoryThatHasWebConfig(dir, true);
                parent = Directory.GetParent(dir);

                if (parent == null)
                    dir = null;
                else
                    dir = parent.FullName;
            }

            return match;
        }

        static string FindDirectoryThatHasWebConfig(string rootDirectoryToLookIn, bool lookInSubdirectories) {
            // if this directory has a Web.config, return it
            if (File.Exists(Path.Combine(rootDirectoryToLookIn, "Web.config")))
                return rootDirectoryToLookIn;
            
            // if any of the directories IN this directory have a Web.config, return it
            if (lookInSubdirectories == true) {
                foreach (var dir in Directory.GetDirectories(rootDirectoryToLookIn)) {
                    var result = FindDirectoryThatHasWebConfig(dir, false); // if this has the Web.config, it won't be null
                    if (result != null)
                        return result;
                }
            }
            
            // else return null, meaning that we couldn't find it
            return null;
        }

        static IDriver InstantiateDefaultDriver() {
            Console.WriteLine("InstantiateDefaultDriver");
            Type driverType = GetTypeFromWhereverWeCan(Mara.DefaultDriverName);
            if (driverType != null)
                return Activator.CreateInstance(driverType) as IDriver;
            return null;
        }

        static IServer InstantiateDefaultServer() {
            Console.WriteLine("InstantiateDefaultServer");
            Type serverType = GetTypeFromWhereverWeCan(Mara.DefaultServerName);
            if (serverType != null) {
                var server = Activator.CreateInstance(serverType) as IServer;
                if (server != null)
                    server.App = Mara.App;
                return server;
            }
            return null;
        }

        static Type GetTypeFromWhereverWeCan(string typeFullName) {
            // Try finding the type in this assembly
            Type type = Type.GetType(typeFullName);
            if (type != null) return type;

            // Try finding the type in any of our references assemblies
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                type = assembly.GetType(typeFullName);
                if (type != null) return type;
            }

            // Try loading up a DLL with the same name as the type and finding the type in there
            var dll = typeFullName + ".dll";
            if (File.Exists(dll)) {
                type = Assembly.LoadFile(dll).GetType(typeFullName);
                if (type != null) return type;
            }

            return null;
        }
    }
}
