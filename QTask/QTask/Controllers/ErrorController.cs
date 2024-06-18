using Microsoft.AspNetCore.Mvc;

namespace QTask.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
