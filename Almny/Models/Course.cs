using System.Text.Json.Serialization;

namespace Almny.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public Guid? InstructorId { get; set; }
    public User? Instructor { get; set; }
    public List<Section> Sections { get; set; } = new();
    public List<Exam> Exams { get; set; } = new();
}

public class Section
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public List<Lesson> Lessons { get; set; } = new();
}

public class Lesson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }
    public bool IsFreePreview { get; set; }
    public Guid SectionId { get; set; }
    public Section? Section { get; set; }
}

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Student";
}

public class Exam
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public List<Question> Questions { get; set; } = new();
}

public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? OptionA { get; set; }
    public string? OptionB { get; set; }
    public string? OptionC { get; set; }
    public string? OptionD { get; set; }
    public string CorrectAnswer { get; set; } = "A";
    public Guid ExamId { get; set; }
    public Exam? Exam { get; set; }
}

public class Enrollment
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}

public class Progress
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}