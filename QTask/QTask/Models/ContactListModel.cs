namespace QTask.Models
{
    public class ContactListModel
    {
        public int TotalCount { get; set; }

        public int PageIndex { get; set; }
        public List<ContactList> contactLists { get; set; }
    }

    public class ContactList
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool IsAdmin { get; set; }
        public string GroupName { get; set; }
    }

    public class CreateUserModal
    {
        public string? ID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool? IsAdmin { get; set; }
        public DateTime? DateEntered { get; set; }
        public int? GroupId { get; set; }
    }
}
