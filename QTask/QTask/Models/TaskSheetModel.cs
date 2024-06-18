namespace QTask.Models
{

    public class ResourceslstModel
    {

        public int resourcesid { get; set; }

        public string resourcesname { get; set; }
    }

    public class UserlstModel
    {

        public int id { get; set; }

        public string UserFullName { get; set; }
    }
    public class HistoryListModel
    {

        public string JiraID { get; set; }

        public int TasksheetId { get; set; }

        public string Task { get; set; }
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



    public class JiraIdeListModel
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

    public class TimeSheetListModel
    {

        public string? JiraID { get; set; }

        public int TasksheetId { get; set; }

        public string? Task { get; set; }
        public string? ProjectName { get; set; }
        public string? resourcesname { get; set; }
        public DateTime ModifiedON { get; set; }
        public int Estimatedhours { get; set; }
        public string Actualstartdate { get; set; }
        public string Actualenddate { get; set; }
        public double Actualhours { get; set; }
        public string EstimatePlanDate { get; set; }
        public string Status { get; set; }
        public string EstimatecompletionDate { get; set; }
        public int TotalRecords { get; set; }
        public PaginationModel paginationModels { get; set; }
    }


    public class PaginationModel
    {
        public int TotalPageCount { get; set; }
        public int PreviousPageIndex { get; set; }
        public int NextPageIndex { get; set; }
        public int PageIndex { get; set; }
    }

    public class GetTimeSheetModel
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
    public class SaveTimeSheetModel : OtherModel
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
    public class SearchTaskModel
    {
        public int ID { get; set; }

        public string SUMMARY { get; set; }
    }
}
