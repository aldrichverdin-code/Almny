using Almny.Application.DTOs;
using Almny.Domain.Entities;
using Almny.Persistence.Context;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almny.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LessonsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize (Roles = "Instructor,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateLesson(
        CreateLessonDto dto)
    {
        var lesson = new Lesson
        {
            Title = dto.Title,
            VideoUrl = dto.VideoUrl,
            Content = dto.Content,
            Order = dto.Order,
            IsFreePreview = dto.IsFreePreview,
            SectionId = dto.SectionId
        };

        _context.Lessons.Add(lesson);

        await _context.SaveChangesAsync();

        return Ok(lesson);
    }
}