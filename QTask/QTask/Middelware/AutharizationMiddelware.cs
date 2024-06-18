using Newtonsoft.Json;
using QTask.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QTask.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Configuration;


namespace QTask.Middelware
{
	public class AutharizationMiddelware
	{
		private readonly RequestDelegate _next;

		public AutharizationMiddelware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				//No authuentication required pages to be skiped
				if (!httpContext.Request.Path.Value.ToLower().Contains("/login") && httpContext.Request.Path.Value.ToLower() != "/" && !httpContext.Request.Path.Value.ToLower().Contains("/general/index") && !httpContext.Request.Path.Value.ToLower().Contains("/error"))
				{
					//Will check if session is valid or not
					if (Common.ValidateSession(httpContext))
					{

						httpContext.Items["export"] = "1";
						//httpContext.Items["import"] = "1";
						httpContext.Items["list"] = "1";
						httpContext.Items["view"] = "1";
						//httpContext.Items["massupdate"] = "1";
						httpContext.Items["delete"] = "1";
						httpContext.Items["edit"] = "1";

						await _next.Invoke(httpContext);

						////No Authorisation Required pages will skip and is User is Admin then no need to check as he/she will have all rights
						//if (!httpContext.Request.Path.Value.ToLower().Contains("/dashboard"))
						//{
						//	if (httpContext.Session.Get("IsAdmin") != null && httpContext.Session.GetInt32("IsAdmin") == 1)
						//	{
						//		httpContext.Items["export"] = "1";
						//		//httpContext.Items["import"] = "1";
						//		httpContext.Items["list"] = "1";
						//		httpContext.Items["view"] = "1";
						//		//httpContext.Items["massupdate"] = "1";
						//		httpContext.Items["delete"] = "1";
						//		httpContext.Items["edit"] = "1";

						//		await _next.Invoke(httpContext);
						//	}
						//	else
						//	{
						//		//getting UserAccess data from Sesstion
						//		var AccessData = httpContext.Session.GetString("UserAccess");

						//		if (AccessData != null)
						//		{
						//			//Converting it to Model object
						//			List<UserAccessModel> lstUsrAccess = JsonConvert.DeserializeObject<List<UserAccessModel>>(AccessData.ToString());

						//			string AccessPage = string.Empty;

						//			//Getting name of requested page from url
						//			AccessPage = httpContext.Request.Path.Value.Trim().ToLower();
						//			AccessPage = AccessPage.Replace("http://", "").Replace("https://", "");

						//			if (AccessPage.IndexOf("/") > -1)
						//			{
						//				AccessPage = AccessPage.Substring(AccessPage.IndexOf("/") + 1).Trim();
						//				AccessPage = AccessPage.Substring(0, AccessPage.IndexOf("/"));
						//			}
						//			//string AccessPage = httpContext.Request.Path.Value.Substring(httpContext.Request.Path.Value.LastIndexOf("/")+1).Trim();

						//			//Checking whether user is authorized to access requested page?
						//			var UsrAccess = lstUsrAccess.Where(x => x.PageName.ToLower() == AccessPage.ToLower() && x.AccessName.ToLower() == "view" && x.Access.ToLower() == "enabled");

						//			if (UsrAccess.Count() > 0)
						//			{
						//				//if user is authorized then will redirect to requested page


						//				//get access types for users
						//				var listAccess = lstUsrAccess.Where(x => x.PageName.ToLower() == AccessPage.ToLower());

						//				if (listAccess.Count() > 0)
						//				{
						//					//0 means no access, it is default value                                    
						//					httpContext.Items["export"] = "0";
						//					//httpContext.Items["import"] = "0";
						//					httpContext.Items["list"] = "0";
						//					httpContext.Items["view"] = "0";
						//					//httpContext.Items["massupdate"] = "0";
						//					httpContext.Items["delete"] = "0";
						//					httpContext.Items["edit"] = "0";

						//					//base on available data in list, will update value of access to 1 i.e. having access
						//					foreach (UserAccessModel objAcc in listAccess)
						//					{
						//						switch (objAcc.AccessName.ToLower().Trim())
						//						{
						//							case "export":
						//								httpContext.Items["export"] = "1";
						//								break;
						//							case "import":
						//								httpContext.Items["import"] = "1";

						//								break;
						//							case "list":
						//								httpContext.Items["list"] = "1";
						//								break;
						//							case "view":
						//								httpContext.Items["view"] = "1";
						//								break;
						//							case "massupdate":
						//								httpContext.Items["massupdate"] = "1";
						//								break;
						//							case "delete":
						//								httpContext.Items["delete"] = "1";
						//								break;
						//							case "edit":
						//								httpContext.Items["edit"] = "1";
						//								break;
						//							default:
						//								break;
						//						}
						//					}
						//				}

						//				await _next.Invoke(httpContext);
						//			}
						//			else
						//			{
						//				//if user is not authorised then will redirect to error page
						//				httpContext.Response.Redirect("/error/error");
						//			}
						//		}
						//		else
						//		{
						//			//if user is not authorised then will redirect to error page
						//			httpContext.Response.Redirect("/error/error");
						//		}
						//	}
						//}
						//else
						//{
						//	await _next.Invoke(httpContext);
						//}
					}
					else
					{
						//if session is not valid then will redirect to logout and from there it will be redirected to login
						httpContext.Response.Redirect("/general/index");
					}
				}
				else
				{
					await _next.Invoke(httpContext);
				}
			}
			catch (Exception ex)
			{
				//if session is not valid then will redirect to logout and from there it will be redirected to login
				//httpContext.Response.Redirect("/general/index");
			}

		}
	}

	public static class AutharizationMiddelwareExtension
	{
		public static IApplicationBuilder UseAutharizationMiddelware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AutharizationMiddelware>();
		}
	}
}
