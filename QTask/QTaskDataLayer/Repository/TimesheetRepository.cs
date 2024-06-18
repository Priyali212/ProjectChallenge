using Microsoft.Extensions.Configuration;
using QTaskDataLayer.DBModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTaskDataLayer.DBOperations;

namespace QTaskDataLayer.Repository
{
	public class TimesheetRepository
	{
		DB objDB;
		CommonRepository objComm;

		public TimesheetRepository(IConfiguration iconfig)
		{
			objDB = new DB(iconfig);
			objComm = new CommonRepository(iconfig);
		}

		public string SaveTimesheets(TimesheetDBModel SaveTimesheetModel)
		{
			int result = 0;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@JiraId",SaveTimesheetModel.JiraId),
					new SqlParameter("@WorkedDate",SaveTimesheetModel.WorkedDate),
					new SqlParameter("@Description",SaveTimesheetModel.Description),
					new SqlParameter("@minutesSpent",SaveTimesheetModel.MinSpend),
					new SqlParameter("@UserId", SaveTimesheetModel.UserId)
				};
				result = objDB.InsertData("Q_Pr_SaveTimeSheets", param);
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("TimesheetRepository", "SaveTimesheets", ex.Message, "");
			}
			return result > 0 ? "Data saved Successfully" : "Error occured";
		}


		public List<TimesheetDBModel> GetTimesheetList(int UserId, string WorkedDate, int PageIndex, int PageSize)
		{
			List<TimesheetDBModel> objTimesheet = new List<TimesheetDBModel>();
			DataTable dtFirstTable = new DataTable();
			int totalRecord = 0;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
			   new SqlParameter("@UserId",UserId),
			   new SqlParameter("@WorkedDate",WorkedDate),
			   new SqlParameter("@PageIndex",PageIndex),
					new SqlParameter("@PageSize",PageSize)
				};
				DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_ShowTimesheetdetails", param);
				dtFirstTable = ds.Tables[0];
				if (dtFirstTable.Rows.Count > 0)
				{
					DataRow row = dtFirstTable.Rows[0];
					totalRecord = Convert.ToInt32(row["TotalRec"]);
				}
				if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
				{
					foreach (DataRow dr in ds.Tables[1].Rows)
					{
						TimesheetDBModel objTimeList = new TimesheetDBModel();
						objTimeList.TotalRecords = totalRecord;
						objTimeList.JiraId = dr["JiraId"].ToString().Trim();
						objTimeList.Task = dr["Task"].ToString().Trim();
						objTimeList.TimesheetId = Convert.ToInt32(dr["TimesheetId"].ToString().Trim());
						objTimeList.Description = dr["Description"].ToString().Trim();
						objTimeList.WorkedDate = dr["WorkedDate"].ToString().Trim();
						objTimeList.MinSpend = dr["minutesSpent"].ToString().Trim();

						objTimesheet.Add(objTimeList);
					}
				}
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("TimesheetRepository", "GetTimesheetList", ex.Message, "");
			}

			return objTimesheet;
		}



		public List<SearchDbModel> GetSearchKeywords(string SearchKeywords)
		{
			List<SearchDbModel> objSearchList = new List<SearchDbModel>();
			DataSet dt = new DataSet();

			try
			{
				SqlParameter param1 = new SqlParameter("@Keyword", SearchKeywords);

				SqlParameter[] parameters = new SqlParameter[]
				{
					param1
				};

				dt = objDB.getDataFromDBToDataSet("Q_Pr_GetSearchRes_TimeSheet", parameters);

				if (dt.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dt.Tables[0].Rows)
					{
						SearchDbModel objSearch = new SearchDbModel();
						objSearch.JiraID = row["JiraID"].ToString();
						//objSearch.Task = row["Task"].ToString();

						objSearchList.Add(objSearch);
					}
				}
			}
			catch (Exception ex)
			{
				///ErrorLogRepository errorLogRepository = new ErrorLogRepository();
				//errorLogRepository.InsertErrorLog("SearchRepository.cs", "GetSearchKeywords", ex.Message);
			}

			return objSearchList;
		}

		public List<SearchDbModel> GetSearchKeywordsForTasksheet(string SearchKeywords)
		{
			List<SearchDbModel> objSearchList = new List<SearchDbModel>();
			DataSet dt = new DataSet();

			try
			{
				SqlParameter param1 = new SqlParameter("@Keyword", SearchKeywords);

				SqlParameter[] parameters = new SqlParameter[]
				{
					param1
				};

				dt = objDB.getDataFromDBToDataSet("Q_Pr_GetSearchRes_Task", parameters);

				if (dt.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dt.Tables[0].Rows)
					{
						SearchDbModel objSearch = new SearchDbModel();
						objSearch.JiraID = row["JiraID"].ToString();
						//objSearch.Task = row["Task"].ToString();

						objSearchList.Add(objSearch);
					}
				}
			}
			catch (Exception ex)
			{
				///ErrorLogRepository errorLogRepository = new ErrorLogRepository();
				//errorLogRepository.InsertErrorLog("SearchRepository.cs", "GetSearchKeywords", ex.Message);
			}

			return objSearchList;
		}

		public DataTable CheckJiraId(string JiraId)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
			new SqlParameter("@JiraId", JiraId),
				};
				ds = objDB.getDataFromDBToDataSet("Q_Pr_CheckJiraID", param);

				dt = ds.Tables[0];
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("TimeSheetRepository", "GetTimesheetDetails", ex.Message, "");
			}

			return dt;
		}



		public bool DeleteTimesheet(int TimesheetId, string UserName)
		{
			bool Result = false;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
			new SqlParameter("@TimesheetId",TimesheetId),
			new SqlParameter("@UserId",UserName)

				};

				int Res = objDB.DeleteData("Q_Pr_DeleteTimesheetRec", param);

				if (Res > 0)
					Result = true;
			}
			catch (Exception ex)
			{
				// objcomm.SaveErrorLog("ModuleRepository", "DeleteModule", ex.Message, UserName);
			}

			return Result;
		}

	}
}
