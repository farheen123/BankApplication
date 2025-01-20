using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using BankApp.Web.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BankApp.Web.Controllers;

public class DashboardsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JwtTokenService _tokenService;
    public DashboardsController(IHttpClientFactory httpClientFactory, JwtTokenService tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //if (!_tokenService.IsTokenAvailable())
        //{
        //    return RedirectToAction("Login", "Auth");
        //}

        //var client = _httpClientFactory.CreateClient("ApiClient");
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.GetToken());

        //var response = await client.GetAsync("Login"); 
        //if (response.IsSuccessStatusCode)
        //{
        //    string accountDetails = await response.Content.ReadAsStringAsync();
        //    var account = JsonConvert.DeserializeObject<Account>(accountDetails);
        //    return View(account);
        //}

        //if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //{
        //    _tokenService.StoreToken(null); // Clear the token if unauthorized
        //    return RedirectToAction("Login", "Auth");
        //}

        //ViewBag.Error = "Unable to load account details.";
        return View();
    }
}
public class Account
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public decimal Balance { get; set; }
}
