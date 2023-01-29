namespace eVoucherApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
