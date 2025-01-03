namespace Starcross.Models
{
    public class User
    {
        public Guid user_id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
        public string email { get; set; }
    }
}
