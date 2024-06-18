using Microsoft.AspNetCore.Mvc;
using QTaskDataLayer.Repository;

namespace QTask.Controllers
{
	public class TimeSheetDashboardController : Controller
	{
		public IActionResult TimeSheetDashboard()
		{

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
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);

				string UserName = string.Empty;

				if (HttpContext.Session.Get("UserName") != null && HttpContext.Session.Get("UserName").ToString().Trim() != "")
					UserName = HttpContext.Session.Get("UserName").ToString().Trim();


				objComm.SaveErrorLog("TaskSheetController", "TaskSheet", ex.Message, UserName);
			}
			return View();
		}
	}
}
