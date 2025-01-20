using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankApp.Web.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BankApp.Web.Controllers;

public class AuthController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JwtTokenService _tokenService;

    public AuthController(IHttpClientFactory httpClientFactory, JwtTokenService tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password,string role)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var loginData = new { Username = username, Password = password, Role = role };
        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("Login", content);
        if (response.IsSuccessStatusCode)
       {
            string responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseData);
            var token = JsonConvert.DeserializeObject<TokenResponse>(responseData);
            Console.WriteLine(token);
            _tokenService.StoreToken(token.AccessToken);
            return RedirectToAction("Index", "Dashboards");
        }

        ViewBag.Error = "Invalid credentials. Please try again.";
        return View();
    }
}

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}