using Almny.Application.DTOs;
using Almny.Domain.Entities;
using Almny.Persistence.Context;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almny.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SectionsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Instructor,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateSection(
        CreateSectionDto dto)
    {
        var section = new Section
        {
            Title = dto.Title,
            Order = dto.Order,
            CourseId = dto.CourseId
        };

        _context.Sections.Add(section);

        await _context.SaveChangesAsync();

        return Ok(section);
    }
}