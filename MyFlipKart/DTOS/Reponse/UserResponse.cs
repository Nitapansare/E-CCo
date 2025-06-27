namespace MyFlipKart.DTOS.Reponse
{
    public class UserRegResponse
    {
       public string Status { get; set; }
       public int? UserId { get; set; }
       public DateTime? CreatedDate { get; set; }
       public string Message { get; set; }
    }

    public class LoginResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
    public class UserListResponse 
    {
        public string Name { get; set; }
        public string? Email_Id { get; set; }   
        public DateTime CreatedDate { get; set; }  
        public string? phone { get; set; }   
        public string Address { get; set; }   

    }

   

}
