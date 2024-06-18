using Microsoft.AspNetCore.Mvc;
using QTask.Models;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.Repository;
using QTask.Controllers;

namespace QTask.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModuleAPIController : ControllerBase
	{
		[HttpGet]
		[Route("GetModuleList")]
		public List<ModuleModel> GetModuleList(string keyword = null)
		{
			List<ModuleModel> objlstModule = new List<ModuleModel>();
			try
			{
				if (HttpContext.Items["list"] != null && HttpContext.Items["list"].ToString().Trim() == "1")
				{
					ModuleRepository objModuleRepo = new ModuleRepository(Common.config);
					var lstDBModule = objModuleRepo.GetModuleList(keyword);
					if (lstDBModule != null && lstDBModule.Count > 0)
					{
						foreach (var objDBModule in lstDBModule)
						{
							ModuleModel objModule = new ModuleModel();
							objModule.ModuleId = objDBModule.ModuleId;
							objModule.ModuleName = objDBModule.ModuleName;
							objlstModule.Add(objModule);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("ModuleAPIController", "GetModuleList", ex.Message, HttpContext.Items["UserName"].ToString());
			}
			return objlstModule;
		}

		[HttpPost]
		[Route("SaveModule")]
		public IActionResult SaveModule(ModuleModel objModule)
		{
			bool Status = false;
			string Msg = string.Empty;

			try
			{
				if (HttpContext.Items["edit"] != null && HttpContext.Items["edit"].ToString().Trim() == "1")
				{
					if (objModule != null)
					{
						ModuleDBModel objDBModule = new ModuleDBModel();
						objDBModule.ModuleName = objModule.ModuleName;

						string UserName = HttpContext.Items["UserId"].ToString();
						string IPAddress = CommonAPI.getIPAddress(HttpContext);
						string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);

						ModuleRepository objModuleRepo = new ModuleRepository(Common.config);

						Status = objModuleRepo.SaveModule(objDBModule, UserName, IPAddress, BrowserDetails);

						if (Status)
							Msg = "Module is Created Successfully.";
						else
							Msg = "Thers is Error while Creating Module.";
					}
					else
					{
						Msg = "Data is not Provided";
					}
				}
				else
				{
					Msg = "Access is Denied";
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("ModuleAPIController", "SaveModule", ex.Message, HttpContext.Items["UserId"].ToString());
			}
			return new JsonResult(new { status = Status, msg = Msg });
		}

		[HttpGet]
		[Route("DeleteModule")]
		public IActionResult DeleteModule(int ModuleId)
		{
			bool Status = false;
			string Msg = string.Empty;
			try
			{
				if (HttpContext.Items["delete"] != null && HttpContext.Items["delete"].ToString().Trim() == "1")
				{
					if (ModuleId != null && ModuleId > 0)
					{
						string UserName = HttpContext.Items["UserId"].ToString();
						string IPAddress = CommonAPI.getIPAddress(HttpContext);
						string BrowserDetails = CommonAPI.getBrowserDetails(HttpContext);

						ModuleRepository objModuleRepo = new ModuleRepository(Common.config);

						Status = objModuleRepo.DeleteModule(ModuleId, UserName, IPAddress, BrowserDetails);

						if (Status)
							Msg = "Module is Deleted Successfully.";
						else
							Msg = "Thers is Error while Deleting Module.";
					}
					else
					{
						Msg = "Please provide valid data";
					}
				}
				else
				{
					Msg = "Access is Denied";
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				objComm.SaveErrorLog("ModuleAPIController", "DeleteModule", ex.Message, HttpContext.Items["UserId"].ToString());
			}
			return new JsonResult(new { status = Status, msg = Msg });
		}
	}
}
