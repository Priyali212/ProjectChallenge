using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class LoginDBModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string UserSession { get; set; }
        public string Photo { get; set; }
    }

    public class UserDetailsDB
    {
        public string Id { get; set;}
        public string UserName { get; set;}
        public string UserFullName { get; set;}
        public bool IsAdmin { get; set;}
    }
}
