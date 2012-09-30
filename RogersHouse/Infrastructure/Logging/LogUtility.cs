using System;
using System.Collections;
using System.Text;
using System.Web;

namespace RogersHouse.WebUI.Infrastructure.Logging
{
    public class LogUtility
    {
        private static readonly StringBuilder Sb = new StringBuilder();

        public static string BuildExceptionMessage(Exception x)
        {
            return BuildExceptionMessage(x, 0);
        }

        public static string BuildExceptionMessage(Exception x, int exceptionLevel)
        {
            string tabs = GetTabs(exceptionLevel);
            Sb.AppendLine();
            Sb.AppendFormat("{2}Message: {0}{1}", x.Message, Environment.NewLine, tabs);
            Sb.AppendFormat("{2}Exception Type: {0}{1}", x.GetType(), Environment.NewLine, tabs);
            Sb.AppendFormat("{2}Version: {0}{1}", typeof (LogUtility).Assembly.GetName().Version, Environment.NewLine,
                            tabs);
            Sb.AppendFormat("{2}OS Version: {0}{1}", Environment.OSVersion, Environment.NewLine, tabs);
            Sb.AppendFormat("{2}Source: {0}{1}", x.Source, Environment.NewLine, tabs);
            Sb.AppendFormat("{2}RawUrl: {0}{1}", HttpContext.Current.Request.RawUrl, Environment.NewLine, tabs);
            Sb.AppendFormat("{2}Client Ip: {0}{1}", HttpContext.Current.Request.UserHostAddress, Environment.NewLine,
                            tabs);
            Sb.AppendFormat("{2}TargetSite: {0}{1}", x.TargetSite, Environment.NewLine, tabs);
            Sb.AppendFormat("{2}Stack Trace: {0}{1}", x.StackTrace, Environment.NewLine, tabs);
            if (x.Data.Count > 0)
            {
                Sb.AppendFormat("{1}Exception Data :{0}", Environment.NewLine, tabs);

                foreach (DictionaryEntry item in x.Data)
                {
                    if (item.Value != null)
                        Sb.AppendFormat("{3}{1} - {2}{0}", Environment.NewLine, item.Key, item.Value, tabs);
                }
            }

            if (x.InnerException != null)
            {
                Sb.AppendFormat("{1}Inner Exception:  {0}", Environment.NewLine, tabs);
                Sb.Append(BuildExceptionMessage(x.InnerException, exceptionLevel + 1));
            }

            return Sb.ToString();
        }

        private static string GetTabs(int times)
        {
            string result = string.Empty;

            for (int i = 0; i < times; i++)
            {
                result += "\t";
            }
            return result;
        }
    }
}