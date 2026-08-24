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
public class ExamsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExamsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Instructor,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateExam(
        CreateExamDto dto)
    {
        var exam = new Exam
        {
            Title = dto.Title,
            DurationMinutes = dto.DurationMinutes,
            CourseId = dto.CourseId
        };

        _context.Exams.Add(exam);

        await _context.SaveChangesAsync();

        return Ok(exam);
    }

    [Authorize(Roles = "Instructor,Admin")]
    [HttpPost("question")]
    public async Task<IActionResult> CreateQuestion(
        CreateQuestionDto dto)
    {
        var question = new Question
        {
            Text = dto.Text,
            OptionA = dto.OptionA,
            OptionB = dto.OptionB,
            OptionC = dto.OptionC,
            OptionD = dto.OptionD,
            CorrectAnswer = dto.CorrectAnswer,
            ExamId = dto.ExamId
        };

        _context.Questions.Add(question);

        await _context.SaveChangesAsync();

        return Ok(question);
    }

    [Authorize]
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitExam(
        SubmitExamDto dto)
    {
        var studentId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (studentId == null)
            return Unauthorized();

        var questions = await _context.Questions
            .Where(q => q.ExamId == dto.ExamId)
            .ToListAsync();

        int score = 0;

        foreach (var question in questions)
        {
            if (dto.Answers.TryGetValue(
                question.Id,
                out var answer))
            {
                if (answer == question.CorrectAnswer)
                {
                    score++;
                }
            }
        }

        var attempt = new ExamAttempt
        {
            StudentId = Guid.Parse(studentId),
            ExamId = dto.ExamId,
            Score = score
        };

        _context.ExamAttempts.Add(attempt);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Score = score,
            Total = questions.Count
        });
    }
}