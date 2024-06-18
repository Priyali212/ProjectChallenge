using Microsoft.AspNetCore.Mvc;
using QTaskDataLayer.Repository;

namespace QTask.Controllers
{
	public class DashboardController : Controller
	{	
		public IActionResult Dashboard()
		{
			try
			{
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				string UserName = string.Empty;
				if (HttpContext.Session?.GetString("UserSession") != null && HttpContext.Session?.GetString("UserSession") != string.Empty)
				{
					UserName = HttpContext.Session?.GetString("UserSession");
				}

				objComm.SaveErrorLog("DashboardController", "Dashboard", ex.Message, UserName);
			}
			return View();
		}
	}
}
