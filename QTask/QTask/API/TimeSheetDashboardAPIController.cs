using Microsoft.AspNetCore.Mvc;

namespace QTask.API
{
	public class TimeSheetDashboardAPIController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
