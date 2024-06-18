
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QTaskDataLayer.DBOperations;



namespace QTaskDataLayer.Repository
{
	public class CommonRepository
	{
		DB objDB;
		string path;

		public CommonRepository(IConfiguration _config)
		{
			//objDB = new DB(configuration);			
			path = _config.GetSection("AppSettings")["ErrorPath"].ToString();
			objDB = new DB(_config);

		}
		public int SaveErrorLog(string PageName, string FunctionName, string Error, string UserName)
		{
			int result = 0;

			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@PageName",PageName),
					new SqlParameter("@FunctionName",FunctionName),
					new SqlParameter("@UserName",UserName),
					new SqlParameter("@ErrorMessage",Error)

				};

				result = objDB.InsertData("Q_Pr_SaveErrorLog", param);
			}
			catch (Exception ex)
			{
				WriteErrorLog(Error);
			}

			return result;

		}

		public bool WriteErrorLog(string LogMessage)
		{
			bool Status = false;
			string LogDirectory = path;

			DateTime CurrentDateTime = DateTime.Now;
			string CurrentDateTimeString = CurrentDateTime.ToString();
			CheckCreateLogDirectory(LogDirectory);
			string logLine = BuildLogLine(CurrentDateTime, LogMessage);
			LogDirectory = (LogDirectory + "Log_" + LogFileName(DateTime.Now) + ".txt");

			lock (typeof(CommonRepository))
			{
				StreamWriter oStreamWriter = null;
				try
				{
					oStreamWriter = new StreamWriter(LogDirectory, true);
					oStreamWriter.WriteLine(logLine);
					Status = true;
				}
				catch
				{

				}
				finally
				{
					if (oStreamWriter != null)
					{
						oStreamWriter.Close();
					}
				}
			}
			return Status;
		}

		private bool CheckCreateLogDirectory(string LogPath)
		{
			bool loggingDirectoryExists = false;
			DirectoryInfo oDirectoryInfo = new DirectoryInfo(LogPath);
			if (oDirectoryInfo.Exists)
			{
				loggingDirectoryExists = true;
			}
			else
			{
				try
				{
					Directory.CreateDirectory(LogPath);
					loggingDirectoryExists = true;
				}
				catch
				{
					// Logging failure
				}
			}
			return loggingDirectoryExists;
		}

		private string BuildLogLine(DateTime CurrentDateTime, string LogMessage)
		{
			StringBuilder loglineStringBuilder = new StringBuilder();
			loglineStringBuilder.Append(LogFileEntryDateTime(CurrentDateTime));
			loglineStringBuilder.Append(" \t");
			loglineStringBuilder.Append(LogMessage);
			return loglineStringBuilder.ToString();
		}


		public string LogFileEntryDateTime(DateTime CurrentDateTime)
		{
			return CurrentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
		}


		private string LogFileName(DateTime CurrentDateTime)
		{
			return CurrentDateTime.ToString("dd_MM_yyyy");
		}

	}
}
