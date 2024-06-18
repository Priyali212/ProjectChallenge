using Microsoft.AspNetCore.Mvc;
using QTask.Models;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.Repository;
using System.Data;
using QTask.Controllers;

namespace QTask.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimesheetAPIController : ControllerBase
	{
		HttpContext httpCont;

		[HttpPost]
		[Route("SaveTimesheet")]

		public IActionResult SaveTimesheets(TimesheetModel obj)
		{
			string result = string.Empty;
			bool Status = false;
			try
			{
				TimesheetDBModel objTimesheet = new TimesheetDBModel();

				var input = obj.JiraId;
				var splitted = input.Split(' ', 2);
				var Id = splitted[0].Trim();

				objTimesheet.JiraId = Id;
				objTimesheet.WorkedDate = obj.WorkedDate;
				objTimesheet.Description = obj.Description;
				objTimesheet.MinSpend = obj.MinSpend;

				objTimesheet.UserId = Convert.ToInt32(HttpContext.Items["UserId"]);

				TimesheetRepository objTimesheetRepo = new TimesheetRepository(Common.config);

				result = objTimesheetRepo.SaveTimesheets(objTimesheet);
				Status = true;
			}
			catch (Exception ex)
			{

			}
			return new JsonResult(new { status = Status, msg = result });
		}

		[HttpGet]
		[Route("ShowTimesheetList")]

		public List<TimesheetModel> ShowTimesheetList(int UserId, string WorkedDate, int PageIndex)
		{
			List<TimesheetModel> objTSList = new List<TimesheetModel>();
			int totalRecord = 0;
			int totalPageCount = 0;
			int pageSize = 100;
			try
			{
				TimesheetRepository objTimeSheetRepo = new TimesheetRepository(Common.config);
				double Total = 0.00;
				UserId = Convert.ToInt32(HttpContext.Items["UserId"]);
				var objVarLstTS = objTimeSheetRepo.GetTimesheetList(UserId, WorkedDate, PageIndex, pageSize);

				totalRecord = objVarLstTS[0].TotalRecords;
				if (totalRecord % pageSize == 0)
				{
					totalPageCount = totalRecord / pageSize;
				}
				else
				{
					totalPageCount = (totalRecord / pageSize) + 1;
				}

				if (objVarLstTS != null)
				{
					for (int i = 0; i < objVarLstTS.Count; i++)
					{
						TimesheetPaginationModel obj = new TimesheetPaginationModel();
						TimesheetModel objTimeList = new TimesheetModel();
						totalRecord = objVarLstTS[i].TotalRecords;
						objTimeList.TimesheetId = objVarLstTS[i].TimesheetId;
						objTimeList.JiraId = objVarLstTS[i].JiraId;
						objTimeList.Task = objVarLstTS[i].Task;
						//objTimeList.UserId = objVarLstTS[i].UserId;
						objTimeList.Description = objVarLstTS[i].Description;
						objTimeList.WorkedDate = objVarLstTS[i].WorkedDate;
						objTimeList.MinSpend = objVarLstTS[i].MinSpend;
						Total += Convert.ToInt32(objTimeList.MinSpend);
						//objTimeList.HourSpend = Total / 60;
						TimeSpan spWorkMin = TimeSpan.FromMinutes(Total);
						string workHours = spWorkMin.ToString(@"hh\:mm");
						objTimeList.HourSpend = workHours;
						//string workHours = string.Format("{0}:{1:00}", (int)spWorkMin.TotalHours, spWorkMin.Minutes);
						obj.TotalPageCount = totalPageCount;
						obj.PageIndex = PageIndex;
						obj.PreviousPageIndex = PageIndex - 1;
						obj.NextPageIndex = PageIndex + 1;
						objTimeList.paginationModels = obj;

						objTSList.Add(objTimeList);
					}
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				string Username = string.Empty;

				objComm.SaveErrorLog("TimesheetAPIController", "ShowTimesheetList", ex.Message, Username);
			}
			return objTSList;
		}

		[HttpGet]
		[Route("GetAutoCompleteSearchData")]

		public JsonResult GetAutoCompleteSearchData(string searchValue)
		{
			TimesheetRepository repository = new TimesheetRepository(Common.config);
			string result = string.Empty;
			bool Status = false;
			try
			{

				var schemeKeywordList = repository.GetSearchKeywords(searchValue.Trim().ToLower()).Where(x => x.JiraID.Trim().ToLower().Contains(searchValue.Trim().ToLower()))
								   .Select(x => new
								   {
									   label = x.JiraID,
									   val = x.JiraID.ToString(),
								   }).ToList();


				return new JsonResult(new { status = Status, msg = schemeKeywordList });

			}

			catch (Exception ex)
			{

				CommonRepository objComm = new CommonRepository(Common.config);

				string UserName = string.Empty;

				objComm.SaveErrorLog("TimesheetAPIController", "GetSearch", ex.Message, UserName);
				return new JsonResult(new { Success = false, status = Status });
			}

		}



		[HttpGet]
		[Route("CheckJiraId")]
		public string CheckJiraId(string JiraId)
		{
			string Return = string.Empty;
			DataTable dt = new DataTable();
			TimesheetRepository objTTimesheetRepository = new TimesheetRepository(Common.config);
			try
			{
				var input = JiraId;
				var splitted = input.Split(' ', 2);
				var Id = splitted[0].Trim();
				dt = objTTimesheetRepository.CheckJiraId(Id);

				if (dt.Rows.Count > 0)
				{
					Return = "true";
				}
				else
				{ Return = "false"; }
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(Common.config);
				string Username = string.Empty;

				objComm.SaveErrorLog("TimeSheetAPIController", "CheckJiraId", ex.Message, Username);
			}


			return Return;
		}


		[HttpGet]
		[Route("DeleteTimesheet")]
		public JsonResult DeleteTimesheet(int TimesheetId)
		{
			bool Status = false;
			string Msg = string.Empty;
			try
			{
				if (HttpContext.Items["delete"] != null && HttpContext.Items["delete"].ToString().Trim() == "1")
				{
					if (TimesheetId != null && TimesheetId > 0)
					{
						string UserName = HttpContext.Items["UserId"].ToString();

						TimesheetRepository objTimeRepo = new TimesheetRepository(Common.config);

						Status = objTimeRepo.DeleteTimesheet(TimesheetId, UserName);

						if (Status)
							Msg = "Timesheet is Deleted Successfully.";
						else
							Msg = "There is Error while Deleting Records.";
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
				objComm.SaveErrorLog("TimeSheetAPIController", "DeleteTimesheet", ex.Message, HttpContext.Items["UserId"].ToString());
			}
			return new JsonResult(new { status = Status, msg = Msg });
		}


		//[HttpGet]
		//[Route("DeleteTimesheet")]

		//public IActionResult DeleteTimesheet(int TimesheetId)
		//{

		//    TimesheetRepository objTimeRepo = new TimesheetRepository(Common.config);

		//    int result = objTimeRepo.DeleteTimesheetRec(TimesheetId);

		//    return new JsonResult(new { status = result > 0 ? true : false });
		//}
	}
}