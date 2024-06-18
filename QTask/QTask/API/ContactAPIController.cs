using Microsoft.AspNetCore.Mvc;
using QTask.Models;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.Repository;
using System.Text.Json;
using QTask.Controllers;

namespace QTask.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactAPIController : ControllerBase
    {
        [HttpGet]
        [Route("getuser")]
        public ContactListModel GetUser(string Name = null, int PageIndex = 0, int PageSize = 10)
        {
            ContactListModel objContact = new ContactListModel();
            objContact.contactLists = new List<ContactList>();
            try
            {
                if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
                {
                    ContactRepository objContactRepo = new ContactRepository(Common.config);
                    var ContactUserList = objContactRepo.GetUserList(Name, PageIndex, PageSize);

                    if (ContactUserList != null && ContactUserList.contactLists.Count > 0)
                    {
                        objContact.TotalCount = ContactUserList.TotalCount;
                        objContact.PageIndex = ContactUserList.PageIndex;

                        foreach (var conLst in ContactUserList.contactLists)
                        {
                            ContactList objlstContact = new ContactList();

                            objlstContact.ID = conLst.ID;
                            objlstContact.UserName = conLst.UserName;
                            objlstContact.UserFullName = conLst.UserFullName;
                            objlstContact.IsAdmin = conLst.IsAdmin;
                            objlstContact.GroupName = conLst.GroupName;

                            objContact.contactLists.Add(objlstContact);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                objComm.SaveErrorLog("ContactAPIController", "GetUser", ex.Message, UserName);
            }

            return objContact;
        }

        [HttpGet]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int Id)
        {
            bool status = false;
            string Msg = string.Empty;
            try
            {
                if (HttpContext.Items["delete"] != null && HttpContext.Items["delete"].ToString().Trim() == "1")
                {
                    //if (objlstDeleteUsers.UserName != null && objlstDeleteUsers.UserName.ToString().Trim() != "")
                    //{
                    DeleteUserDBModel objUserDBModel = new DeleteUserDBModel();


                    if (Id != null && Id > 0)
                    {

                        ContactRepository objContactRepo = new ContactRepository(Common.config);
                        status = objContactRepo.DeleteUsers(Id, HttpContext.Items["UserId"].ToString().Trim());
                        if (status)
                            Msg = "Users are Deleted";
                        else
                            Msg = "Users are not Deleted";
                    }
                    else
                    {
                        Msg = "Users list empty";
                    }

                }
                else
                {
                    Msg = "Access Denied";
                }
            }
            catch (Exception ex)
            {
                Msg = "Users are not deleted";
                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                objComm.SaveErrorLog("ContactAPIController", "DeleteUser", ex.Message, UserName);
            }
            return new JsonResult(new { status = status, Msg = Msg }, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost]
        [Route("CreateUsers")]
        public IActionResult CreateUsers([FromBody] CreateUserModal objCreateUser)
        {
            bool status = false;
            string msg = string.Empty;
            try
            {
                if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
                {

                    CreateUserDBModal objCreateUsrDB = new CreateUserDBModal();

                    objCreateUsrDB.UserName = objCreateUser.UserName;
                    objCreateUsrDB.UserFullName = objCreateUser.UserFullName;
                    objCreateUsrDB.ID = objCreateUser.ID;
                    objCreateUsrDB.GroupId = objCreateUser.GroupId;

                    string UserId = HttpContext.Items["UserId"].ToString();

                    string IPAddress = CommonAPI.getIPAddress(HttpContext);
                    string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);

                    ContactRepository objContRepo = new ContactRepository(Common.config);

                    status = objContRepo.CreateUsers(objCreateUsrDB, UserId, IPAddress, BrowserDetails);

                    if (status)
                        msg = "User Created Successfully";
                }
                else
                {
                    msg = "Access Denied";
                }
            }
            catch (Exception ex)
            {
                msg = "User is not added";
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;

                objComm.SaveErrorLog("ContactAPIController", "DeleteUser", ex.Message, UserName);
            }

            return new JsonResult(new { status = status, Msg = msg }, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }


        [HttpGet]
        [Route("GetUserDetails")]
        public CreateUserModal GetUserDetails(string Id)
        {
            CreateUserModal objCreateUsr = new CreateUserModal();
            try
            {
                if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
                {
                    ContactRepository objContRep = new ContactRepository(Common.config);

                    var ObjUser = objContRep.GetUserDetails(Id);

                    if (ObjUser != null)
                    {
                        objCreateUsr.ID = ObjUser.ID;
                        objCreateUsr.UserName = ObjUser.UserName;
                        objCreateUsr.UserFullName = ObjUser.UserFullName;
                        objCreateUsr.IsAdmin = ObjUser.IsAdmin;
                        objCreateUsr.GroupId = ObjUser.GroupId;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;

                objComm.SaveErrorLog("ContactAPIController", "GetUserDetails", ex.Message, UserName);
            }

            return objCreateUsr;
        }


        [HttpPost]
        [Route("UpdateUsers")]
        public IActionResult UpdateUsers(CreateUserModal objCreateUser)
        {
            bool status = false;
            string msg = string.Empty;
            try
			{
				if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
				{

					CreateUserDBModal objCreateUsrDB = new CreateUserDBModal();
                    objCreateUsrDB.UserName = objCreateUser.UserName;
                    objCreateUsrDB.UserFullName = objCreateUser.UserFullName;
                    objCreateUsrDB.ID = objCreateUser.ID;
                    objCreateUsrDB.GroupId = objCreateUser.GroupId;

                    string UserId = HttpContext.Items["UserId"].ToString();

                    string IPAddress = CommonAPI.getIPAddress(HttpContext);
                    string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);


                    ContactRepository objContRepo = new ContactRepository(Common.config);

                    status = objContRepo.UpdateUsers(objCreateUsrDB, UserId, IPAddress, BrowserDetails);

                    if (status)
                        msg = "User Updated Successfully";

                    else
                       msg = "UserName should not be blank";
                }
                else
                {
                    msg = "Access Denied";
                }
            }
            catch (Exception ex)
            {
                msg = "User is not added";
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;

                objComm.SaveErrorLog("ContactAPIController", "UpdateUsers", ex.Message, UserName);
            }

            return new JsonResult(new { status = status, Msg = msg }, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
    }
}
