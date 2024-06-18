using Microsoft.AspNetCore.Http;
using QTask.Controllers;
using QTask.Global;
using QTask.Models;

namespace QTask.Middelware
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	//created Middleware for tokenbase authentication to access the api
	public class APIAuthenticationMiddleware
	{
		private readonly RequestDelegate _next;

		public APIAuthenticationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			string AccessPage = httpContext.Request.Path.Value.Trim().ToLower();
			try
			{
				AccessPage = AccessPage.Replace("http://", "").Replace("https://", "");
				AccessPage = AccessPage.Replace("/api/", "");
				if (AccessPage.IndexOf("/") > -1)
				{
					AccessPage = AccessPage.Substring(0, AccessPage.IndexOf("/")).Trim();
				}
                //httpContext.Items["isApi"] = "1";



                //if (AccessPage.Trim() == "login")  // open the QuantumWebsite data  
                if (AccessPage.Trim() != "loginapi")   //original code for CRM
				{
					AccessPage = AccessPage.Replace("api", "");
					if (!String.IsNullOrEmpty(httpContext.Request.Cookies["Token"]))
					{
						string DecToken = Common.QDecrypt(httpContext.Request.Cookies["token"].ToString().Trim());

						string[] SessionData = DecToken.Split(new string[] { "|#sep#|" }, StringSplitOptions.RemoveEmptyEntries);

						if (SessionData.Length > 0)
						{
							//TokenData objToken = Global.Token.get(SessionData[0].Trim());

							string BrowserDetails = httpContext.Request.Headers["User-Agent"].ToString();
							string IPAddress = httpContext.Connection.RemoteIpAddress.ToString();

							if (Global.Token.ValidateToken(SessionData[0].Trim(), SessionData[2].Trim(), BrowserDetails, IPAddress))
							{
								TokenData objToken = Global.Token.get(SessionData[0].Trim());

								if (objToken != null)
								{
									httpContext.Items["UserId"] = objToken.TokenId;
									httpContext.Items["export"] = "1";
									//httpContext.Items["import"] = "1";
									httpContext.Items["list"] = "1";
									httpContext.Items["view"] = "1";
									//httpContext.Items["massupdate"] = "1";
									httpContext.Items["delete"] = "1";
									httpContext.Items["edit"] = "1";
									//httpContext = SetAutharization(httpContext, objToken, AccessPage);
									//httpContext.Response.Headers["Token"] = RegenerateToken(objToken.TokenId, httpContext.Session.Id.ToString());
									await _next.Invoke(httpContext);
								}
								else
								{
									httpContext.Response.Clear();
									ErrorModel objError = new ErrorModel();

									objError.ErrorMessage = "UnAuthorized";
									//objResp.MsgResponse = "Invalid Session Data";
									httpContext.Response.WriteAsJsonAsync<ErrorModel>(objError);
								}
							}
							else
							{
								httpContext.Response.Clear();
								ErrorModel objError = new ErrorModel();

								objError.ErrorMessage = "UnAuthorized";
								//objResp.MsgResponse = "Invalid Session Data";
								httpContext.Response.WriteAsJsonAsync<ErrorModel>(objError);
							}
						}
						else
						{
							httpContext.Response.Clear();
							ErrorModel objError = new ErrorModel();

							objError.ErrorMessage = "UnAuthorized";
							//objResp.MsgResponse = "Invalid Session Data";
							httpContext.Response.WriteAsJsonAsync<ErrorModel>(objError);
						}
					}
					else
					{
						httpContext.Response.Clear();
						ErrorModel objError = new ErrorModel();

						objError.ErrorMessage = "UnAuthorized";
						//objResp.MsgResponse = "Invalid Session Data";
						httpContext.Response.WriteAsJsonAsync<ErrorModel>(objError);
					}
				}
				else
				{
					//httpContext.Items["UserId"] = "1"; //Temp code need to be removed
					await _next(httpContext);
				}
			}
			catch (Exception ex)
			{
				//httpContext.Response.Clear();
				//ErrorModel objError = new ErrorModel();

				//objError.ErrorMessage = "UnAuthorized";
				//objResp.MsgResponse = "Invalid Session Data";
				//httpContext.Response.WriteAsJsonAsync<ErrorModel>(objError);
				//await _next(httpContext);
			}

		}

		public string RegenerateToken(string TokenId, string SessionId)
		{
			return Global.Token.GenerateNewTokenId(TokenId, SessionId);
		}

		public HttpContext SetAutharization(HttpContext httpContext, TokenData objToken, string AccessPage)
		{	
			if (objToken.IsAdmin)
			{
				httpContext.Items["export"] = "1";
				//httpContext.Items["import"] = "1";
				httpContext.Items["list"] = "1";
				httpContext.Items["view"] = "1";
				//httpContext.Items["massupdate"] = "1";
				httpContext.Items["delete"] = "1";
				httpContext.Items["edit"] = "1";

				//httpContext.Response.Headers["Token"] = RegenerateToken(objToken.TokenId, httpContext.Session.Id.ToString());
			}
			else
			{
				List<UserAccessModel> objLstUserAccess = objToken.lstUserAcceessModel;

				if (objLstUserAccess != null)
				{
                    //Checking whether user is authorized to access requested page?
                    var UsrAccess = objLstUserAccess.Where(x => x.PageName.ToLower() == AccessPage.ToLower() && x.AccessName.ToLower() == "view" && x.Access.ToLower() == "enabled");

                    if (UsrAccess.Count() > 0)
					{
						//if user is authorized then will redirect to requested page


						//get access types for users
                        var listAccess = objLstUserAccess.Where(x => x.PageName.ToLower() == AccessPage.ToLower());

                        if (listAccess.Count() > 0)
						{
							//0 means no access, it is default value                                    
							httpContext.Items["export"] = "0";
							//httpContext.Items["import"] = "0";
							httpContext.Items["list"] = "0";
							httpContext.Items["view"] = "0";
							//httpContext.Items["massupdate"] = "0";
							httpContext.Items["delete"] = "0";
							httpContext.Items["edit"] = "0";

							//base on available data in list, will update value of access to 1 i.e. having access
							foreach (UserAccessModel objAcc in listAccess)
							{
								switch (objAcc.AccessName.ToLower().Trim())
								{
									case "export":
										httpContext.Items["export"] = "1";
										break;
									case "import":
										httpContext.Items["import"] = "1";

										break;
									case "list":
										httpContext.Items["list"] = "1";
										break;
									case "view":
										httpContext.Items["view"] = "1";
										break;
									case "massupdate":
										httpContext.Items["massupdate"] = "1";
										break;
									case "delete":
										httpContext.Items["delete"] = "1";
										break;
									case "edit":
										httpContext.Items["edit"] = "1";
										break;
									default:
										break;
								}
							}
						}
						//httpContext.Response.Headers["Token"] = RegenerateToken(objToken.TokenId, httpContext.Session.Id.ToString());
					}
					else
					{
						//if user is not authorised then will redirect to error page
						httpContext.Response.Clear();
						LoginResponseModel objResp = new LoginResponseModel();
						objResp.status = false;
						//objResp.MsgResponse = "UnAuthorized";
						objResp.MsgResponse = "Data Not Found";
						httpContext.Response.WriteAsJsonAsync<LoginResponseModel>(objResp);
					}
				}

			}
			return httpContext;
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class APIAuthenticationMiddlewareExtensions
	{
		public static IApplicationBuilder UseAPIAuthenticationMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<APIAuthenticationMiddleware>();
		}
	}
}