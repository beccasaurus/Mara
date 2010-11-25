using System;

namespace Mara.Drivers {

    /*
     * All Mara drivers must implement Mara.IDriver
     * 
     * This interface has the Visit(), CurrentPath(), FillIn(), etc methods
     */
    public interface IDriver {

        void Close();
        void ResetSession();
        void Visit(string path);

        string Body        { get; }
        string CurrentUrl  { get; }
        string CurrentPath { get; }
    }
}
