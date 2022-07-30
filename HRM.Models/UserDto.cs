namespace HRM.Models
{
    public class UserDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public Guid ManagerId { get; set; }

    }
    public class CreateUserDto
    {
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Password { get; set; }

    }
    
}



