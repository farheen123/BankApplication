using BankApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JwtTokenService _tokenService;
        public HomeController(IHttpClientFactory httpClientFactory, JwtTokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!_tokenService.IsTokenAvailable())
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.GetToken());

            var response = await client.GetAsync("Login");
            if (response.IsSuccessStatusCode)
            {
                string accountDetails = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<Authdata>(accountDetails);
                return View(account);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _tokenService.StoreToken(null); // Clear the token if unauthorized
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Error = "Unable to load account details.";
            return View();
        }
    }
}
   public class Authdata
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public decimal Balance { get; set; }
}
