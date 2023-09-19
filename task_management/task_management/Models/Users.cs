namespace task_management.Models
{
    public class Users
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class UserDetails
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string contactno { get; set; }
        public string gender { get; set; }

        public Result result { get; set; }
    }

    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
