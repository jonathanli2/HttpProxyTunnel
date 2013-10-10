using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace WinProxy
{
	/// <summary>
	/// Summary description for Logger.
	/// </summary>
	public class Logger
	{
		private static Logger s_logger;
        private bool m_bLogToFile;

		private StreamWriter m_logWriter;

		private Logger() {} //private constructor--for singleton


		public static bool initialize(string strLogFilePath, bool bLogToFile)
		{
            s_logger = new Logger();
            s_logger.m_bLogToFile = bLogToFile;

            //delete old log files
            if (bLogToFile)
            {
                if (File.Exists(strLogFilePath + ".bak"))
                    File.Delete(strLogFilePath + ".bak"); //remove the .bak if it exists

                //copy current file to .bak
                if (File.Exists(strLogFilePath))
                {
                    File.Copy(strLogFilePath, strLogFilePath + ".bak");
                    File.Delete(strLogFilePath);
                }

                try
                {
                    //Create the stream for writing the log
                    s_logger.m_logWriter = new StreamWriter(strLogFilePath, false);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Unable to create log {0}.  The exception is {1}.", strLogFilePath, e);
                    s_logger.m_logWriter = null;
                }
            }
			return true;

		}

		public static void close()
		{
            if (s_logger!=null)
            {
                if (s_logger.m_logWriter != null)
                {
                    s_logger.m_logWriter.Close();
                    s_logger.m_logWriter = null;
                }
                s_logger = null;
            }
		}

        public static void log(byte[] data, int start, int end, String msg, params object[] vars)
        {
                String strData = Encoding.UTF8.GetString(data, start, end);
                String convertedMsg = s_logger.convertToLogMsg(msg, vars);
                s_logger.writeToLog(convertedMsg + "\r\n"+ strData +"\r\n");
        }

		private String convertToLogMsg( String msg, object[] vars)
		{
			StringBuilder builder = new StringBuilder();
			//create the header:  TimeStamp [Level] 
			builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff "));

			builder.Append("T");
			builder.Append(Thread.CurrentThread.ManagedThreadId);
			builder.Append( " ");

			builder.Append(msg);

			//do variable substitution
			if (vars != null)
			{
				for (int i=0; i< vars.Length; i++)
				{
					builder.Replace("{" + i + "}", vars[i].ToString());
				}
			}
			return builder.ToString();
		}

		public void writeToLog(String msg)
		{
			WinTunnel.WriteTextToLog(msg);

			if (m_bLogToFile && m_logWriter != null)
			{
				lock(m_logWriter)
				{
					try
					{
						m_logWriter.Write(msg);
						m_logWriter.Write(Environment.NewLine);
						m_logWriter.Flush();
			
					}
					catch (Exception e)
					{
						m_logWriter = null;
						System.Console.WriteLine("Exception occurred when writing to log: {0}", e);
					}
				}
			}
		}
	}
}
