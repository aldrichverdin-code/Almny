using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ThumbnailUrl { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public Guid InstructorId { get; set; }

        public User Instructor { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Section> Sections { get; set; }
        = new List<Section>();
        public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
        public ICollection<Exam> Exams { get; set; }
        = new List<Exam>();
    }
}
