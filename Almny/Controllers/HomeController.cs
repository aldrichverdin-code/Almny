using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using Almny.Models;

namespace Almny.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public HomeController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<IActionResult> Index()
    {
        var apiUrl = _config["ApiBaseUrl"];
        var response = await _httpClient.GetAsync($"{apiUrl}/api/courses");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var courses = JsonSerializer.Deserialize<List<Course>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return View(courses);
        }

        return View(new List<Course>());
    }

    public async Task<IActionResult> CourseDetail(Guid id)
    {
        var apiUrl = _config["ApiBaseUrl"];
        var response = await _httpClient.GetAsync($"{apiUrl}/api/courses/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<Course>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return View(course);
        }

        return NotFound();
    }
}