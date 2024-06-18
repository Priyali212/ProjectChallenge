namespace QTask.Models
{
    public class RolesModel
    {
        public string RolesId { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string RoleName { get; set; }
        public string RoleDescriptioin { get; set; }
        public bool? Deleted { get; set; }
    }

    public class RolesWiseUserListModel
    {
        public string RoleId { get; set; }
        public string Userid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string RoleDescriptioin { get; set; }

    }

    public class RoleWiseAccesses
    {
        public string? RoleId { get; set; }
        public string? UserName { get; set; }

        public List<RoleWiseAccessData> lstRoleWiseAccess { get; set; }
    }

    public class CategaryWiseAccess
    {
        public string? CategaryName { get; set; }
        public string? View { get; set; }
        public string? List { get; set; }
        public string? MassUpdate { get; set; }
        public string? Access { get; set; }
        public string? Edit { get; set; }
        public string? Delete { get; set; }
        public string? Export { get; set; }
        public string? Import { get; set; }

    }

    public class RoleModalaccess
    {
        public int? RoleId { get; set; }
        public List<RoleCategory> lstCategory { get; set; }
        public List<RoleModalAccessName> lstRoleAccess { get; set; }
        public List<RoleAccessType> lstAccessType { get; set; }
        public List<RoleWiseAccessData> lstRoleWiseAccessData { get; set; }
    }

    public class RoleCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class RoleAccessType
    {
        public int Id { get; set; }
        public string AccessTypeName { get; set; }
    }
    public class RoleModalAccessName
    {
        public int AccessId { get; set; }
        public string AccessName { get; set; }
        public string AccessFor { get; set; }
    }

    public class RoleWiseAccessData
    {
        public string RoleId { get; set; }
        public string ActionId { get; set; }// Id of the Not Set, Enabled, Disabled
        public string? RoleName { get; set; }
        public string? accessOverride { get; set; }
        public string? AccessName { get; set; }
        public string? AccessCategory { get; set; }
        public string? AclType { get; set; }
        public string? Access { get; set; }
        public string AccessId { get; set; } // Id of View, Edit, Delete, Export
        public string PageId { get; set; } //id of the pages
    }

    public class Role
    {
        public int? Id { get; set; }
        public string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
