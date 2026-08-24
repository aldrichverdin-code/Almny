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
public class EnrollmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EnrollmentsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Enroll(
        EnrollCourseDto dto)
    {
        var studentId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (studentId == null)
            return Unauthorized();

        var alreadyEnrolled = await _context.Enrollments
            .AnyAsync(x =>
                x.StudentId == Guid.Parse(studentId)
                && x.CourseId == dto.CourseId);

        if (alreadyEnrolled)
            return BadRequest("Already enrolled");

        var enrollment = new Enrollment
        {
            StudentId = Guid.Parse(studentId),
            CourseId = dto.CourseId
        };

        _context.Enrollments.Add(enrollment);

        await _context.SaveChangesAsync();

        return Ok(enrollment);
    }

    [Authorize]
    [HttpGet("my-courses")]
    public async Task<IActionResult> MyCourses()
    {
        var studentId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (studentId == null)
            return Unauthorized();

        var courses = await _context.Enrollments
            .Include(x => x.Course)
            .Where(x =>
                x.StudentId == Guid.Parse(studentId))
            .Select(x => x.Course)
            .ToListAsync();

        return Ok(courses);
    }
}