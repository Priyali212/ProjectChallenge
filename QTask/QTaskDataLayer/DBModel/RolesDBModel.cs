using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class RolesDBModel
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

    public class RolesWiseUserListDBModel
    {
        public string RoleId { get; set; }
        public string Userid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string RoleDescriptioin { get; set; }

    }

    public class RoleWiseAccessesDB
    {
        public string RoleId { get; set; }
        public string UserName { get; set; }

        public List<RoleDBWiseAccessData> lstRoleDBWiseAccess { get; set; }
    }

    public class CategaryWiseAccessDB
    {
        public string CategaryName { get; set; }
        public string View { get; set; }
        public string List { get; set; }
        public string MassUpdate { get; set; }
        public string Access { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
        public string Export { get; set; }
        public string Import { get; set; }

    }

    public class RoleDBModalaccess
    {
        public int? RoleId { get; set; }
        public List<RoleDBCategory> lstCategory { get; set; }
        public List<RoleDBModalAccessName> lstRoleAccess { get; set; }
        public List<RoleDBAccessType> lstAccessType { get; set; }
        public List<RoleDBWiseAccessData> lstRoleWiseAccessData { get; set; }
    }

    public class RoleDBCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class RoleDBAccessType
    {
        public int Id { get; set; }
        public string AccessTypeName { get; set; }
    }

    public class RoleDBModalAccessName
    {
        public int AccessId { get; set; }
        public string AccessName { get; set; }
        public string AccessFor { get; set; }
    }

    public class RoleDBWiseAccessData
    {
        public string RoleId { get; set; }
        public string ActionId { get; set; }
        public string? RoleName { get; set; }
        public string? accessOverride { get; set; }
        public string? AccessName { get; set; }
        public string? AccessCategory { get; set; }
        public string? AclType { get; set; }
        public string? Access { get; set; }
        public string AccessId { get; set; }
        public string PageId { get; set; }
    }

    public class RoleDB
    {
        public int? Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
