namespace QTask.Models
{
    public class DeleteUserModel
    {
        //public string UserName { get; set; }

        public List<UserList> lstUserList { get; set; }
    }

    public class UserList
    {
        public string Id { get; set; }
    }
}
