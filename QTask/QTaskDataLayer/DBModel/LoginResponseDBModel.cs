using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBModel
{
    public class LoginResponseDBModel
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool is_admin { get; set; }
        public bool receive_notifications { get; set; }
        public string title { get; set; }
        public string department { get; set; }
        public string phone_mobile { get; set; }
        public string Activestatus { get; set; }
        public string reports_to_id { get; set; }
        public string is_group { get; set; }
        public string UserSession { get; set; }
        public string Photo { get; set; }
        public string MsgResponse { get; set; }
        public bool Status { get; set; }
    }
}
