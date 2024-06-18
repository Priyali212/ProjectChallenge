using Microsoft.AspNetCore.Mvc;
using QTask.Models;
using QTaskDataLayer.Repository;

namespace QTask.Controllers
{
    public class ContactController : Controller
    {        
        public IActionResult Contact()
        {
            ContactListModel objContactLst = null;
            try
            {
                if (HttpContext.Items["export"] != null && HttpContext.Items["export"].ToString().Trim() == "1")
                    ViewBag.Export = true;
                else
                    ViewBag.Export = false;

                if (HttpContext.Items["import"] != null && HttpContext.Items["import"].ToString().Trim() == "1")
                    ViewBag.Import = true;
                else
                    ViewBag.Import = false;

                if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
                    ViewBag.List = true;
                else
                    ViewBag.List = false;

                if (HttpContext.Items["view"] != null && HttpContext.Items["view"].ToString().Trim() == "1")
                    ViewBag.View = true;
                else
                    ViewBag.View = false;

                if (HttpContext.Items["massupdate"] != null && HttpContext.Items["massupdate"].ToString().Trim() == "1")
                    ViewBag.MassUpdate = true;
                else
                    ViewBag.MassUpdate = false;

                if (HttpContext.Items["delete"] != null && HttpContext.Items["delete"].ToString().Trim() == "1")
                    ViewBag.Delete = true;
                else
                    ViewBag.Delete = false;

                if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
                    ViewBag.Edit = true;
                else
                    ViewBag.Edit = false;

                // RolesManagementAPIController objAPIRoleManage = new RolesManagementAPIController();
                //objContactLst = GetUserList(1, 20);
                //ViewBag.SalesManagetList = objAPIRoleManage.GetRoleWiseUserList("Sales Manager");
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                if (HttpContext.Session.Get("UserName") != null && HttpContext.Session.Get("UserName").ToString().Trim() != "")
                    UserName = HttpContext.Session.Get("UserName").ToString().Trim();


                objComm.SaveErrorLog("ContactController", "Contact", ex.Message, UserName);
            }

            return View(objContactLst);
        }


        public IActionResult cc(CreateUserModal objUser)
        {
            //CreateUserModal objUser = new CreateUserModal();
            //objUser.lstEmail = new List<CreateUserEmailModal>();
            //objUser.lstAddress = new List<CreateUserAddressModal>();
            try
            {
                if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString() == "1")
                {
                    //if (!String.IsNullOrEmpty(Id))
                    //{
                    //    ContactAPIController objAPIContact = new ContactAPIController();
                    // objUser = objAPIContact.GetUserDetails(Id);
                    //}

                    //RolesManagementAPIController objAPIRoleManage = new RolesManagementAPIController();
                    //ViewBag.SalesManagetList = objAPIRoleManage.GetRoleWiseUserList("Sales Manager");

                    string[] PreFix = new string[] { "Mr-Mr.", "Ms-Ms.", "Mrs-Mrs.", "Miss-Miss.", "Dr-Dr.", "Prof-Prof." };
                    ViewBag.PreFix = PreFix;

                    string[] TypeOfEmail = new string[] { "personal-Personal", "Official-Official", "Other-Other" };
                    ViewBag.TypeOfEmail = TypeOfEmail;

                    string[] OptIn = new string[] { "1-Yes", "2-No" };
                    ViewBag.OptIn = OptIn;
                }
                else
                {
                    return Redirect("/error/error");
                }

            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                if (HttpContext.Session.Get("UserSession") != null && HttpContext.Session.Get("UserSession").ToString().Trim() != "")
                    UserName = HttpContext.Session.Get("UserSession").ToString().Trim();

                objComm.SaveErrorLog("ContactController", "_CreateUser", ex.Message, UserName);
            }
            return PartialView(objUser);
        }
    }
}
