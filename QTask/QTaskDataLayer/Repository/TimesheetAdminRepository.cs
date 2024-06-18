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
	public class TimesheetAdminRepository
	{
		DB objDB;
		CommonRepository objComm;

		public TimesheetAdminRepository(IConfiguration iconfig)
		{
			objDB = new DB(iconfig);
			objComm = new CommonRepository(iconfig);
		}

		public List<TimesheetAdminDBModel> GetTimesheetList(int UserId, string? FromDate, string? ToDate, int PageIndex, int PageSize)
		{
			List<TimesheetAdminDBModel> objAdmTimesheet = new List<TimesheetAdminDBModel>();
			DataTable dtFirstTable = new DataTable();
			int totalRecord = 0;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
				   new SqlParameter("@UserId",UserId),
				   new SqlParameter("@FromDate",FromDate),
				   new SqlParameter("@ToDate",ToDate),
				   new SqlParameter("@PageIndex",PageIndex),
					new SqlParameter("@PageSize",PageSize)
				};
				DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_GetTimesheetDetails", param);
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
						TimesheetAdminDBModel objTimeList = new TimesheetAdminDBModel();
						objTimeList.TotalRecords = totalRecord;
						objTimeList.UserId = Convert.ToInt32(dr["UserId"].ToString().Trim());
						objTimeList.JiraId = dr["JiraId"].ToString().Trim();
						objTimeList.Description = dr["Description"].ToString().Trim();
						objTimeList.WorkedDate = dr["WorkedDate"].ToString().Trim();
						objTimeList.MinSpend = Convert.ToInt32(dr["minutesSpent"].ToString().Trim());
						objTimeList.Task = dr["Task"].ToString().Trim();
						objAdmTimesheet.Add(objTimeList);
					}
				}
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("TimesheetRepository", "GetTimesheetList", ex.Message, "");
			}

			return objAdmTimesheet;
		}
	}
}
