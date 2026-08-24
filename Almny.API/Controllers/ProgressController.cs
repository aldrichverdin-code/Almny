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
public class ProgressController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProgressController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteLesson(
        CompleteLessonDto dto)
    {
        var studentId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (studentId == null)
            return Unauthorized();

        var exists = await _context.LessonProgresses
            .AnyAsync(x =>
                x.StudentId == Guid.Parse(studentId)
                && x.LessonId == dto.LessonId);

        if (exists)
            return BadRequest("Lesson already completed");

        var progress = new LessonProgress
        {
            StudentId = Guid.Parse(studentId),
            LessonId = dto.LessonId,
            IsCompleted = true,
            CompletedAt = DateTime.UtcNow
        };

        _context.LessonProgresses.Add(progress);

        await _context.SaveChangesAsync();

        return Ok(progress);
    }

    [Authorize]
    [HttpGet("my-progress")]
    public async Task<IActionResult> MyProgress()
    {
        var studentId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (studentId == null)
            return Unauthorized();

        var progress = await _context.LessonProgresses
            .Include(x => x.Lesson)
            .Where(x =>
                x.StudentId == Guid.Parse(studentId))
            .ToListAsync();

        return Ok(progress);
    }
}