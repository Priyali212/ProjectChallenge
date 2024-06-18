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
using static QTaskDataLayer.DBModel.GenerateReportDBModel;

namespace QTaskDataLayer.Repository
{
    public class CheckStatusRepository
    {

        DB objDB;
        CommonRepository objComm;


        public CheckStatusRepository(IConfiguration iconfig)
        {
            objDB = new DB(iconfig);
            objComm = new CommonRepository(iconfig);
        }


        public CheckStatusDBModel CheckStatus(int UserId, string? FromDate, string? ToDate)
        {
            //List<GenerateReportDBModel> objAdmTimesheet = new List<GenerateReportDBModel>();
            CheckStatusDBModel objcheckList = new CheckStatusDBModel();

            DataTable dtFirstTable = new DataTable();
            //int totalRecord = 0;
            //int OpenTask = 0;
            //int ClosedTask = 0;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@UserId",UserId),
                   new SqlParameter("@FromDate",FromDate),
                   new SqlParameter("@ToDate",ToDate),

                };
                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_CheckStatus", param);
                dtFirstTable = ds.Tables[0];
                objcheckList.resourcesname = dtFirstTable.Rows[0]["resourcesname"].ToString().Trim();
                //objcheckList.WorkedDate = dtFirstTable.Rows[0]["WorkedDate"].ToString().Trim();
				



			}
            catch (Exception ex)
            {
                objComm.SaveErrorLog("GenerateReportRepository", "GenerateReports", ex.Message, "");
            }

            return objcheckList;
        }
    }
}
