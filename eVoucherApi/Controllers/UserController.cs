using eVoucherApi.Helper;
using eVoucherApi.Models;
using eVoucherApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherApi.Controllers
{
    public class UserController : Controller
    {
        private UserDbService _service;
        DecryptPassword decrypt = new DecryptPassword();
        EncryptPassword security = new EncryptPassword();

        public UserController(UserDbService service)
        {
            _service = service;
        }

        [HttpPost("api/register")]
        public async Task<ObjectResult> UserRegister([FromBody] UserModel reqModel)
        {
            if (reqModel != null)
            {
                reqModel.Password = security.Encrypt_Password(reqModel.Password);
            }
            var dataResult = await _service.Register(reqModel);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK, "Success") : StatusCode(StatusCodes.Status304NotModified, "No row effected");
        }

        [HttpPost("api/login")]
        public async Task<ObjectResult> Login([FromBody] UserModel reqModel)
        {
            if (reqModel != null)
            {
                reqModel.Password = security.Encrypt_Password(reqModel.Password);
            }
            LoginResponseModel responseModel = new LoginResponseModel();
            var dataResult = await _service.Login(reqModel);
            if (dataResult != null)
            {
                responseModel.Token = JWTManager.GenerateToken(dataResult.PhoneNumber);
            }
            return dataResult != null ? StatusCode(StatusCodes.Status200OK, responseModel) : StatusCode(StatusCodes.Status404NotFound, "User Not Found");
        }
    }
}
