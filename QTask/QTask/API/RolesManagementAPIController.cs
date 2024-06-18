using Microsoft.AspNetCore.Mvc;
using QTask.Models;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.Repository;
using System.Text.Json;
using System.Xml;
using QTask.Controllers;

namespace QTask.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesManagementAPIController : ControllerBase
	{
		// GET: api/<RolesManagementAPIController>
		[HttpGet]
		public List<RolesModel> Get()
		{
			List<RolesModel> objLstRoles = new List<RolesModel>();
			try
			{
				if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
				{
					RolesManagementRepository objRolesRepo = new RolesManagementRepository(Common.config);
					var Roles = objRolesRepo.GetRolesList();

					if (Roles != null)
					{
						for (int i = 0; i < Roles.Count; i++)
						{
							RolesModel objRoles = new RolesModel();
							objRoles.RolesId = Roles[i].RolesId.ToString().Trim();
							objRoles.DateEntered = Convert.ToDateTime(Roles[i].DateEntered.ToString().Trim());
							objRoles.DateModified = Convert.ToDateTime(Roles[i].DateModified.ToString().Trim());
							objRoles.ModifiedBy = Roles[i].ModifiedBy.ToString().Trim();
							objRoles.CreatedBy = Roles[i].CreatedBy.ToString().Trim();
							objRoles.RoleName = Roles[i].RoleName.ToString().Trim();
							objRoles.RoleDescriptioin = Roles[i].RoleDescriptioin.ToString().Trim();
							objRoles.Deleted = Roles[i].Deleted;

							objLstRoles.Add(objRoles);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "Get", ex.Message, "");
			}

			return objLstRoles;
		}

		[HttpGet]
		[Route("GetRoleWiseAccessList")]
		public XmlDocument GetRoleWiseAccessList(string RoleId)
		{
			XmlDocument xDoc = null;
			try
			{
				//if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
				//{
				RolesManagementRepository objRolesRepo = new RolesManagementRepository(Common.config);
				xDoc = objRolesRepo.GetRoleWiseAccess(RoleId);
				//}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "GetRoleWiseAccessList", ex.Message, "");
			}

			return xDoc;
		}


		[HttpGet]
		[Route("GetRoleWiseAccessMetrixList")]
		public RoleModalaccess GetRoleWiseAccessMetrixList(string RoleId)
		{
			RoleModalaccess objRoleModalaccess = new RoleModalaccess();
			try
			{
				//if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
				//{
				RolesManagementRepository objRolesRepo = new RolesManagementRepository(Common.config);
				var objDBRoleModal = objRolesRepo.GetRoleWiseAccessData(RoleId);

				objRoleModalaccess.RoleId = objDBRoleModal.RoleId;
				List<RoleCategory> lstObjCategory = new List<RoleCategory>();

				if (objDBRoleModal.lstCategory != null && objDBRoleModal.lstCategory.Count() > 0)
				{
					foreach (var objDBRoleCategory in objDBRoleModal.lstCategory)
					{
						RoleCategory objCategory = new RoleCategory();

						objCategory.Id = Convert.ToInt32(objDBRoleCategory.Id);
						objCategory.CategoryName = objDBRoleCategory.CategoryName;

						lstObjCategory.Add(objCategory);
					}

					objRoleModalaccess.lstCategory = lstObjCategory;

				}

				if (objDBRoleModal.lstRoleAccess != null && objDBRoleModal.lstRoleAccess.Count() > 0)
				{
					objRoleModalaccess.lstRoleAccess = new List<RoleModalAccessName>();

					foreach (var objDBRoleAccess in objDBRoleModal.lstRoleAccess)
					{
						RoleModalAccessName objRoleAccess = new RoleModalAccessName();

						objRoleAccess.AccessId = objDBRoleAccess.AccessId;
						objRoleAccess.AccessName = objDBRoleAccess.AccessName;
						objRoleAccess.AccessFor = objDBRoleAccess.AccessFor;

						objRoleModalaccess.lstRoleAccess.Add(objRoleAccess);

					}

				}

				List<RoleAccessType> objlstAccessType = new List<RoleAccessType>();
				if (objDBRoleModal.lstAccessType != null && objDBRoleModal.lstAccessType.Count() > 0)
				{

					foreach (var objDBRoleAccessType in objDBRoleModal.lstAccessType)
					{
						RoleAccessType objRoleAccessType = new RoleAccessType();
						objRoleAccessType.Id = objDBRoleAccessType.Id;
						objRoleAccessType.AccessTypeName = objDBRoleAccessType.AccessTypeName;
						objlstAccessType.Add(objRoleAccessType);
					}

					objRoleModalaccess.lstAccessType = objlstAccessType;
				}
				if (objDBRoleModal.lstRoleWiseAccessData != null && objDBRoleModal.lstRoleWiseAccessData.Count() > 0)
				{
					objRoleModalaccess.lstRoleWiseAccessData = new List<RoleWiseAccessData>();

					foreach (var ObjDBRoleWiseAccess in objDBRoleModal.lstRoleWiseAccessData)
					{
						RoleWiseAccessData ObjRoleWiseAccess = new RoleWiseAccessData();

						ObjRoleWiseAccess.RoleId = ObjDBRoleWiseAccess.RoleId;
						ObjRoleWiseAccess.ActionId = ObjDBRoleWiseAccess.ActionId;
						ObjRoleWiseAccess.RoleName = ObjDBRoleWiseAccess.RoleName;
						ObjRoleWiseAccess.accessOverride = ObjDBRoleWiseAccess.accessOverride;
						ObjRoleWiseAccess.AccessName = ObjDBRoleWiseAccess.AccessName;
						ObjRoleWiseAccess.AccessCategory = ObjDBRoleWiseAccess.AccessCategory;
						ObjRoleWiseAccess.AclType = ObjDBRoleWiseAccess.AclType;
						ObjRoleWiseAccess.Access = ObjDBRoleWiseAccess.Access;
						ObjRoleWiseAccess.AccessId = ObjDBRoleWiseAccess.AccessId;
						ObjRoleWiseAccess.PageId = ObjDBRoleWiseAccess.PageId;

						objRoleModalaccess.lstRoleWiseAccessData.Add(ObjRoleWiseAccess);
					}

				}
				//}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "GetRoleWiseAccessMetrixList", ex.Message, "");
			}

			return objRoleModalaccess;
		}

		[HttpGet]
		[Route("GetRoleWiseUserList")]
		public List<RolesWiseUserListModel> GetRoleWiseUserList(string RoleName = null)
		{
			List<RolesWiseUserListModel> lstRolesWiseUserList = new List<RolesWiseUserListModel>();
			try
			{
				if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
				{
					RolesManagementRepository objRoleManage = new RolesManagementRepository(Common.config);
					var RoleWiseUserList = objRoleManage.GetRoleWiseUserList(RoleName);

					if (RoleWiseUserList != null)
					{
						for (int i = 0; i < RoleWiseUserList.Count; i++)
						{
							RolesWiseUserListModel objRoleWiseUsr = new RolesWiseUserListModel();

							objRoleWiseUsr.RoleId = RoleWiseUserList[i].RoleId.ToString().Trim();
							objRoleWiseUsr.Userid = RoleWiseUserList[i].Userid.ToString().Trim();
							objRoleWiseUsr.FirstName = RoleWiseUserList[i].FirstName.ToString().Trim();
							objRoleWiseUsr.LastName = RoleWiseUserList[i].LastName.ToString().Trim();
							objRoleWiseUsr.RoleName = RoleWiseUserList[i].RoleName.ToString().Trim();
							objRoleWiseUsr.RoleDescriptioin = RoleWiseUserList[i].RoleDescriptioin.ToString().Trim();

							lstRolesWiseUserList.Add(objRoleWiseUsr);
						}
					}
				}

			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "GetRoleWiseUserList", ex.Message, "");
			}

			return lstRolesWiseUserList;
		}

		[HttpPost]
		[Route("SaveAccessesForRole")]
		public JsonResult SaveAccessesForRole(RoleWiseAccesses objRoleWiseAccess)
		{
			bool Status = false;
			string Msg = string.Empty;
			try
			{
				if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
				{
					if (objRoleWiseAccess != null)
					{
						RoleWiseAccessesDB objRoleWiseAccessDB = new RoleWiseAccessesDB();

						objRoleWiseAccessDB.RoleId = objRoleWiseAccess.RoleId;
						objRoleWiseAccessDB.UserName = HttpContext.Items["UserId"].ToString();

						if (objRoleWiseAccess.lstRoleWiseAccess != null && objRoleWiseAccess.lstRoleWiseAccess.Count() > 0)
						{
							List<RoleDBWiseAccessData> lstRoleDBWiseAccessData = new List<RoleDBWiseAccessData>();

							foreach (RoleWiseAccessData objData in objRoleWiseAccess.lstRoleWiseAccess)
							{
								RoleDBWiseAccessData objDBData = new RoleDBWiseAccessData();
								objDBData.RoleId = objData.RoleId;
								objDBData.ActionId = objData.ActionId;
								objDBData.AccessId = objData.AccessId;
								objDBData.PageId = objData.PageId;

								lstRoleDBWiseAccessData.Add(objDBData);
							}
							objRoleWiseAccessDB.lstRoleDBWiseAccess = lstRoleDBWiseAccessData;
						}

						string IPAddress = CommonAPI.getIPAddress(HttpContext);
						string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);

						RolesManagementRepository objRoleRepo = new RolesManagementRepository(Common.config);

						Status = objRoleRepo.SaveRoleWiseAccess(objRoleWiseAccessDB, IPAddress, BrowserDetails);

					}
					else
					{
						Msg = "Data is not Provided";
					}
				}
				else
				{
					Msg = "Access Denied";
				}
			}
			catch (Exception ex)
			{
				Msg = "Error While Saving Access Data";
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "SaveAccessesForRole", ex.Message, "");
			}

			return new JsonResult(new { status = Status, msg = Msg }, new JsonSerializerOptions { PropertyNamingPolicy = null });
		}

		[HttpPost]
		[Route("SaveRole")]
		public JsonResult SaveRole(Role ObjRole)
		{
			bool Status = false;
			string Msg = string.Empty;

			try
			{
				if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
				{
					if (ObjRole != null)
					{
						if (ObjRole.RoleName != null && ObjRole.RoleName.Trim() != "")
						{
							RolesManagementRepository objRoleManageRepo = new RolesManagementRepository(Common.config);

							string IPAddress = CommonAPI.getIPAddress(HttpContext);
							string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);
							string UserName = HttpContext.Items["UserId"].ToString();

							var objRoleDB = new RoleDB();

							objRoleDB.RoleName = ObjRole.RoleName;
							objRoleDB.Description = ObjRole.Description;


							Status = objRoleManageRepo.SaveRole(objRoleDB, UserName, IPAddress, BrowserDetails);

							if (Status)
								Msg = "Role Is Created Successfully";
							else
								Msg = "Role is not Created";
						}
						else
						{
							Msg = "Role Name is not provided";
						}
					}
					else
					{
						Msg = "Role Data is not provided";
					}
				}
				else
				{
					Msg = "Access is Denied";
				}
			}
			catch (Exception ex)
			{
				Msg = "Error While Creating New Role";
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "SaveRole", ex.Message, "");
			}

			return new JsonResult(new { status = Status, msg = Msg }, new JsonSerializerOptions { PropertyNamingPolicy = null });
		}


		[HttpGet]
		[Route("DeleteRole")]
		public JsonResult DeleteRole(string RoleId)
		{
			bool Status = false;
			string Msg = string.Empty;

			try
			{
				if (HttpContext.Items["delete"] != null && HttpContext.Items["delete"].ToString().Trim() == "1")
				{
					if (RoleId != null)
					{

						RolesManagementRepository objRoleManageRepo = new RolesManagementRepository(Common.config);

						string IPAddress = CommonAPI.getIPAddress(HttpContext);
						string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);
						string UserName = HttpContext.Items["UserId"].ToString();


						Status = objRoleManageRepo.DeleteRole(RoleId, UserName, IPAddress, BrowserDetails);

						if (Status)
							Msg = "Role Is Deleted Successfully";
						else
							Msg = "Role is not Deleted";

					}
					else
					{
						Msg = "Role Id is not provided";
					}
				}
				else
				{
					Msg = "Access is Denied";
				}
			}
			catch (Exception ex)
			{
				Msg = "Error While Deleting New Role";
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("RolesManagementAPIController", "DeleteRole", ex.Message, "");
			}

			return new JsonResult(new { status = Status, msg = Msg }, new JsonSerializerOptions { PropertyNamingPolicy = null });
		}
	}
}
