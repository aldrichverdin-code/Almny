using Almny.Application.DTOs;
using Almny.Domain.Entities;
using Almny.Persistence.Context;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace Almny.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Instructor,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        CreateCourseDto dto)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            ThumbnailUrl = dto.ThumbnailUrl,
            Category = dto.Category,
            Price = dto.Price,
            InstructorId = Guid.Parse(userId)
        };

        _context.Courses.Add(course);

        await _context.SaveChangesAsync();

        return Ok(course);
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _context.Courses
            .Include(x => x.Instructor)
            .ToListAsync();

        return Ok(courses);
    }
}