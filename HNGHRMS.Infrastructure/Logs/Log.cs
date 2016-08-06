using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Configuration;
namespace HNGHRMS.Infrastructure.Logs
{
	public class Log
	{
        private const string LogView = "HNGHRMSAPP";             
        //public static void Write(string source, string message, EventLogEntryType type, int eventId)
        //{
            
        //    try
        //    {
        //        if (!EventLog.SourceExists(source))
        //        {
        //            EventLog.CreateEventSource(source, LogView);
        //        }
        //        EventLog.WriteEntry(source, message, type, eventId);
        //    }
        //    catch { }
        //}
        public static void Write(string message,EventLogEntryType type,int eventId,string source = "")
        {
            try
            {
                if(source == "")
                {
                    source = WebConfigurationManager.AppSettings["LogSource"] != null ? WebConfigurationManager.AppSettings["LogSource"] : "HNGHRMSAPP";
                    
                }
                if (!EventLog.SourceExists(source))
                {
                    EventLog.CreateEventSource(source, LogView);
                }
                EventLog.WriteEntry(source, message, type, eventId);                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
        public static void Write(string message, string source = "")
		{
			//Write(source, message, EventLogEntryType.Information, 0);
            Write(message, EventLogEntryType.Information,0,source);
		}

        public static void Write(string message, int eventId, string source = "")
		{
			//Write(source, message, EventLogEntryType.Information, eventId);
            Write(message, EventLogEntryType.Information, eventId,source);
		}

        public static void WriteWarning(string message, string source = "")
		{
			//Write(source, message, EventLogEntryType.Warning, 0);
            Write(message, EventLogEntryType.Warning, 0,source);
		}

		public static void WriteWarning(string message, int eventId,string source = "")
		{
			//Write(source, message, EventLogEntryType.Warning, eventId);
            Write(message, EventLogEntryType.Warning, eventId,source);
		}

		public static void WriteError(string message,string source = "")
		{
			Write(message, EventLogEntryType.Error, 0,source);
		}

		public static void WriteError(string message, int eventId,string source = "" )
		{
			Write(message, EventLogEntryType.Error, eventId,source);
		}
	}
}
