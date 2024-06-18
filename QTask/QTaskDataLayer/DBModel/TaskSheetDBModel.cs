using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class ResourceslstDBModel
    {

        public int resourcesid { get; set; }
        public string resourcesname { get; set; }
    }


    public class UserlstDBModel
    {

        public int id { get; set; }

        public string UserFullName { get; set; }
    }
    public class JiraIdeListDBModel
    {

        public string JiraId { get; set; }

        public int timesheetid { get; set; }

        public string WorkedDate { get; set; }
        public string Description { get; set; }
        public string minutesSpent { get; set; }

        public int UserId { get; set; }
        public string Addeddate { get; set; }
        public string modifiedby { get; set; }

    }

    public class HistoryListDBModel
    {
        public string JiraID { get; set; }

        public int TasksheetId { get; set; }

        public string? Task { get; set; }
        public string? ProjectName { get; set; }
        public string? resourcesname { get; set; }
        public DateTime ModifiedON { get; set; }
        public int Estimatedhours { get; set; }
        public string Actualstartdate { get; set; }
        public string Actualenddate { get; set; }
        public int Actualhours { get; set; }
        public string EstimatePlanDate { get; set; }
        public string Status { get; set; }
        public string EstimatecompletionDate { get; set; }

        public string Modifiedby { get; set; }
    }
    public class TimeSheetListDBModel
    {
        public string? JiraID { get; set; }

        public int TasksheetId { get; set; }

        public string Task { get; set; }
        public string? ProjectName { get; set; }
        public string? resourcesname { get; set; }
        public DateTime ModifiedON { get; set; }
        public int Estimatedhours { get; set; }
        public string Actualstartdate { get; set; }
        public string Actualenddate { get; set; }
        public float Actualhours { get; set; }
        public string EstimatePlanDate { get; set; }
        public string Status { get; set; }
        public string EstimatecompletionDate { get; set; }

        public int TotalRecords { get; set; }

        public List<PaginationDBModel> paginationDModels { get; set; }
    }
    public class PaginationDBModel
    {
        public int TotalPageCount { get; set; }
        public int PreviousPageIndex { get; set; }
        public int NextPageIndex { get; set; }
    }

    public class GetTimeSheetDBModel
    {
        public int TasksheetId { get; set; }
        public string JiraID { get; set; }
        public string ProjectName { get; set; }
        public string? Task { get; set; }
        //public string? Remarks { get; set; }
        public string ReqFrom { get; set; }
        public string ReqDate { get; set; }

        public string EstimatePlanDate { get; set; }
        public string EstimatecompletionDate { get; set; }
        public int Estimatedhours { get; set; }
        public string Actualstartdate { get; set; }
        public string Actualenddate { get; set; }
        public int Actualhours { get; set; }
        public string Status { get; set; }
        public string Addeddate { get; set; }
        public string AssignedTo { get; set; }

        public string Discription { get; set; }
        public string modifiedby { get; set; }

        public string ModifiedON { get; set; }

    }

    public class SaveTimeSheetListDBModel : OtherDBModel
    {
        public int TasksheetId { get; set; }
        public string JiraID { get; set; }
        public string ProjectName { get; set; }
        public string? Task { get; set; }

        //public string? Remarks { get; set; }
        public string ReqFrom { get; set; }
        public string ReqDate { get; set; }

        public string? EstimatePlanDate { get; set; }
        public string? EstimatecompletionDate { get; set; }

        public int Estimatedhours { get; set; }
        public string? Actualstartdate { get; set; }
        public string? Actualenddate { get; set; }
        public int? Actualhours { get; set; }
        public string Status { get; set; }

        //public string Addeddate { get; set; }
        public string AssignedTo { get; set; }

        public string? Discription { get; set; }





    }
    public class SearchTaskDbModel
    {
        public int ID { get; set; }

        public string SUMMARY { get; set; }
    }
}
