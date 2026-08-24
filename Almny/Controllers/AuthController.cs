using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Almny.Controllers;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuthController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var apiUrl = _config["ApiBaseUrl"];
        var loginData = new { email, password };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{apiUrl}/api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponse>(json);

            if (result?.Token != null)
            {
                // Decode JWT para obtener claims
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(result.Token);
                var claims = jwt.Claims.ToList();
                claims.Add(new Claim("Token", result.Token));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Guardar token en cookie
                Response.Cookies.Append("JwtToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("Index", "Home");
            }
        }

        ViewBag.Error = "Invalid email or password";
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string fullName, string email, string password, string role)
    {
        var apiUrl = _config["ApiBaseUrl"];
        var registerData = new { fullName, email, password, role = role ?? "Student" };
        var content = new StringContent(JsonSerializer.Serialize(registerData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{apiUrl}/api/auth/register", content);

        if (response.IsSuccessStatusCode)
        {
            ViewBag.Success = "Account created successfully! Please sign in.";
            return View("Login");
        }

        ViewBag.Error = "Registration failed. Please try again.";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Cookies.Delete("JwtToken");
        return RedirectToAction("Index", "Home");
    }

    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}