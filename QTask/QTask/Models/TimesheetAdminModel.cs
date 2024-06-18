namespace QTask.Models
{
	public class TimesheetAdminModel
	{
		public int UserId { get; set; }
		public string? FromDate { get; set; }
		public string? ToDate { get; set; }
		public string JiraId { get; set; }
		public string WorkedDate { get; set; }
		public string Description { get; set; }
		public int MinSpend { get; set; }
		public double HourSpend { get; set; }
		public string Task { get; set; }
		public int TotalRecords { get; set; }
		public AdminPaginationModel paginationModels { get; set; }
	}

	public class AdminPaginationModel
	{
		public int TotalPageCount { get; set; }
		public int PreviousPageIndex { get; set; }
		public int NextPageIndex { get; set; }
		public int PageIndex { get; set; }
	}


    public class GenerateReportModel
    {
		public int TasksheetId { get; set; }
		public int totalRecord { get; set; }
        public string? OpenTask { get; set; }
        public string? ClosedTask { get; set; }

        public string? resourcesname { get; set; }
        //public string JiraId { get; set; }

	}
    public class CheckStatusModel
    {

		public string? resourcesname { get; set; }
        

    }
}

