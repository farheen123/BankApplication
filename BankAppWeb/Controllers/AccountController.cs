using BankApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _BaseApi;
        private readonly HttpClient _client;

        public AccountController(IConfiguration configuration)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration["ApiBaseURL:API"]);
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<AccountViewModel> accountList = new List<AccountViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Account/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                accountList = JsonConvert.DeserializeObject<List<AccountViewModel>>(data);
            }
            return View(accountList);
        }

    }
}
