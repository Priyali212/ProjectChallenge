using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class ContactListDBModel
    {
        public int TotalCount { get; set; }

        public int PageIndex { get; set; }
        public List<ContactListDB> contactLists { get; set; }
    }

    public class ContactListDB
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool IsAdmin { get; set; }
        public string GroupName { get; set; }
    }

    public class CreateUserDBModal
    {
        public string? ID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool? IsAdmin { get; set; }
        public DateTime? DateEntered { get; set; }
        public int? GroupId { get; set; }
    }
}
