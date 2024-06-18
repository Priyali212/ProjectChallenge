using Microsoft.Extensions.Configuration;
using QTask.Models;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.DBOperations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.DirectoryServices.AccountManagement;


namespace QTaskDataLayer.Repository
{
	public class LoginRepository
	{
		DB objDB;
		CommonRepository objComm;
		string LDAPDirectory;
		public LoginRepository(IConfiguration _config)
		{
			objDB = new DB(_config);
			objComm = new CommonRepository(_config);
			LDAPDirectory = _config.GetSection("AppSettings")["LDAPDirectory"].ToString();
		}

		public LoginResponseDBModel getLogin(string UserName)
		{
			LoginResponseDBModel objDBModel = new LoginResponseDBModel();
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@UserName",UserName)
				};
				DataSet dsUser = objDB.getDataFromDBToDataSet("Q_Pr_ValidateLogin", param);

				if (dsUser != null && dsUser.Tables[0] != null && dsUser.Tables[0].Rows.Count > 0)
				{
					objDBModel.UserName = dsUser.Tables[0].Rows[0]["user_name"].ToString().Trim();
					objDBModel.first_name = dsUser.Tables[0].Rows[0]["first_name"].ToString().Trim();
					objDBModel.last_name = dsUser.Tables[0].Rows[0]["last_name"].ToString().Trim();
					objDBModel.is_admin = Convert.ToBoolean(dsUser.Tables[0].Rows[0]["is_admin"]);
					objDBModel.receive_notifications = Convert.ToBoolean(dsUser.Tables[0].Rows[0]["receive_notifications"]);
					objDBModel.title = dsUser.Tables[0].Rows[0]["title"].ToString().Trim();
					objDBModel.department = dsUser.Tables[0].Rows[0]["department"].ToString().Trim();
					objDBModel.phone_mobile = dsUser.Tables[0].Rows[0]["phone_mobile"].ToString().Trim();
					objDBModel.Activestatus = dsUser.Tables[0].Rows[0]["status"].ToString().Trim();
					objDBModel.reports_to_id = dsUser.Tables[0].Rows[0]["reports_to_id"].ToString().Trim();
					objDBModel.is_group = dsUser.Tables[0].Rows[0]["is_group"].ToString().Trim();
					objDBModel.id = dsUser.Tables[0].Rows[0]["Id"].ToString().Trim();
					objDBModel.Password = dsUser.Tables[0].Rows[0]["user_hash"].ToString().Trim();
				}

			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("LoginRepository", "getLogin", ex.Message, UserName);
			}
			return objDBModel;
		}

		public UserDetailsDB GetUserDetails(string UserName)
		{
			UserDetailsDB objUserDetails = new UserDetailsDB();
			try
			{
				DataSet ds = new DataSet();

				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@UserName",UserName)
				};

				ds = objDB.getDataFromDBToDataSet("Q_Pr_GetQTaskUser", param);

				if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
				{
					objUserDetails.Id = ds.Tables[1].Rows[0]["ID"].ToString().Trim();
					objUserDetails.UserName = ds.Tables[1].Rows[0]["UserName"].ToString().Trim();
					objUserDetails.UserFullName = ds.Tables[1].Rows[0]["UserFullName"].ToString().Trim();
					objUserDetails.IsAdmin = (ds.Tables[1].Rows[0]["IsAdmin"] != null && ds.Tables[1].Rows[0]["IsAdmin"].ToString().Trim() == "1") ? true : false;
				}
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("LoginRepository", "GetUserDetails", ex.Message, UserName);
			}
			return objUserDetails;
		}

		public bool ValidateUser(string UserName, string Password)
		{
			bool Result = false;
			try
			{
				if (UserName != null && UserName.Trim() != "" && Password != null && Password.Trim() != "")
				{
					Regex rgHtml = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");

					if (rgHtml.IsMatch(UserName.Trim()))
						return false;

					if (rgHtml.IsMatch(Password.Trim()))
						return false;

					string LDAPServer = LDAPDirectory;

					foreach (string Server in LDAPServer.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
					{
						using (PrincipalContext context = new PrincipalContext(ContextType.Domain, Server))
						{
							if (context.ValidateCredentials(UserName, Password))
							{
								using (UserPrincipal userdetails = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, UserName))
								{
									if (userdetails != null)
									{
										Result = true;
										break;
									}
								}
							}
						}
					}

				}
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("LoginRepository", "ValidateUser", ex.Message, UserName);
			}
			return Result;
		}

		public List<UserAccessDBModel> getUserAccess(string UserId)
		{
			DataTable dt = new DataTable();
			List<UserAccessDBModel> objUserAccessList = new List<UserAccessDBModel>();
			try
			{
				SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@UserId",UserId)
				};
				dt = objDB.getDataFromDBToDataSet("Q_Pr_GetUserWiseAccess", param).Tables[0];

				if (dt != null && dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						UserAccessDBModel objUserAccess = new UserAccessDBModel();
						objUserAccess.UserID = dr["UserId"].ToString();
						objUserAccess.RoleId = dr["GroupId"].ToString();
						objUserAccess.ActionId = dr["AccessStatusId"].ToString();
						objUserAccess.RoleName = dr["RoleName"].ToString();
						//objUserAccess.AccessOverride = dr["Access_override"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Access_override"].ToString().Trim());
						objUserAccess.AccessName = dr["AccessName"].ToString().Trim();
						//objUserAccess.Category = dr["Category"].ToString().Trim();
						objUserAccess.AclType = dr["AccessStatusId"].ToString().Trim();
						objUserAccess.Access = dr["AccessStatusName"].ToString().Trim();
						objUserAccess.PageId = dr["PageId"].ToString().Trim();
						objUserAccess.PageName = dr["PageName"].ToString().Trim();

						objUserAccessList.Add(objUserAccess);
					}
				}
			}
			catch (Exception ex)
			{
				objComm.SaveErrorLog("LoginRepository", "getUserAccess", ex.Message, UserId);
			}
			return objUserAccessList;
		}
	}
}
