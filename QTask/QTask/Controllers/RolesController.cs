using Microsoft.AspNetCore.Mvc;
using QTask.API;
using QTask.Models;
using QTaskDataLayer.Repository;
using System.Xml;

namespace QTask.Controllers
{
	public class RolesController : Controller
	{
        public IActionResult Roles()
        {
            try
            {
                if ((HttpContext.Items["view"] != null && HttpContext.Items["view"].ToString().Trim() != "" && Convert.ToInt32(HttpContext.Items["view"].ToString()) == 1) || HttpContext.Session.GetInt32("IsAdmin") == 1)
                {
                    if (HttpContext.Items["export"] != null && HttpContext.Items["export"].ToString().Trim() == "1")
                        ViewBag.Export = true;
                    else
                        ViewBag.Export = false;

                    if (HttpContext.Items["view"] != null && HttpContext.Items["view"].ToString().Trim() == "1")
                        ViewBag.View = true;
                    else
                        ViewBag.View = false;

                    if (HttpContext.Items["delete"] != null && HttpContext.Items["delete"].ToString().Trim() == "1")
                        ViewBag.Delete = true;
                    else
                        ViewBag.Delete = false;

                    if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
                        ViewBag.Edit = true;
                    else
                        ViewBag.Edit = false;
                }
                else
                {
                    Response.Redirect("/error/error");
                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;
                if (HttpContext.Session?.GetString("UserSession") != null && HttpContext.Session?.GetString("UserSession") != string.Empty)
                {
                    UserName = HttpContext.Session?.GetString("UserSession");
                }

                objComm.SaveErrorLog("RolesController", "Roles", ex.Message, UserName);
            }
            return View();
        }

        public IActionResult _RolesAccessList(List<RolesModel> lstRolesModel)
        {
            try
            {
                string empty = string.Empty;
                //RolesManagementAPIController objRolesManageAPi = new RolesManagementAPIController();
                //List<RolesModel> objLstRoles = objRolesManageAPi.Get();

                //if(objLstRoles != null && objLstRoles.Count>0)
                //{
                //    ViewBag.Roles = objLstRoles;
                //}
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;
                if (HttpContext.Session?.GetString("UserSession") != null && HttpContext.Session?.GetString("UserSession") != string.Empty)
                {
                    UserName = HttpContext.Session?.GetString("UserSession");
                }

                objComm.SaveErrorLog("RolesController", "Roles", ex.Message, UserName);
            }

            return PartialView(lstRolesModel);
        }

        public IActionResult _RoleWiseAccessList(string RoleId)
        {
            try
            {
                if ((HttpContext.Items["view"] != null && HttpContext.Items["view"].ToString().Trim() != "" && Convert.ToInt32(HttpContext.Items["view"].ToString()) == 1) || HttpContext.Session.GetInt32("IsAdmin") == 1)
                {
                    RolesManagementAPIController objRolesApi = new RolesManagementAPIController();
                    XmlDocument xDoc = objRolesApi.GetRoleWiseAccessList(RoleId);

                    if (xDoc != null)
                    {
                        ViewBag.RolesAccessData = xDoc;
                        ViewBag.RoleId = RoleId;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;
                if (HttpContext.Session?.GetString("UserSession") != null && HttpContext.Session?.GetString("UserSession") != string.Empty)
                {
                    UserName = HttpContext.Session?.GetString("UserSession");
                }

                objComm.SaveErrorLog("RolesController", "Roles", ex.Message, UserName);
            }

            return PartialView();
        }

    }
}
