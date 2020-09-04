using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Utilities.Logger
{
    public static class ErrorTrace
    {
        private static object s_locker = new object();
        private static string s_logFolder;
        static ErrorTrace()
        {
            lock (s_locker)
            {
                s_logFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/LogFile/");
                if (!Directory.Exists(s_logFolder))
                {
                    Directory.CreateDirectory(s_logFolder);
                }
            }
        }
        private static string[] BuildLogHeading(LogArea logArea)
        {
            List<string> list = new List<string> { "====================================================================", "Administration Log" };
            try
            {
                list.Add(string.Format(CultureInfo.InvariantCulture, "DateTime : {0}", new object[] { DateTime.Now }));
                list.Add(string.Format(CultureInfo.InvariantCulture, "Area     : {0}", new object[] { logArea.ToString() }));
                //list.Add(string.Format(CultureInfo.InvariantCulture, "IP       : {0}", new object[] { httpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress }));
            }
            catch (Exception exception)
            {
                list.Add(string.Format(CultureInfo.InvariantCulture, "Error retrieving environment information: ", new object[] { exception.Message }));
            }
            list.Add("====================================================================");
            return list.ToArray();
        }

        private static void Heading(LogArea logArea, string message)
        {
            Info(logArea, "-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            Info(logArea, "Error :-");
            Info(logArea, message);
            Info(logArea, "-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
        }

        private static void Heading(LogArea logArea, Exception exception)
        {
            Info(logArea, "-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            Info(logArea, string.Format("\nMessage        :-\n{0}", exception.Message));
            Info(logArea, string.Format("\nHelpLink       :-\n{0}", exception.HelpLink));
            Info(logArea, string.Format("\nSource         :- \n{0}", exception.Source));
            Info(logArea, string.Format("\nStackTrace     :- \n{0}", exception.StackTrace));
            Info(logArea, string.Format("\nInner Exception:-  \n{0}", exception.InnerException));
            Info(logArea, string.Format("\nTargetSite     :- \n{0}", exception.TargetSite));
            Info(logArea, "-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
        }

        private static string BuildLogPath(string baseName)
        {
            string format = string.Format(CultureInfo.InvariantCulture, "{0}_{1:MM}{1:dd}{1:yyyy}_{1:HH}.log", new object[] { "{0}", DateTime.Now });
            return Path.Combine(s_logFolder, string.Format(CultureInfo.InvariantCulture, format, new object[] { baseName }));
        }

        private static void WriteLine(LogArea logArea, string message)
        {
            try
            {
                lock (s_locker)
                {
                    using (StreamWriter w = File.AppendText(BuildLogPath(logArea.ToString())))
                    {
                        w.WriteLine(message);
                        w.Flush();
                        w.Close();
                    }
                }
            }
            catch
            {
            }
        }

        private static void Info(LogArea logArea, string message)
        {
            WriteLine(logArea, message);
        }

        internal static void StartActivity(LogArea logArea)
        {
            lock (s_locker)
            {
                Info(logArea, string.Empty);
                Info(logArea, string.Empty);
                Info(logArea, string.Empty);
                foreach (string str in BuildLogHeading(logArea))
                {
                    Info(logArea, str);
                }
            }
        }

        public static void Logger(LogArea logArea, string message)
        {
            StartActivity(logArea);
            Heading(logArea, message);
        }

        public static void Logger(LogArea logArea, Exception exception)
        {
            StartActivity(logArea);
            Heading(logArea, exception);
        }
    }
    public enum LogArea
    {
        DataAccessLayer,
        ProviderLayer,
        RepositoryLayer,
        DataLayer,
        Lab,
        Proxy,
        Reporting,
        TeamBuild,
        Urls,
        Unknown,
        Info
    }
}
