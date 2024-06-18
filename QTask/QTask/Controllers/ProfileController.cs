using Microsoft.AspNetCore.Mvc;
using QTaskDataLayer.Repository;

namespace QTask.Controllers
{
	public class ProfileController : Controller
	{
		public IActionResult AddUser()
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

				objComm.SaveErrorLog("ProfileController", "AddUser", ex.Message, UserName);
			}
			return View();
		}
	}
}
