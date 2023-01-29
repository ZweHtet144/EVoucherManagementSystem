namespace eVoucherManagementSystem.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class LoginResponseModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
