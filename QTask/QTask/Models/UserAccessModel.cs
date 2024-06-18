namespace QTask.Models
{
    public class UserAccessModel
    {
        public string UserID { get; set; }
        public string RoleId { get; set; }
        public string ActionId { get; set; }
        public string RoleName { get; set; }
        public int AccessOverride { get; set; }
        public string AccessName { get; set; }
        public string Category { get; set; }
        public string AclType { get; set; }
        public string Access { get; set; }
        public string PageId { get; set; }
        public string PageName { get; set; }
    }
}
