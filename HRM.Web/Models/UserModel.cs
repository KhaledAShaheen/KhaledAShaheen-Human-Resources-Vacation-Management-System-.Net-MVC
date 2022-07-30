using HRM.Models;

namespace HRM.Web.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Password { get; set; }
    }
}
