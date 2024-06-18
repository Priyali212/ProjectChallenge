using Microsoft.AspNetCore.Mvc;

namespace QTask.Controllers
{
	public class GeneralController : Controller
	{
		public IActionResult Index()
		{
			if (!String.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
				Global.Application.remove("app-" + HttpContext.Session.GetString("UserSession"), HttpContext.Session.Id);

			HttpContext.Session.Clear();
			return Redirect("/");
		}
	}
}
