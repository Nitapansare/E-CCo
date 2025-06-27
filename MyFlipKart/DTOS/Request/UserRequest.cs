using System.ComponentModel.DataAnnotations;

namespace MyFlipKart.DTOS.Request
{
    public class UserRegRequest
    {
        public string Name { get; set; }
        public string Email_Id { get; set; }
        public string password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class LoginRequest
    {
        public string Email_Id { get; set; }
        public string Password { get; set; }
    }
    

}
