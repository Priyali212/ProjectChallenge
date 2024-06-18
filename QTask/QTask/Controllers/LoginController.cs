using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QTask.API;
using QTask.Models;
using QTaskDataLayer.Repository;

namespace QTask.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            try
            {
                if (Common.ValidateSession(HttpContext))
                {
                    return Redirect("/dashboard/dashboard");
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

                objComm.SaveErrorLog("LoginController", "Login", ex.Message, UserName);
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            try
            {
                if (Common.ValidateSession(HttpContext))
                {
                    return Redirect("/dashboard/dashboard");
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

                objComm.SaveErrorLog("LoginController", "ForgotPassword", ex.Message, UserName);
            }
            return View();
        }

        public IActionResult ConfirmPassword()
        {
            try
            {
                if (Common.ValidateSession(HttpContext))
                {
                    return Redirect("/dashboard/dashboard");
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

                objComm.SaveErrorLog("LoginController", "ConfirmPassword", ex.Message, UserName);
            }
            return View();
        }

        [HttpPost]
        public IActionResult ValidateLogin(LoginModel objLogin)
        {
            bool result = false;
            string msg = string.Empty;

            try
            {
                if (String.IsNullOrEmpty(objLogin.UserName))
                {
                    msg = "Enter UserName";
                }
                else
                    if (String.IsNullOrEmpty(objLogin.Password))
                {
                    msg = "Enter Password";
                }
                else
                {
                    LoginAPIController objLogApi = new LoginAPIController();
                    LoginResponseModel objResp = objLogApi.Post(objLogin);

                    //LoginRepository objLog = new LoginRepository();
                    //var LogDB = objLog.getLogin(objLogin.UserName);
                    //objLogin.Password = Common.encrypt(objLogin.Password);

                    if (objResp != null)
                    {
                        if (objResp.status)
                        {
                            bool IsContinueLogin = true;
                            if (Common.CheckConcurrentSession(objResp.id))
                            {
                                if (objLogin.Force == "yes")
                                {
                                    Global.Application.removeAll("app-" + objResp.id);
                                    IsContinueLogin = true;
                                }
                                else
                                {
                                    msg = "logged in";
                                    IsContinueLogin = false;
                                }

                            }
                            if (IsContinueLogin)
                            {
                                HttpContext.Session.SetString("UserSession", objResp.id);
                                HttpContext.Session.SetString("Name", objResp.first_name);
                                if (objResp.is_admin)
                                    HttpContext.Session.SetInt32("IsAdmin", 1);
                                else
                                    HttpContext.Session.SetInt32("IsAdmin", 0);
                                Global.Application.set(objResp.id, HttpContext.Session.Id);

                                List<UserAccessModel> objlstUsrAccess = objLogApi.GetUserWiseAccess(objResp.id);

                                if (objlstUsrAccess != null && objlstUsrAccess.Count > 0)
                                {
                                    HttpContext.Session.SetString("UserAccess", JsonConvert.SerializeObject(objlstUsrAccess));
                                }

                                result = true;
                            }
                        }
                        else
                        {
                            msg = objResp.MsgResponse;
                        }
                    }
                    else
                    {
                        msg = objResp.MsgResponse;
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

                objComm.SaveErrorLog("LoginController", "ValidateLogin", ex.Message, UserName);
            }
            return Json(new { result = result, msg = msg });
        }

        [HttpPost]
        public IActionResult CheckLogin(LoginResponseModel objResp)
        {
            bool result = false;
            string ReturnUrl = string.Empty;
            string msg = string.Empty;
            LoginAPIController objLogApi = new LoginAPIController();
            try
            {
                if (objResp != null)
                {
                    if (objResp.status)
                    {
                        bool IsContinueLogin = true;
                        if (Common.CheckConcurrentSession(objResp.id))
                        {
                            Global.Application.removeAll("app-" + objResp.id);
                            IsContinueLogin = true;

                        }
                        if (IsContinueLogin)
                        {
                            HttpContext.Session.SetString("UserSession", objResp.id);
                            HttpContext.Session.SetString("Name", objResp.first_name);
                            if (objResp.is_admin)
                                HttpContext.Session.SetInt32("IsAdmin", 1);
                            else
                                HttpContext.Session.SetInt32("IsAdmin", 0);
                            Global.Application.set("app-" + objResp.id, HttpContext.Session.Id);

                            

                            if (objResp.lstUserAccess != null && objResp.lstUserAccess.Count > 0)
                            {
                                HttpContext.Session.SetString("UserAccess", JsonConvert.SerializeObject(objResp.lstUserAccess));
                            }

                            result = true;
                            ReturnUrl = "/dashboard/dashboard";
                        }
                    }
                    else
                    {
                        msg = objResp.MsgResponse;
                    }
                }
                else
                {
                    msg = objResp.MsgResponse;
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

                objComm.SaveErrorLog("LoginController", "CheckLogin", ex.Message, UserName);
            }
            return Json(new { result = result, msg = msg, retUrl = ReturnUrl });
        }
    }
}
