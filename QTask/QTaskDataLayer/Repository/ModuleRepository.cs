using Microsoft.Extensions.Configuration;
using QTaskDataLayer.DBOperations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTaskDataLayer.DBModel;

namespace QTaskDataLayer.Repository
{
	public class ModuleRepository
	{
		DB objDB;
		CommonRepository objcomm;
		public ModuleRepository(IConfiguration _config)
		{
			objDB = new DB(_config);
			objcomm = new CommonRepository(_config);
		}

		public List<ModuleDBModel> GetModuleList(string keyword = null)
		{
			List<ModuleDBModel> objlstDBModule = new List<ModuleDBModel>();
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@Keyword",keyword)
				};
				DataTable dt = objDB.getDataFromDBToDataSet("Q_Pr_GetTaskModuleList", param).Tables[0];

				if (dt != null && dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						ModuleDBModel objDBModule = new ModuleDBModel();
						objDBModule.ModuleId = Convert.ToInt32(dr["ID"].ToString().Trim());
						objDBModule.ModuleName = dr["PageName"].ToString().Trim();
						objlstDBModule.Add(objDBModule);
					}
				}

			}
			catch (Exception ex)
			{
				objcomm.SaveErrorLog("ModuleRepository", "GetModuleList", ex.Message, "");
			}
			return objlstDBModule;
		}

		public bool SaveModule(ModuleDBModel objDBModule, string UserName, string IPAddress, string BrowserDetails)
		{
			bool Result = false;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@ModuleName",objDBModule.ModuleName),
					new SqlParameter("@UserName",UserName),
					new SqlParameter("@IPAddress",IPAddress),
					new SqlParameter("@BrowserDetails",BrowserDetails)
				};

				int Res = objDB.InsertData("Q_Pr_SaveTaskModule", param);
				if (Res > 0)
					Result = true;
			}
			catch (Exception ex)
			{
				objcomm.SaveErrorLog("ModuleRepository", "SaveModule", ex.Message, UserName);
			}

			return Result;
		}

		public bool DeleteModule(int ModuleId, string UserName, string IPAddress, string BrowserDetails)
		{
			bool Result = false;
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@ModuleId",ModuleId),
					new SqlParameter("@UserName",UserName),
					new SqlParameter("@IPAddress",IPAddress),
					new SqlParameter("@BrowserDetails",BrowserDetails)
				};

				int Res = objDB.DeleteData("Q_Pr_DeleteTaskModule", param);

				if (Res > 0)
					Result = true;
			}
			catch (Exception ex)
			{
				objcomm.SaveErrorLog("ModuleRepository", "DeleteModule", ex.Message, UserName);
			}

			return Result;
		}
	}
}
