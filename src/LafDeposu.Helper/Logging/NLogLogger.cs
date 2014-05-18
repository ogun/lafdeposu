using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LafDeposu.Helper.Logging
{
    public class NLogger : ILogger
    {
        private Logger Logger;

        public NLogger()
        {
            string fileName = "${basedir}/App_Data/log.txt";
            string layout = "${message}";
            LoggingConfiguration config = CreateLoggingConfiguration(LogLevel.Debug, fileName, layout);

            // Activate the configuration
            LogManager.Configuration = config;

            Logger = LogManager.GetCurrentClassLogger();
        }

        public static LoggingConfiguration CreateLoggingConfiguration(LogLevel minLogLevel, string fileName, string layout)
        {
            // Step 1. Create configuration object 
            LoggingConfiguration config = new LoggingConfiguration();


            // Step 2. Create targets
            FileTarget fileTarget = new FileTarget();

            // Step 3. Set target properties 
            fileTarget.FileName = fileName; ;
            fileTarget.Layout = layout;
            fileTarget.Encoding = Encoding.GetEncoding(1254);

            // Step 4. Wrap target with async
            AsyncTargetWrapper asyncWrapper = new AsyncTargetWrapper();
            asyncWrapper.QueueLimit = 5000;
            asyncWrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Discard;
            asyncWrapper.WrappedTarget = fileTarget;

            // Step 5. Add wrapper to config
            config.AddTarget("file", asyncWrapper);

            // Step 6. Define rules
            LoggingRule rule = new LoggingRule("*", minLogLevel, asyncWrapper);
            config.LoggingRules.Add(rule);

            return config;
        }

        public void Debug(string msg)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Debug, Logger.Name, msg);
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void DebugException(string msg, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Debug, Logger.Name, msg);
            logEvent.Exception = ex;
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void Error(string msg)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Error, Logger.Name, msg);
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void ErrorException(string msg, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Error, Logger.Name, msg);
            logEvent.Exception = ex;
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void Fatal(string msg)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Fatal, Logger.Name, msg);
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void FatalException(string msg, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Fatal, Logger.Name, msg);
            logEvent.Exception = ex;
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void Info(string msg)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, Logger.Name, msg);
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void InfoException(string msg, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, Logger.Name, msg);
            logEvent.Exception = ex;
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void Trace(string msg)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Trace, Logger.Name, msg);
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void TraceException(string msg, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Trace, Logger.Name, msg);
            logEvent.Exception = ex;
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void Warn(string msg)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Warn, Logger.Name, msg);
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void WarnException(string msg, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Warn, Logger.Name, msg);
            logEvent.Exception = ex;
            Logger.Log(typeof(NLogger), logEvent);
        }

        public void DebugException(Exception ex)
        {
            this.DebugException(ex.Message, ex);
        }

        public void ErrorException(Exception ex)
        {
            this.ErrorException(ex.Message, ex);
        }

        public void FatalException(Exception ex)
        {
            this.FatalException(ex.Message, ex);
        }

        public void InfoException(Exception ex)
        {
            this.InfoException(ex.Message, ex);
        }

        public void TraceException(Exception ex)
        {
            this.TraceException(ex.Message, ex);
        }

        public void WarnException(Exception ex)
        {
            this.WarnException(ex.Message, ex);
        }
    }
}
