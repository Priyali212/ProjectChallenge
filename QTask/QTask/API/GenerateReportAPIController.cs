using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTask.Controllers;
using QTask.Models;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.Repository;
using System.Data;
using System.Globalization;

namespace QTask.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenerateReportAPIController : ControllerBase
	{
		
        HttpContext httpCont;
        [HttpGet]
        [Route("GenerateReport")]

		
		public GenerateReportDBModel GenerateReport(int UserId, string? FromDate, string? ToDate)
        {

            DateTimeFormatInfo InDtFmt = new CultureInfo("en-IN", false).DateTimeFormat;
           // List<GenerateReportModel> objTSList = new List<GenerateReportModel>();
			GenerateReportDBModel objTimeList = new GenerateReportDBModel();
			try
            {
                GenerateReportRepository objReportRepo = new GenerateReportRepository(Common.config);
                 objTimeList = objReportRepo.GenerateReports(UserId, FromDate, ToDate);

      //          if (objVarLstTS != null)
      //          {
      //              for (int i = 0; i < objVarLstTS.Count; i++)
      //              {

      //                  GenerateReportModel objTimeList = new GenerateReportModel();
						//string ID = objVarLstTS[i].JiraId + " " + objVarLstTS[i].Task;
						//objTimeList.totalRecord = objVarLstTS[i].totalRecord;
						//objTimeList.resourcesname = objVarLstTS[i].resourcesname;
						//objTimeList.OpenTask = objVarLstTS[i].OpenTask;
						//objTimeList.ClosedTask = objVarLstTS[i].ClosedTask;
						




						//objTSList.Add(objTimeList);
      //              }
      //          }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string Username = string.Empty;

                objComm.SaveErrorLog("GenerateReportAPIController", "GenerateReport", ex.Message, Username);
            }
            return objTimeList;
        }



		[HttpGet]
		[Route("ExportToExcel")]
		public IActionResult ExportToExcel()//string ValidationQueue, string FromDate, string ToDate
		{
			
			int UserId = 0;
			string? FromDate = string.Empty;
			string? ToDate=string.Empty;

			if (Request.Query["UserId"] != "undefined" && Request.Query["UserId"] != "")
			{
				UserId =Convert.ToInt32( Request.Query["UserId"]);
			}
			if (Request.Query["FromDate"] != "undefined" && Request.Query["FromDate"] != "")
				FromDate = Request.Query["FromDate"].ToString();

			if (Request.Query["ToDate"] != "undefined" && Request.Query["ToDate"] != "")
				ToDate = Request.Query["ToDate"].ToString();


			DataTable ds = new DataTable();

			GenerateReportRepository objGetVendorResponse = new GenerateReportRepository(Common.config);
			ds = objGetVendorResponse.GenerateReportsExport(UserId, FromDate, ToDate);

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Sheet1");
				 

				// Load DataTable to worksheet starting from cell A1
				worksheet.Cell(1, 1).InsertTable(ds);

				// Prepare Excel file stream
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					stream.Position = 0;

					// Return Excel file as downloadable attachment
					string excelName = $"GenerateReport{DateTime.Now.ToString("yyyy-MM-dd")}.xlsx";
					return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
				}
			}
		}


	}
}
