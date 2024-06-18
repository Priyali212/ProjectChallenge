namespace QTask.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? UserSession { get; set; }
        public string? Photo { get; set; }

        public string Force { get; set; }
    }
}
