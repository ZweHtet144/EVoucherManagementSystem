using eVoucherApi.Helper;
using eVoucherApi.Models;
using eVoucherApi.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace eVoucherApi.Controllers
{
    [ApiController]
    public class EVoucherController : Controller
    {
        private readonly EVoucherDbServices _eVoucherservice;

        public EVoucherController(EVoucherDbServices eVoucherDbServices)
        {
            _eVoucherservice = eVoucherDbServices;
        }

        [HttpGet("api/getlist")]

        public async Task<ObjectResult> GetEVoucherList(string token)
        {
            var validateToken = JWTManager.ValidateToken(token);
            if (string.IsNullOrEmpty(validateToken))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "UnAuthorized Request");
            }
            var dataResult = await _eVoucherservice.GetEVoucherList(validateToken);
            return dataResult.Count >= 0 ? StatusCode(StatusCodes.Status200OK, dataResult) : StatusCode(StatusCodes.Status404NotFound, "No Data");
        }

        [HttpPost("api/create_voucher")]
        public async Task<ObjectResult> CreateEVoucher([FromBody] EVoucherRequestModel reqModel)
        {

            var validateToken = JWTManager.ValidateToken(reqModel.Token);
            if (string.IsNullOrEmpty(validateToken))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "UnAuthorized Request");
            }
            EvoucherModel model = new EvoucherModel();
            model.Title = reqModel.Title;
            model.Name = reqModel.Name;
            model.Description = reqModel.Description;
            model.ExpiryDate = reqModel.ExpiryDate;
            model.Quantity = reqModel.Quantity;
            model.limit = reqModel.limit;
            model.PhoneNumber = reqModel.PhoneNumber;
            model.PaymentMethod = reqModel.PaymentMethod;
            model.BuyType = reqModel.BuyType;
            model.Amount = reqModel.Amount;
            model.PhoneNumber = reqModel.PhoneNumber;
            model.Image = reqModel.Image;

            var dataResult = await _eVoucherservice.CreateEVoucher(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK, dataResult) : StatusCode(StatusCodes.Status304NotModified, "No Row Effected");
        }


        [HttpPut("api/update_evoucher")]
        public async Task<ObjectResult> UpdateEVoucher([FromBody] EVoucherRequestModel reqModel)
        {
            var validateToken = JWTManager.ValidateToken(reqModel.Token);
            if (string.IsNullOrEmpty(validateToken))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "UnAuthorized Request");
            }
            EvoucherModel model = new EvoucherModel();
            model.Id = reqModel.Id;
            model.Title = reqModel.Title;
            model.Name = reqModel.Name;
            model.Description = reqModel.Description;
            model.ExpiryDate = reqModel.ExpiryDate;
            model.Quantity = reqModel.Quantity;
            model.limit = reqModel.limit;
            model.PhoneNumber = reqModel.PhoneNumber;
            model.PaymentMethod = reqModel.PaymentMethod;
            model.BuyType = reqModel.BuyType;
            model.Amount = reqModel.Amount;
            model.PhoneNumber = reqModel.PhoneNumber;
            model.Image = reqModel.Image;
            var dataResult = await _eVoucherservice.UpdateEVoucher(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK, "Success") : StatusCode(StatusCodes.Status304NotModified, "No Row Effected");
        }

        [HttpGet("api/evoucherdetails")]
        public async Task<ObjectResult> EVoucherDetails([FromQuery] int Id)
        {
            var dataResult = await _eVoucherservice.EVoucherDetails(Id);
            return dataResult != null ? StatusCode(StatusCodes.Status200OK, dataResult) : StatusCode(StatusCodes.Status404NotFound, "No Data");
        }

        [HttpPost("api/delete_evoucher")]
        public async Task<ObjectResult> DeleteEVoucher(int Id)
        {
            var dataResult = await _eVoucherservice.DeleteEVocuher(Id);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK, "Success") : StatusCode(StatusCodes.Status304NotModified, "No Row Effected");
        }

    }
}
