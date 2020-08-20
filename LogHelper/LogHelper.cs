using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHelperLib
{
    /// <summary>
    /// Logger Lib Handler
    /// </summary>

    public enum LogTarget
    {
        File, Database, EventLog
    }

    public abstract class LogBase
    {
        public abstract void Log(string message);
    }

    public class FileLogger : LogBase
    {
        public string filePath = @"C:\rundeck\FLog.txt";
        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }

    //public class DBLogger : LogBase
    //{
    //    string connectionString = string.Empty;
    //    public override void Log(string message)
    //    {
    //        Code to log data to the database
    //    }
    //}

    public class EventLogger : LogBase
    {
        public string filePath = @"C:\rundeck\ELog.txt";
        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }


    public static class LogHelper
    {

        private static LogBase logger = null;
        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;
                //case LogTarget.Database:
                //    logger = new DBLogger();
                //    logger.Log(message);
                //    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    logger.Log(message);
                    break;
                default:
                    return;
            }
        }
    }
    //public class Class1
    //{
    //}
}
