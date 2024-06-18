using Microsoft.AspNetCore.Mvc;
using QTask.Controllers;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.Repository;
using System.Globalization;
using static QTaskDataLayer.DBModel.GenerateReportDBModel;

namespace QTask.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class CheckStatusAPIController : Controller
    {
        HttpContext httpCont;
        [HttpGet]
        [Route("CheckStatus")]

        public CheckStatusDBModel CheckStatus(int UserId, string? FromDate, string? ToDate)
        {

            DateTimeFormatInfo InDtFmt = new CultureInfo("en-IN", false).DateTimeFormat;
            // List<GenerateReportModel> objTSList = new List<GenerateReportModel>();
            CheckStatusDBModel objTimeList = new CheckStatusDBModel();
            try
            {
                CheckStatusRepository objReportRepo = new CheckStatusRepository(Common.config);
                objTimeList = objReportRepo.CheckStatus(UserId, FromDate, ToDate);

            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string Username = string.Empty;

                objComm.SaveErrorLog("CheckStatusAPIController", "CheckStatus", ex.Message, Username);
            }
            return objTimeList;
        }

    }
}
