using Microsoft.AspNetCore.Mvc;
using QTask.Models;
using QTaskDataLayer.Repository;
using System.Globalization;
using QTask.Controllers;


namespace QTask.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimesheetAdminAPIController : ControllerBase
	{
		HttpContext httpCont;
		[HttpGet]
		[Route("GetTimesheet")]

		public List<TimesheetAdminModel> GetTimesheet(int UserId, string? FromDate, string? ToDate, int PageIndex)
		{

			DateTimeFormatInfo InDtFmt = new CultureInfo("en-IN", false).DateTimeFormat;
			List<TimesheetAdminModel> objTSList = new List<TimesheetAdminModel>();
			int totalRecord = 0;
			int totalPageCount = 0;
			int pageSize = 100;
			try
			{
				TimesheetAdminRepository objTimeSheetRepo = new TimesheetAdminRepository(Common.config);
				double Total = 0.00;

				var objVarLstTS = objTimeSheetRepo.GetTimesheetList(UserId, FromDate, ToDate, PageIndex, pageSize);

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
						AdminPaginationModel obj = new AdminPaginationModel();
						TimesheetAdminModel objTimeList = new TimesheetAdminModel();
						totalRecord = objVarLstTS[i].TotalRecords;
						string ID = objVarLstTS[i].JiraId + " " + objVarLstTS[i].Task;
						objTimeList.UserId = objVarLstTS[i].UserId;
						objTimeList.JiraId = ID;
						objTimeList.Description = objVarLstTS[i].Description;
						objTimeList.WorkedDate = objVarLstTS[i].WorkedDate;
						objTimeList.MinSpend = (objVarLstTS[i].MinSpend);
						Total += objTimeList.MinSpend;
						objTimeList.HourSpend = Total / 60;
						//                  TimeSpan spWorkMin = TimeSpan.FromMinutes(Total);
						//                  string workHours = spWorkMin.ToString(@"hh\:mm");
						//objTimeList.HourSpend = workHours;
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
	}
}
