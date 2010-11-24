using System;
using System.Collections.Generic;
using System.Text;

namespace Mara {

    /*
     * All Mara drivers implement IMara
     * 
     * This interface has the Visit(), CurrentPath(), FillIn(), etc methods
     */
    public interface IMara {

        void ResetSession();

        void Visit(string path);

        string Body        { get; }
        string CurrentUrl  { get; }
        string CurrentPath { get; }
    }
}