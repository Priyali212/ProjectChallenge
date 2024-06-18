using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
	public class TimesheetAdminDBModel
	{
		public int UserId { get; set; }
		public string FromDate { get; set; }
		public string ToDate { get; set; }
		public string JiraId { get; set; }
		public string WorkedDate { get; set; }
		public string Description { get; set; }
		public int MinSpend { get; set; }
		public string Task { get; set; }
		public int TotalRecords { get; set; }

		public List<AdminPaginationDBModel> paginationDModels { get; set; }
	}
	public class AdminPaginationDBModel
	{
		public int TotalPageCount { get; set; }
		public int PreviousPageIndex { get; set; }
		public int NextPageIndex { get; set; }
	}


	public class GenerateReportDBModel
	{

		public int totalRecord { get; set; }
		public int OpenTask { get; set; }
		public int ClosedTask { get; set; }

		public string? resourcesname { get; set; }
		//public string JiraId { get; set; }


	}


	public class CheckStatusDBModel
	{

		public string? resourcesname { get; set; }
		
	}
 }

    
