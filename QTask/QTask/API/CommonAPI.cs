using QTaskDataLayer.Repository;

namespace QTask.API
{
    public class CommonAPI
    {

        public static string getIPAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }

        public static string getBrowserDetails(HttpContext context)
        {
            return context.Request.Headers["User-Agent"].ToString();
        }

       

      

        
    }
}
