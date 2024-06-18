using Microsoft.AspNetCore.Mvc;

namespace QTask.Controllers
{
	public class CheckStatusController : Controller
	{
		public IActionResult CheckStatus()
		{
			return View();
		}
	}
}
