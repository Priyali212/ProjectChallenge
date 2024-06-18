namespace QTask.Models
{
    public class TimesheetModel
    {
        public string JiraId { get; set; }
        public string? Task { get; set; }
        public string WorkedDate { get; set; }
        public string Description { get; set; }
        public string MinSpend { get; set; }
        public int UserId { get; set; }
        public int TimesheetId { get; set; }

        public string? HourSpend { get; set; }
        public int TotalRecords { get; set; }
        public TimesheetPaginationModel? paginationModels { get; set; }
    }
    public class TimesheetPaginationModel
    {
        public int TotalPageCount { get; set; }
        public int PreviousPageIndex { get; set; }
        public int NextPageIndex { get; set; }
        public int PageIndex { get; set; }
    }

    public class SearchModel
    {
        public string JiraID { get; set; }

        public string Task { get; set; }
    }
}
