using eVoucherManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Protocol;
using RestSharp;
using System.Linq;
using System.Text.Json;

namespace eVoucherManagementSystem.Controllers
{
    public class CMSController : Controller
    {
        private readonly RestClient _client;
        private IWebHostEnvironment _environment;

        public CMSController(IWebHostEnvironment environment)
        {
            _client = new RestClient("https://localhost:7142/");
            _environment = environment;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel viewModel)
        {
            var request = new RestRequest("api/register").AddJsonBody(viewModel);
            var response = await _client.ExecutePostAsync(request);

            if (!response.IsSuccessful)
            {
                return await (Task.FromResult(View()));
            }
            return await (Task.FromResult(RedirectToAction("Login")));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel viewModel)
        {
            var request = new RestRequest("api/login").AddJsonBody(viewModel);
            var response = await _client.ExecutePostAsync(request);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", "Login Failed");
                return await (Task.FromResult(View(viewModel)));
            }
            var dataResult = JsonConvert.DeserializeObject<LoginResponseModel>(response.Content);
            HttpContext.Session.SetString("token", dataResult.Token);
            string getToken = HttpContext.Session.GetString("token");
            return await (Task.FromResult(RedirectToAction("GetEVoucherList")));
        }
        public IActionResult CreateEVoucher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEVoucher(EVoucherViewModel viewModel)
        {
            var imagePath = UploadImage(viewModel.file);
            viewModel.Image = imagePath;
            viewModel.Token = HttpContext.Session.GetString("token");
            var request = new RestRequest("api/create_voucher").AddJsonBody(viewModel);
            var response = await _client.ExecutePostAsync(request);

            if (!response.IsSuccessful)
            {
                return await (Task.FromResult(View()));
            }
            return await (Task.FromResult(View()));
        }

        public async Task<IActionResult> GetEVoucherList()
        {
            string token = HttpContext.Session.GetString("token");
            var request = new RestRequest($"api/getlist?token={token}");
            var response = await _client.ExecuteGetAsync(request);
            if (!response.IsSuccessful || response.Content == null)
            {
                return await (Task.FromResult(View()));
            }
            var dataResult = JsonConvert.DeserializeObject<List<EVoucherViewModel>>(response.Content);
            return await (Task.FromResult(View(dataResult)));
        }

        public async Task<IActionResult> CheckOut(string Id)
        {
            var request = new RestRequest($"api/evoucherdetails?Id={Id}");
            var response = await _client.ExecuteGetAsync(request);
            if (!response.IsSuccessful || response.Content == null)
            {
                return await (Task.FromResult(View()));
            }
            var dataResult = JsonConvert.DeserializeObject<EVoucherViewModel>(response.Content);
            return await (Task.FromResult(View(dataResult)));
        }


        public async Task<IActionResult> Details(string Id)
        {
            var request = new RestRequest($"api/evoucherdetails?Id={Id}");
            var response = await _client.ExecuteGetAsync(request);
            if (!response.IsSuccessful || response.Content == null)
            {
                return await (Task.FromResult(View()));
            }
            var dataResult = JsonConvert.DeserializeObject<EVoucherViewModel>(response.Content);
            return await (Task.FromResult(View(dataResult)));
        }

        public async Task<IActionResult> Update(string Id)
        {
            var request = new RestRequest($"api/evoucherdetails?Id={Id}");
            var response = await _client.ExecuteGetAsync(request);
            if (!response.IsSuccessful || response.Content == null)
            {
                return await (Task.FromResult(View()));
            }
            var dataResult = JsonConvert.DeserializeObject<EVoucherViewModel>(response.Content);
            return await (Task.FromResult(View(dataResult)));
        }

        [HttpPost]
        public async Task<IActionResult> Update(EVoucherViewModel viewModel)

        {
            if (viewModel.file != null)
            {
                var imagePath = UploadImage(viewModel.file);
                viewModel.Image = imagePath;
            }
            viewModel.Token = HttpContext.Session.GetString("token");
            var request = new RestRequest("api/update_evoucher").AddJsonBody(viewModel);
            var response = await _client.ExecutePutAsync(request);

            if (!response.IsSuccessful)
            {
                return await (Task.FromResult(View()));
            }
            return await (Task.FromResult(RedirectToAction("GetEVoucherList")));
        }

        public async Task<IActionResult> Delete(string Id)
        {
            var request = new RestRequest($"api/delete_evoucher?Id={Id}");
            var response = await _client.ExecutePostAsync(request);
            if (!response.IsSuccessful || response.Content == null)
            {
                return await (Task.FromResult(View()));
            }
            return await (Task.FromResult(RedirectToAction("GetEVoucherList")));
        }

        public string UploadImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                try
                {
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\upload\\" + file.FileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        string FilePath = "\\upload\\" + file.FileName;
                        return FilePath;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return "Unsuccessful";
            }
        }

    }
}
