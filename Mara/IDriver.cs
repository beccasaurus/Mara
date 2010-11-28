using System;
using System.Collections.Generic;

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

        IElement Find(string xpath);
        IElement Find(string xpath, bool throwExceptionIfNotFound);
        
        // TODO Instead of returning a simple List<IElement>, All should return something that you can chain additional finders on
        List<IElement> All(string xpath);

        string Body        { get; }
        string CurrentUrl  { get; }
        string CurrentPath { get; }
    }
}
