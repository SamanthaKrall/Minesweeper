using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using NUnit.Framework.Internal;

namespace Minesweeper.Services.Utility
{
    public class MyLogger1 : ILogger
    {
        private static MyLogger1 instance;
        private static NLog.Logger logger;

        private MyLogger1() { }

        public static MyLogger1 GetInstance()
        {
            if(instance == null)
            {
                instance = new MyLogger1();
            }
            return instance;
        }

        public void Debug(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Debug(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Debug(message, arg);
            }
            
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Error(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Error(message, arg);
            }
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Info(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Info(message, arg);
            }
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Warn(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Warn(message, arg);
            }
        }

        private NLog.Logger GetLogger(string theLogger)
        {
            if (MyLogger1.logger == null)
            {
                MyLogger1.logger = LogManager.GetLogger(theLogger);
            }
            return MyLogger1.logger;
        }
    }
}