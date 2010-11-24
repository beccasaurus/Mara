using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Mara {

    /*
     * Mara offers the main API for working with Mara configuration 
     * and getting drivers, etc
     */
    public class Mara {

        static string _app;

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

        public string DefaultDriverClass = "Mara.WebDriver"; // <--- if available ...

        public static IMara Driver {
            get { return null; }
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
    }
}
