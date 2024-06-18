using Microsoft.Extensions.Configuration;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.DBOperations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.Repository
{
    public class GenerateReportRepository
    {

        DB objDB;
        CommonRepository objComm;

        public GenerateReportRepository(IConfiguration iconfig)
        {
            objDB = new DB(iconfig);
            objComm = new CommonRepository(iconfig);
        }

        public GenerateReportDBModel GenerateReports(int UserId, string? FromDate, string? ToDate)
        {
			//List<GenerateReportDBModel> objAdmTimesheet = new List<GenerateReportDBModel>();
			GenerateReportDBModel objTimeList = new GenerateReportDBModel();

			DataTable dtFirstTable = new DataTable();
            int totalRecord = 0;
            int OpenTask = 0;
            int ClosedTask = 0;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@UserId",UserId),
                   new SqlParameter("@FromDate",FromDate),
                   new SqlParameter("@ToDate",ToDate),
                  
                };
                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_GenerateReport", param);
                dtFirstTable = ds.Tables[0];
                objTimeList.totalRecord = dtFirstTable.Rows.Count;
				objTimeList.resourcesname = dtFirstTable.Rows[0]["resourcesname"].ToString().Trim();
			    objTimeList.OpenTask = dtFirstTable.Select("Status in ('In Progress','Live','UAT')").Length;
				objTimeList.ClosedTask = dtFirstTable.Select("Status in ('Completed')").Length;



			}
            catch (Exception ex)
            {
                objComm.SaveErrorLog("GenerateReportRepository", "GenerateReports", ex.Message, "");
            }

            return objTimeList;
        }

		public DataTable GenerateReportsExport(int UserId, string? FromDate, string? ToDate)
		{
			
			DataTable dtFirstTable = new DataTable();
			int totalRecord = 0;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
				   new SqlParameter("@UserId",UserId),
				   new SqlParameter("@FromDate",FromDate),
				   new SqlParameter("@ToDate",ToDate),

				};
				DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_GenerateReport", param);
				dtFirstTable = ds.Tables[0];

		
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("GenerateReportRepository", "GenerateReports", ex.Message, "");
			}

			return dtFirstTable;
		}
	}
}
