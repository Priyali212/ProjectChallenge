using Microsoft.AspNetCore.Mvc;
using QTask.Controllers;
using QTask.Models;
using QTaskDataLayer.Repository;

namespace QTask.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginAPIController : ControllerBase
	{
		[HttpPost]
		public LoginResponseModel Post([FromBody] LoginModel objLog)
		{
			LoginResponseModel objResp = new LoginResponseModel();
			try
			{
				LoginRepository objLogRepo = new LoginRepository(Common.config);

				var objUserDetails = objLogRepo.GetUserDetails(objLog.UserName);
				if (objUserDetails != null)
				{
					if (objLogRepo.ValidateUser(objLog.UserName, objLog.Password))
					{
						objResp.status = true;
						objResp.id = objUserDetails.Id;
						objResp.UserName = objUserDetails.UserName;
						// objResp.Password = LogDB.Password;
						objResp.first_name = objUserDetails.UserFullName;
						objResp.last_name = "";
						objResp.is_admin = objUserDetails.IsAdmin;
						objResp.receive_notifications = false;
						objResp.title = "";
						objResp.department = "";
						objResp.phone_mobile = "";
						objResp.Activestatus = "Active";
						objResp.reports_to_id = "";
						objResp.is_group = "";
						objResp.UserSession = objUserDetails.Id;
						objResp.Photo = "";

						string TimeStamp = DateTime.Now.ToString("ddMMyyyyhhmmss");

						HttpContext.Session.Clear();
						string SessionId = HttpContext.Session.Id;
						string BrowserDetails = HttpContext.Request.Headers["User-Agent"].ToString();
						string IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();

						var Response = new HttpResponseMessage();
						string TokenKey = Common.QEncrypt(objResp.id + "|#sep#|" + TimeStamp + "|#sep#|" + SessionId);

						var UserAccess = objLogRepo.getUserAccess(objResp.id);
						List<UserAccessModel> objLstUsrAccess = new List<UserAccessModel>();
						if (UserAccess != null)
						{

							for (int i = 0; i < UserAccess.Count; i++)
							{
								UserAccessModel objModUserAccess = new UserAccessModel();

								objModUserAccess.UserID = UserAccess[i].UserID.ToString();
								objModUserAccess.RoleId = UserAccess[i].RoleId;
								objModUserAccess.ActionId = UserAccess[i].ActionId.ToString();
								objModUserAccess.RoleName = UserAccess[i].RoleName.ToString();
								objModUserAccess.AccessOverride = UserAccess[i].AccessOverride;
								objModUserAccess.AccessName = UserAccess[i].AccessName;
								objModUserAccess.Category = UserAccess[i].Category;
								objModUserAccess.AclType = UserAccess[i].AclType;
								objModUserAccess.Access = UserAccess[i].Access;
								objModUserAccess.PageId = UserAccess[i].PageId;
								objModUserAccess.PageName = UserAccess[i].PageName;

								objLstUsrAccess.Add(objModUserAccess);
							}
						}
						objResp.lstUserAccess = objLstUsrAccess;

                        Global.Token.removeAll(objResp.id);
						Global.Token.set(objResp.id, objResp.UserName, SessionId, objLstUsrAccess, objResp.is_admin, BrowserDetails, IPAddress);
						objResp.Token = TokenKey;

						//Cookie objCookie = new Cookie("Token", TokenKey);'
						CookieOptions objCookOpt = new CookieOptions();
						//objCookOpt.Domain = HttpContext.Request.Host.ToString();
						objCookOpt.Path = "/api";
						objCookOpt.Secure = true;
						objCookOpt.HttpOnly = true;
						HttpContext.Response.Cookies.Append("Token", TokenKey, objCookOpt);
					}
					else
					{
						objResp.MsgResponse = "Please enter correct password";
						objResp.status = false;
					}
				}
				else
				{
					objResp.MsgResponse = "User Not Exist";
					objResp.status = false;
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);

				string UserName = string.Empty;
				if (objLog != null)
					UserName = objLog.UserName;

				objComm.SaveErrorLog("LoginAPIController", "ValidateLogin", ex.Message, UserName);

			}

			return objResp;
		}


		[HttpGet]
		public List<UserAccessModel> GetUserWiseAccess(string UserId)
		{
			List<UserAccessModel> objLstUsrAccess = new List<UserAccessModel>();
			try
			{
				if (!String.IsNullOrEmpty(UserId))
				{
					LoginRepository objLogRepo = new LoginRepository(Common.config);
					var UserAccess = objLogRepo.getUserAccess(UserId);

					if (UserAccess != null)
					{
						for (int i = 0; i < UserAccess.Count; i++)
						{
							UserAccessModel objModUserAccess = new UserAccessModel();

							objModUserAccess.UserID = UserAccess[i].UserID.ToString();
							objModUserAccess.RoleId = UserAccess[i].RoleId;
							objModUserAccess.ActionId = UserAccess[i].ActionId.ToString();
							objModUserAccess.RoleName = UserAccess[i].RoleName.ToString();
							objModUserAccess.AccessOverride = UserAccess[i].AccessOverride;
							objModUserAccess.AccessName = UserAccess[i].AccessName;
							objModUserAccess.Category = UserAccess[i].Category;
							objModUserAccess.AclType = UserAccess[i].AclType;
							objModUserAccess.Access = UserAccess[i].Access;
							objModUserAccess.PageId = UserAccess[i].PageId;
							objModUserAccess.PageName = UserAccess[i].PageName;

							objLstUsrAccess.Add(objModUserAccess);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("LoginAPIController", "GetUserWiseAccess", ex.Message, UserId);
			}

			return objLstUsrAccess;
		}
	}
}
