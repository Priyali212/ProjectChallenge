using Microsoft.AspNetCore.Mvc;

namespace QTask.Controllers
{
	public class GenerateReportController : Controller
	{
		public IActionResult GenerateReport()
		{
			return View();
		}
	}
}
