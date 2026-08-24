using Almny.Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almny.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly IAIQuizService _aiQuizService;

    public AIController(IAIQuizService aiQuizService)
    {
        _aiQuizService = aiQuizService;
    }

    [Authorize(Roles = "Instructor,Admin")]
    [HttpPost("generate-quiz")]
    public async Task<IActionResult> GenerateQuiz(
        [FromBody] string lessonContent)
    {
        var questions =
            await _aiQuizService
                .GenerateQuestions(lessonContent);

        return Ok(questions);
    }
}