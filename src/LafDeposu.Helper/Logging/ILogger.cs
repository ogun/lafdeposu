using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LafDeposu.Helper.Logging
{
    public interface ILogger
    {
        void Debug(string msg);
        void DebugException(Exception ex);
        void DebugException(string msg, Exception ex);

        void Error(string msg);
        void ErrorException(Exception ex);
        void ErrorException(string msg, Exception ex);

        void Fatal(string msg);
        void FatalException(Exception ex);
        void FatalException(string msg, Exception ex);

        void Info(string msg);
        void InfoException(Exception ex);
        void InfoException(string msg, Exception ex);

        void Trace(string msg);
        void TraceException(Exception ex);
        void TraceException(string msg, Exception ex);

        void Warn(string msg);
        void WarnException(Exception ex);
        void WarnException(string msg, Exception ex);
    }
}
