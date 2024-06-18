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
    public class TaskSheetAPIController : ControllerBase
    {
        [HttpGet]
        [Route("GetResourcesList")]
        public List<ResourceslstModel> GetResourceList()
        {

            List<ResourceslstModel> ResourcesList = new List<ResourceslstModel>();

            try
            {
                TaskSheetRepository objResourcesRepo = new TaskSheetRepository(Common.config);
                var lstDBResources = objResourcesRepo.GetResourcesLists();

                if (lstDBResources != null)
                {
                    foreach (var objDBResources in lstDBResources)
                    {
                        ResourceslstModel objResources = new ResourceslstModel();

                        objResources.resourcesid = objDBResources.resourcesid;
                        objResources.resourcesname = objDBResources.resourcesname;

                        ResourcesList.Add(objResources);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;
                objComm.SaveErrorLog("TaskSheetAPIController", "GetRiskList", ex.Message, "");
            }
            return ResourcesList;
        }


        [HttpGet]
        [Route("GetUserList")]
        public List<UserlstModel> GetUserList()
        {

            List<UserlstModel> UserList = new List<UserlstModel>();

            try
            {
                TaskSheetRepository objResourcesRepo = new TaskSheetRepository(Common.config);
                var lstDBResources = objResourcesRepo.GetUserLists();

                if (lstDBResources != null)
                {
                    foreach (var objDBResources in lstDBResources)
                    {
                        UserlstModel objResources = new UserlstModel();

                        objResources.id = objDBResources.id;
                        objResources.UserFullName = objDBResources.UserFullName;

                        UserList.Add(objResources);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;
                objComm.SaveErrorLog("TaskSheetAPIController", "GetUserList", ex.Message, "");
            }
            return UserList;
        }



        [HttpGet]
        [Route("GetTimesheetList")]
        public List<TimeSheetListModel> GetTimesheetList(string? JiraID, string? ProjectName, string? AssignedTo, string? Status, int PageIndex)
        {

            List<TimeSheetListModel> objlstTimesheetList = new List<TimeSheetListModel>();
            //PageIndex = 1;
            int totalRecord = 0;
            int totalPageCount = 0;
            int pageSize = 100;
            try
            {
                TaskSheetRepository objTimesheetRepo = new TaskSheetRepository(Common.config);

                var objVarLsttimesheet = objTimesheetRepo.GetTimesheetList(JiraID, ProjectName, AssignedTo, Status, PageIndex, pageSize);
                totalRecord = objVarLsttimesheet[1].TotalRecords;
                if (totalRecord % pageSize == 0)
                {
                    totalPageCount = totalRecord / pageSize;
                }
                else
                {
                    totalPageCount = (totalRecord / pageSize) + 1;
                }
                if (objVarLsttimesheet != null)
                {
                    for (int i = 0; i < objVarLsttimesheet.Count; i++)
                    {
                        PaginationModel objj = new PaginationModel();
                        TimeSheetListModel objTimesheetsList = new TimeSheetListModel();
                        totalRecord = objVarLsttimesheet[i].TotalRecords;
                        objTimesheetsList.JiraID = objVarLsttimesheet[i].JiraID;
                        objTimesheetsList.TasksheetId = objVarLsttimesheet[i].TasksheetId;
                        objTimesheetsList.Task = objVarLsttimesheet[i].Task;
                        objTimesheetsList.ProjectName = objVarLsttimesheet[i].ProjectName;
                        objTimesheetsList.resourcesname = objVarLsttimesheet[i].resourcesname;
                        objTimesheetsList.Actualstartdate = objVarLsttimesheet[i].Actualstartdate;
                        objTimesheetsList.Actualenddate = objVarLsttimesheet[i].Actualenddate;
                        objTimesheetsList.EstimatePlanDate = objVarLsttimesheet[i].EstimatePlanDate;
                        objTimesheetsList.EstimatecompletionDate = objVarLsttimesheet[i].EstimatecompletionDate;
                        objTimesheetsList.Actualhours = objVarLsttimesheet[i].Actualhours;
                        objTimesheetsList.Estimatedhours = objVarLsttimesheet[i].Estimatedhours;
                        objj.TotalPageCount = totalPageCount;
                        objj.PageIndex = PageIndex;
                        objj.PreviousPageIndex = PageIndex - 1;
                        objj.NextPageIndex = PageIndex + 1;
                        objTimesheetsList.paginationModels = objj;
                        objlstTimesheetList.Add(objTimesheetsList);


                    }
                    //TimeSheetListModel objTimesheetsListt = new TimeSheetListModel();
                    //PaginationModel obj = new PaginationModel();


                    //PaginationModel obj = new PaginationModel();

                    //obj.TotalPageCount = totalPageCount;
                    //obj.PreviousPageIndex = pageIndex - 1;
                    //obj.NextPageIndex = pageIndex + 1;
                    //obj.TotalPageCount = totalPageCount;
                    //obj.PreviousPageIndex = pageIndex - 1;
                    //obj.NextPageIndex = pageIndex + 1;
                    //objTimesheetsListt.paginationModels = obj;
                    //objlstTimesheetList.Add(objTimesheetsListt);
                    //objlstTimesheetList.Add();
                }



            }

            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string Username = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "GetTimesheetList", ex.Message, Username);


            }
            return objlstTimesheetList;
        }


        //[HttpGet]
        //[Route("CheckJiraId")]
        //public string CheckJiraId(string JiraId)
        //{
        //	string Return = string.Empty;
        //	DataTable dt = new DataTable();
        //	TaskSheetRepository objTaskSheetSheetRepository = new TaskSheetRepository(Common.config);
        //	try
        //	{
        //		var splitted = JiraId.Split(' ', 2);
        //		var Id = splitted[0].Trim();
        //		dt = objTaskSheetSheetRepository.CheckJiraId(Id);

        //		if (dt.Rows.Count > 0)
        //		{
        //			Return = "true";
        //		}
        //		else
        //		{ Return = "false"; }
        //	}
        //	catch (Exception ex)
        //	{
        //		CommonRepository objComm = new CommonRepository(Common.config);
        //		string Username = string.Empty;

        //		objComm.SaveErrorLog("TaskSheetAPIController", "CheckJiraId", ex.Message, Username);
        //	}


        //	return Return;
        //}




        [HttpGet]
        [Route("CheckJiraId")]
        public JsonResult CheckJiraId(string JiraId)
        {
            string Return = string.Empty;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            TaskSheetRepository objTaskSheetSheetRepository = new TaskSheetRepository(Common.config);
            try
            {

                var splitted = JiraId.Split(' ', 2);
                var Id = splitted[0].Trim();
                dt = objTaskSheetSheetRepository.CheckJiraId(Id);

                if (dt.Rows.Count > 0)
                {
                    Return = "true";
                    // return new JsonResult(new { status = true, msg = "True" });
                }
                else
                {
                    return new JsonResult(new { status = false, msg = "Please Enter Valid JiraId" });
                }

                dt1 = objTaskSheetSheetRepository.CheckJiraIdinTasksheet(Id);

                if (dt1.Rows.Count > 0)
                {
                    return new JsonResult(new { status = false, msg = "JiraId Already Exist in Task sheet" });
                    //return new JsonResult(new { status = true, msg = "True" });
                }
                else
                {
                    Return = "true";
                }

            }

            catch (Exception ex)
            {

                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "CheckJiraId", ex.Message, UserName);
                //return new JsonResult(new { Success = false, status = Status });
            }
            return new JsonResult(new { status = true, msg = Return });
        }


        [HttpGet]
        [Route("GetTaskHistory")]
        public List<HistoryListModel> GetTaskHistory(int TasksheetId)
        {

            List<HistoryListModel> objlstTimesheetList = new List<HistoryListModel>();
            try
            {
                TaskSheetRepository objTimesheetRepo = new TaskSheetRepository(Common.config);

                var objVarLsttimesheet = objTimesheetRepo.GetTaskHistory(TasksheetId);

                if (objVarLsttimesheet != null)
                {
                    for (int i = 0; i < objVarLsttimesheet.Count; i++)
                    {
                        HistoryListModel objTimesheetsList = new HistoryListModel();

                        objTimesheetsList.JiraID = objVarLsttimesheet[i].JiraID;
                        objTimesheetsList.TasksheetId = objVarLsttimesheet[i].TasksheetId;
                        objTimesheetsList.Task = objVarLsttimesheet[i].Task;
                        objTimesheetsList.ProjectName = objVarLsttimesheet[i].ProjectName;
                        objTimesheetsList.resourcesname = objVarLsttimesheet[i].resourcesname;
                        objTimesheetsList.Actualstartdate = objVarLsttimesheet[i].Actualstartdate;
                        objTimesheetsList.Actualenddate = objVarLsttimesheet[i].Actualenddate;
                        objTimesheetsList.EstimatePlanDate = objVarLsttimesheet[i].EstimatePlanDate;
                        objTimesheetsList.EstimatecompletionDate = objVarLsttimesheet[i].EstimatecompletionDate;
                        objTimesheetsList.Actualhours = objVarLsttimesheet[i].Actualhours;
                        objTimesheetsList.Estimatedhours = objVarLsttimesheet[i].Estimatedhours;
                        objTimesheetsList.Modifiedby = objVarLsttimesheet[i].Modifiedby;

                        objlstTimesheetList.Add(objTimesheetsList);
                    }

                }

            }

            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string Username = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "GetTimesheetList", ex.Message, Username);


            }
            return objlstTimesheetList;
        }



        [HttpGet]
        [Route("GetJiraRecord")]
        public List<JiraIdeListModel> GetJiraRecord(string JiraId)
        {

            List<JiraIdeListModel> objlstTimesheetList = new List<JiraIdeListModel>();
            try
            {
                TaskSheetRepository objTimesheetRepo = new TaskSheetRepository(Common.config);

                var objVarLsttimesheet = objTimesheetRepo.GetJiraRecord(JiraId);

                if (objVarLsttimesheet != null)
                {
                    for (int i = 0; i < objVarLsttimesheet.Count; i++)
                    {
                        JiraIdeListModel objTimesheetsList = new JiraIdeListModel();

                        objTimesheetsList.timesheetid = objVarLsttimesheet[i].timesheetid;
                        objTimesheetsList.JiraId = objVarLsttimesheet[i].JiraId;
                        objTimesheetsList.WorkedDate = objVarLsttimesheet[i].WorkedDate;
                        objTimesheetsList.Description = objVarLsttimesheet[i].Description;
                        objTimesheetsList.minutesSpent = objVarLsttimesheet[i].minutesSpent;
                        objTimesheetsList.UserId = objVarLsttimesheet[i].UserId;
                        objTimesheetsList.Addeddate = objVarLsttimesheet[i].Addeddate;
                        objTimesheetsList.modifiedby = objVarLsttimesheet[i].modifiedby;


                        objlstTimesheetList.Add(objTimesheetsList);
                    }

                }

            }

            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string Username = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "GetJiraRecord", ex.Message, Username);


            }
            return objlstTimesheetList;
        }


        [HttpGet]
        [Route("GetSingleTimesheet")]
        public GetTimeSheetModel GetSingleTasksheet(int TasksheetId)
        {
            TaskSheetRepository objTimesheetRepo = new TaskSheetRepository(Common.config);
            GetTimeSheetModel objTimesheetDetails = new GetTimeSheetModel();

            try
            {
                var objTimesheetModel = objTimesheetRepo.GetTasksheetDetails(TasksheetId);

                if (objTimesheetModel != null)
                {
                    var splitted = objTimesheetModel.JiraID.Split('-', 2);
                    string ID = splitted[1] + " " + objTimesheetModel.Task;

                    objTimesheetDetails.TasksheetId = objTimesheetModel.TasksheetId;
                    objTimesheetDetails.JiraID = ID;
                    objTimesheetDetails.ProjectName = objTimesheetModel.ProjectName;
                    objTimesheetDetails.Task = objTimesheetModel.Task;
                    //objTimesheetDetails.Remarks = objTimesheetModel.Remarks;
                    objTimesheetDetails.ReqFrom = objTimesheetModel.ReqFrom;
                    objTimesheetDetails.ReqDate = objTimesheetModel.ReqDate;
                    objTimesheetDetails.EstimatePlanDate = objTimesheetModel.EstimatePlanDate;
                    objTimesheetDetails.EstimatecompletionDate = objTimesheetModel.EstimatecompletionDate;
                    objTimesheetDetails.Estimatedhours = objTimesheetModel.Estimatedhours;
                    objTimesheetDetails.Actualstartdate = objTimesheetModel.Actualstartdate;
                    objTimesheetDetails.Actualenddate = objTimesheetModel.Actualenddate;
                    objTimesheetDetails.Actualhours = objTimesheetModel.Actualhours;
                    objTimesheetDetails.Status = objTimesheetModel.Status;
                    objTimesheetDetails.AssignedTo = objTimesheetModel.AssignedTo;
                    objTimesheetDetails.Discription = objTimesheetModel.Discription;



                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);
                string UserName = string.Empty;
                objComm.SaveErrorLog("TaskSheetAPIController", "GetSingleTasksheet", ex.Message, "");

            }
            return objTimesheetDetails;
        }


        [HttpPost]
        [Route("SaveTaskSheet")]
        public IActionResult SaveTaskSheet(SaveTimeSheetModel obj)
        {


            string result = string.Empty;
            bool Status = false;
            try
            {
                SaveTimeSheetListDBModel objTimesheetDB = new SaveTimeSheetListDBModel();


                var splitted = obj.JiraID.Split(' ', 2);
                var Id = "DS-" + splitted[0].Trim();


                var Task = splitted[1].Trim();

                objTimesheetDB.TasksheetId = obj.TasksheetId;
                objTimesheetDB.JiraID = Id;
                objTimesheetDB.ProjectName = obj.ProjectName;
                objTimesheetDB.Task = Task;
                //objTimesheetDB.Remarks = obj.Remarks;
                objTimesheetDB.ReqFrom = obj.ReqFrom;
                objTimesheetDB.ReqDate = obj.ReqDate;
                objTimesheetDB.EstimatePlanDate = obj.EstimatePlanDate;
                objTimesheetDB.EstimatecompletionDate = obj.EstimatecompletionDate;
                objTimesheetDB.Estimatedhours = obj.Estimatedhours;
                if (obj.Actualstartdate == "" && obj.Actualstartdate == string.Empty)
                {
                    objTimesheetDB.Actualstartdate = null;
                }
                else
                {
                    objTimesheetDB.Actualstartdate = obj.Actualstartdate;
                }
                if (obj.Actualenddate == "" && obj.Actualenddate == string.Empty)
                {
                    objTimesheetDB.Actualenddate = null;
                }
                else
                {
                    objTimesheetDB.Actualenddate = obj.Actualenddate;
                }

                objTimesheetDB.Actualhours = obj.Actualhours;
                objTimesheetDB.Status = obj.Status;

                objTimesheetDB.AssignedTo = obj.AssignedTo;
                objTimesheetDB.Discription = obj.Discription;




                TaskSheetRepository objTimesheetRepo = new TaskSheetRepository(Common.config);
                objTimesheetDB.ModifiedBy = HttpContext.Items["UserId"].ToString();

                if (obj.TasksheetId == 0)
                {
                    result = objTimesheetRepo.SaveTasksheet(objTimesheetDB);
                    Status = true;

                }


                else
                {
                    result = objTimesheetRepo.UpdateTimesheet(objTimesheetDB);
                    Status = true;
                }


            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "SaveTaskSheet", ex.Message, UserName);
            }

            return new JsonResult(new { status = Status, msg = result });
        }



        [HttpGet]
        [Route("GetAutoCompleteSearchData")]

        public JsonResult GetAutoCompleteSearchData(string searchValue)
        {
            TaskSheetRepository repository = new TaskSheetRepository(Common.config);
            string result = string.Empty;
            bool Status = false;
            try
            {

                var schemeKeywordList = repository.GetSearchKeywordsForTasksheet(searchValue.Trim().ToLower()).Where(x => x.JiraID.Trim().ToLower().Contains(searchValue.Trim().ToLower()))
                                   .Select(x => new
                                   {
                                       label = x.JiraID,
                                       val = x.JiraID.ToString()
                                   }).ToList();


                return new JsonResult(new { status = Status, msg = schemeKeywordList });

            }

            catch (Exception ex)
            {

                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "GetAutoCompleteSearchData", ex.Message, UserName);
                return new JsonResult(new { Success = false, status = Status });
            }

        }




        [HttpGet]
        [Route("GetSearchUserLst")]

        public JsonResult GetSearchUserLst(string searchValue)
        {
            TaskSheetRepository repository = new TaskSheetRepository(Common.config);
            string result = string.Empty;
            bool Status = false;
            try
            {

                var schemeKeywordList = repository.GetSearchUserLst(searchValue.Trim().ToLower()).Where(x => x.UserFullName.Trim().ToLower().Contains(searchValue.Trim().ToLower()))
                                   .Select(x => new
                                   {
                                       label = x.UserFullName,
                                       val = x.UserFullName.ToString()
                                   }).ToList();


                return new JsonResult(new { status = Status, msg = schemeKeywordList });

            }

            catch (Exception ex)
            {

                CommonRepository objComm = new CommonRepository(Common.config);

                string UserName = string.Empty;

                objComm.SaveErrorLog("TaskSheetAPIController", "GetSearchUserLst", ex.Message, UserName);
                return new JsonResult(new { Success = false, status = Status });
            }

        }



    }
}
