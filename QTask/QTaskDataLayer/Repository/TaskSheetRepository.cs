using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.DBOperations;

namespace QTaskDataLayer.Repository
{
    public class TaskSheetRepository
    {

        DB objDB;
        CommonRepository objComm;
        IConfiguration config;
        public TaskSheetRepository(IConfiguration iconfig)
        {
            objDB = new DB(iconfig);
            objComm = new CommonRepository(iconfig);
        }


        //Resources DropdownList
        public List<ResourceslstDBModel> GetResourcesLists()
        {
            List<ResourceslstDBModel> ResourceslstDB = new List<ResourceslstDBModel>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
               {


               };
                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_IntResourcesList", param);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ResourceslstDBModel objDBResources = new ResourceslstDBModel();

                        objDBResources.resourcesid = Convert.ToInt32(dr["resourcesid"]);
                        objDBResources.resourcesname = (dr["resourcesname"]).ToString();

                        ResourceslstDB.Add(objDBResources);
                    }

                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(config);
                objComm.SaveErrorLog("TaskSheetSheetRepository", "GetResourcesLists", ex.Message + "-" + ex.InnerException, "");
            }

            return ResourceslstDB;
        }




        //Users DropdownList
        public List<UserlstDBModel> GetUserLists()
        {
            List<UserlstDBModel> ResourceslstDB = new List<UserlstDBModel>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
               {


               };
                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_IntUserList", param);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserlstDBModel objDBResources = new UserlstDBModel();

                        objDBResources.id = Convert.ToInt32(dr["id"]);
                        objDBResources.UserFullName = (dr["UserFullName"]).ToString();

                        ResourceslstDB.Add(objDBResources);
                    }

                }
            }
            catch (Exception ex)
            {
                CommonRepository objComm = new CommonRepository(config);
                objComm.SaveErrorLog("TaskSheetSheetRepository", "GetResourcesLists", ex.Message + "-" + ex.InnerException, "");
            }

            return ResourceslstDB;
        }





        public List<HistoryListDBModel> GetTaskHistory(int TasksheetId)
        {
            List<HistoryListDBModel> objlstTimesheet = new List<HistoryListDBModel>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                    {
                    new SqlParameter("@TasksheetId",TasksheetId),
                    };
                DataSet ds = objDB.getDataFromDBToDataSet("[Q_Pr_GetTaskHistoryById]", param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        HistoryListDBModel objTimesheetList = new HistoryListDBModel();

                        objTimesheetList.TasksheetId = Convert.ToInt32(dr["TasksheetId"].ToString().Trim());
                        objTimesheetList.JiraID = dr["JiraID"].ToString().Trim();
                        objTimesheetList.Task = dr["Task"].ToString().Trim();
                        objTimesheetList.ProjectName = dr["ProjectName"].ToString().Trim();
                        objTimesheetList.resourcesname = dr["resourcesname"].ToString().Trim();
                        objTimesheetList.EstimatePlanDate = Convert.ToDateTime(dr["EstimatePlanDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                        objTimesheetList.EstimatecompletionDate = Convert.ToDateTime(dr["EstimatecompletionDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                        objTimesheetList.Estimatedhours = Convert.ToInt32(dr["Estimatedhours"].ToString().Trim());
                        if (!String.IsNullOrEmpty(Convert.ToString(dr["Actualstartdate"])))
                        {
                            objTimesheetList.Actualstartdate = Convert.ToDateTime(dr["Actualstartdate"].ToString()).ToString("yyyy-MM-dd");
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["Actualenddate"])))
                        {
                            objTimesheetList.Actualenddate = Convert.ToDateTime(dr["Actualenddate"].ToString()).ToString("yyyy-MM-dd");
                        }
                        objTimesheetList.Actualhours = string.IsNullOrEmpty(dr["Actualhourss"].ToString()) ? 0 : Convert.ToInt32(dr["Actualhourss"].ToString().Trim());
                        objTimesheetList.Modifiedby = dr["Modifiedby"].ToString().Trim();


                        objlstTimesheet.Add(objTimesheetList);
                    }
                }

            }

            catch (Exception ex)
            {
                objComm.SaveErrorLog("TaskSheetRepository", "GetTaskHistory", ex.Message, "");

            }
            return objlstTimesheet;

        }


        public List<JiraIdeListDBModel> GetJiraRecord(string JiraId)
        {
            List<JiraIdeListDBModel> objlstTimesheet = new List<JiraIdeListDBModel>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                    {
                    new SqlParameter("@JiraId",JiraId),

                    };
                DataSet ds = objDB.getDataFromDBToDataSet("[Q_Pr_GetTimesheetByJiraId]", param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        JiraIdeListDBModel objTimesheetList = new JiraIdeListDBModel();

                        objTimesheetList.timesheetid = Convert.ToInt32(dr["timesheetid"].ToString().Trim());
                        objTimesheetList.JiraId = dr["JiraId"].ToString().Trim();
                        objTimesheetList.WorkedDate = dr["WorkedDate"].ToString().Trim();
                        objTimesheetList.Description = dr["Description"].ToString().Trim();
                        objTimesheetList.minutesSpent = dr["minutesSpent"].ToString().Trim();
                        objTimesheetList.UserId = Convert.ToInt32(dr["UserId"].ToString().Trim());
                        objTimesheetList.Addeddate = dr["Addeddate"].ToString().Trim();
                        //objTimesheetList.modifiedby = dr["modifiedby"].ToString().Trim();



                        objlstTimesheet.Add(objTimesheetList);
                    }
                }

            }

            catch (Exception ex)
            {
                objComm.SaveErrorLog("TimeSheetRepository", "GetTimesheetList", ex.Message, "");

            }
            return objlstTimesheet;

        }


        public List<TimeSheetListDBModel> GetTimesheetList(string? JiraID, string? ProjectName, string? AssignedTo, string? Status, int PageIndex, int PageSize)
        {
            List<TimeSheetListDBModel> objlstTimesheet = new List<TimeSheetListDBModel>();
            DataTable dtFirstTable = new DataTable();
            int totalRecord = 0;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                    {
                    new SqlParameter("@JiraID",JiraID),
                    new SqlParameter("@ProjectName",ProjectName),
                    new SqlParameter("@AssignedTo",AssignedTo),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@PageIndex",PageIndex),
                    new SqlParameter("@PageSize",PageSize)
                    };
                DataSet ds = objDB.getDataFromDBToDataSet("[Q_Pr_GetTaskSheet]", param);
                dtFirstTable = ds.Tables[0];
                if (dtFirstTable.Rows.Count > 0)
                {
                    DataRow row = dtFirstTable.Rows[0];
                    totalRecord = Convert.ToInt32(row["TotalRec"]);
                }
                if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        TimeSheetListDBModel objTimesheetList = new TimeSheetListDBModel();
                        objTimesheetList.TotalRecords = totalRecord;
                        objTimesheetList.TasksheetId = Convert.ToInt32(dr["TasksheetId"].ToString().Trim());
                        objTimesheetList.JiraID = dr["JiraID"].ToString().Trim();
                        objTimesheetList.Task = dr["Task"].ToString().Trim();
                        objTimesheetList.ProjectName = dr["ProjectName"].ToString().Trim();
                        objTimesheetList.resourcesname = dr["resourcesname"].ToString().Trim();
                        objTimesheetList.EstimatePlanDate = Convert.ToDateTime(dr["EstimatePlanDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                        objTimesheetList.EstimatecompletionDate = Convert.ToDateTime(dr["EstimatecompletionDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                        objTimesheetList.Estimatedhours = Convert.ToInt32(dr["Estimatedhours"].ToString().Trim());
                        if (!String.IsNullOrEmpty(Convert.ToString(dr["Actualstartdate"])))
                        {
                            objTimesheetList.Actualstartdate = Convert.ToDateTime(dr["Actualstartdate"].ToString()).ToString("yyyy-MM-dd");
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["Actualenddate"])))
                        {
                            objTimesheetList.Actualenddate = Convert.ToDateTime(dr["Actualenddate"].ToString()).ToString("yyyy-MM-dd");
                        }

                        objTimesheetList.Actualhours = string.IsNullOrEmpty(dr["Actualhourss"].ToString()) ? 0 : Convert.ToSingle(dr["Actualhourss"].ToString().Trim());


                        objlstTimesheet.Add(objTimesheetList);
                    }
                }

            }

            catch (Exception ex)
            {
                objComm.SaveErrorLog("TaskSheetRepository", "GetTimesheetList", ex.Message, "");

            }
            return objlstTimesheet;

        }



        public DataTable CheckJiraId(string JiraId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@JiraId", JiraId),
                };
                ds = objDB.getDataFromDBToDataSet("Q_Pr_CheckExistingJiraId", param);

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("TaskSheetRepository", "GetTimesheerDetails", ex.Message, "");
            }

            return dt;
        }



        public DataTable CheckJiraIdinTasksheet(string JiraId)
        {
            JiraId = "DS-" + JiraId.Trim();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@JiraId", JiraId),
                };
                ds = objDB.getDataFromDBToDataSet("Q_Pr_CheckExistingJiraId_Tasksheet", param);


                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("TaskSheetRepository", "GetTimesheerDetails", ex.Message, "");
            }

            return dt;
        }

        public GetTimeSheetDBModel GetTasksheetDetails(int TasksheetId)
        {
            GetTimeSheetDBModel objTimesheetDModel = new GetTimeSheetDBModel();
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@TasksheetId", TasksheetId),
                };
                ds = objDB.getDataFromDBToDataSet("Q_Pr_GetTaskSheetbyID", param);


                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    objTimesheetDModel.TasksheetId = Convert.ToInt32(ds.Tables[0].Rows[0]["TasksheetId"]);
                    objTimesheetDModel.JiraID = ds.Tables[0].Rows[0]["JiraID"].ToString();
                    objTimesheetDModel.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    objTimesheetDModel.Task = ds.Tables[0].Rows[0]["Task"].ToString();
                    //objTimesheetDModel.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    objTimesheetDModel.ReqFrom = ds.Tables[0].Rows[0]["ReqFrom"].ToString();
                    objTimesheetDModel.ReqDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReqDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                    objTimesheetDModel.EstimatePlanDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EstimatePlanDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                    objTimesheetDModel.EstimatecompletionDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EstimatecompletionDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                    objTimesheetDModel.Estimatedhours = Convert.ToInt32(ds.Tables[0].Rows[0]["Estimatedhours"]);
                    if (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Actualstartdate"])))
                    {
                        objTimesheetDModel.Actualstartdate = (ds.Tables[0].Rows[0]["Actualstartdate"].ToString());
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Actualenddate"])))
                    {
                        objTimesheetDModel.Actualenddate = (ds.Tables[0].Rows[0]["Actualenddate"].ToString());
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Actualhours"])))
                    {
                        objTimesheetDModel.Actualhours = Convert.ToInt32(ds.Tables[0].Rows[0]["Actualhours"]);
                    }

                    objTimesheetDModel.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    objTimesheetDModel.AssignedTo = ds.Tables[0].Rows[0]["AssignedTo"].ToString();
                    objTimesheetDModel.Discription = ds.Tables[0].Rows[0]["Discription"].ToString();





                }

            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("TimeSheetRepository", "GetTasksheetDetails", ex.Message, "");
            }

            return objTimesheetDModel;
        }

        public string UpdateTimesheet(SaveTimeSheetListDBModel SaveTimeSheetModel)
        {

            int result = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[]
                {

                new SqlParameter("@TasksheetId ", SaveTimeSheetModel.TasksheetId ),
                  new SqlParameter("@JiraID ", SaveTimeSheetModel.JiraID ),
                  new SqlParameter("@ProjectName ", SaveTimeSheetModel.ProjectName ),
                  new SqlParameter("@Task ", SaveTimeSheetModel.Task ),
				  //new SqlParameter("@Remarks ", SaveTimeSheetModel.Remarks ),
				  new SqlParameter("@ReqFrom ", SaveTimeSheetModel.ReqFrom ),
                  new SqlParameter("@ReqDate", SaveTimeSheetModel.ReqDate.ToString()),
                  new SqlParameter("@EstimatePlanDate", SaveTimeSheetModel.EstimatePlanDate.ToString()),
                  new SqlParameter("@EstimatecompletionDate",SaveTimeSheetModel.EstimatecompletionDate.ToString()),
                  new SqlParameter("@Estimatedhours ", SaveTimeSheetModel.Estimatedhours ),
                  new SqlParameter("@Actualstartdate ", SaveTimeSheetModel.Actualstartdate ),
                  new SqlParameter("@Actualenddate ", SaveTimeSheetModel.Actualenddate ),
                  new SqlParameter("@Actualhours ", SaveTimeSheetModel.Actualhours ),
                  new SqlParameter("@Status ", SaveTimeSheetModel.Status ),
                  new SqlParameter("@AssignedTo ", SaveTimeSheetModel.AssignedTo ),
                  new SqlParameter("@Discription ", SaveTimeSheetModel.Discription ),
                  new SqlParameter("@modifiedby ", SaveTimeSheetModel.ModifiedBy ),


                };

                result = objDB.InsertData("Q_Pr_UpdateTaskSheetManagment", param);
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("TaskSheetRepository", "UpdateTimesheet", ex.Message, "");
            }
            return result > 0 ? "Data saved Successfully" : "Error occured";
        }



        public string SaveTasksheet(SaveTimeSheetListDBModel SaveTimeSheetModel)
        {

            int result = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[]
                {
                  new SqlParameter("@JiraID ", SaveTimeSheetModel.JiraID ),
                  new SqlParameter("@ProjectName ", SaveTimeSheetModel.ProjectName ),
                  new SqlParameter("@Task ", SaveTimeSheetModel.Task ),
                  new SqlParameter("@ReqFrom ", SaveTimeSheetModel.ReqFrom ),
                  new SqlParameter("@ReqDate ", SaveTimeSheetModel.ReqDate ),
                  new SqlParameter("@EstimatePlanDate ", SaveTimeSheetModel.EstimatePlanDate ),
                  new SqlParameter("@EstimatecompletionDate ", SaveTimeSheetModel.EstimatecompletionDate ),
                  new SqlParameter("@Estimatedhours ", SaveTimeSheetModel.Estimatedhours ),
                  new SqlParameter("@Actualstartdate ", SaveTimeSheetModel.Actualstartdate ),
                  new SqlParameter("@Actualenddate ", SaveTimeSheetModel.Actualenddate ),
                  new SqlParameter("@Actualhours ", SaveTimeSheetModel.Actualhours ),
                  new SqlParameter("@Status ", SaveTimeSheetModel.Status ),
                  new SqlParameter("@AssignedTo ", SaveTimeSheetModel.AssignedTo ),
                  new SqlParameter("@Discription ", SaveTimeSheetModel.Discription ),
                  new SqlParameter("@modifiedby ", SaveTimeSheetModel.ModifiedBy ),


                };

                result = objDB.InsertData("Q_Pr_SavetaskSheet", param);
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("TaskSheetRepository", "SaveTasksheet", ex.Message, "");
            }
            return result > 0 ? "Data saved Successfully" : "Error occured";
        }



        public List<SearchDbModel> GetSearchKeywordsForTasksheet(string SearchKeywords)
        {
            List<SearchDbModel> objSearchList = new List<SearchDbModel>();
            DataSet dt = new DataSet();

            try
            {
                SqlParameter param1 = new SqlParameter("@Keyword", SearchKeywords);

                SqlParameter[] parameters = new SqlParameter[]
                {
                    param1
                };

                dt = objDB.getDataFromDBToDataSet("Q_Pr_GetSearchRes_Task", parameters);

                if (dt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Tables[0].Rows)
                    {
                        SearchDbModel objSearch = new SearchDbModel();
                        objSearch.JiraID = row["JiraID"].ToString();
                        //objSearch.Task = row["Task"].ToString();

                        objSearchList.Add(objSearch);
                    }
                }
            }
            catch (Exception ex)
            {
                ///ErrorLogRepository errorLogRepository = new ErrorLogRepository();
                //errorLogRepository.InsertErrorLog("SearchRepository.cs", "GetSearchKeywords", ex.Message);
            }

            return objSearchList;
        }






        public List<UserlstDBModel> GetSearchUserLst(string SearchKeywords)
        {
            List<UserlstDBModel> objSearchList = new List<UserlstDBModel>();
            DataSet dt = new DataSet();

            try
            {
                SqlParameter param1 = new SqlParameter("@Keyword", SearchKeywords);

                SqlParameter[] parameters = new SqlParameter[]
                {
                    param1
                };

                dt = objDB.getDataFromDBToDataSet("Q_Pr_GetUserNameList", parameters);

                if (dt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Tables[0].Rows)
                    {
                        UserlstDBModel objSearch = new UserlstDBModel();
                        objSearch.UserFullName = row["UserFullName"].ToString();
                        //objSearch.Task = row["Task"].ToString();

                        objSearchList.Add(objSearch);
                    }
                }
            }
            catch (Exception ex)
            {
                ///ErrorLogRepository errorLogRepository = new ErrorLogRepository();
                //errorLogRepository.InsertErrorLog("SearchRepository.cs", "GetSearchKeywords", ex.Message);
            }

            return objSearchList;
        }


    }
}



