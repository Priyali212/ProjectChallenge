using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class DeleteUserDBModel
    {
        public string UserName { get; set; }

        public List<UserListDB> lstUserList { get; set; }
    }
    public class UserListDB
    {
        public string Id { get; set; }
    }
}
