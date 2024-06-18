using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class TimesheetDBModel
    {
        public int TimesheetId { get; set; }
        public string JiraId { get; set; }
        public string? Task { get; set; }
        public string WorkedDate { get; set; }
        public string Description { get; set; }
        public string MinSpend { get; set; }
        public int UserId { get; set; }
        public double? HourSpend { get; set; }
        public int TotalRecords { get; set; }

        public List<TimesheetPaginationDBModel> paginationDModels { get; set; }
    }
    public class TimesheetPaginationDBModel
    {
        public int TotalPageCount { get; set; }
        public int PreviousPageIndex { get; set; }
        public int NextPageIndex { get; set; }
    }

    public class SearchDbModel
    {
        public string JiraID { get; set; }

        public string Task { get; set; }
    }

    public class SearchUserDbModel
    {
        public string JiraID { get; set; }

        public string Task { get; set; }
    }
}
