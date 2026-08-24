using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Domain.Entities
{
    public class Exam
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public ICollection<Question> Questions { get; set; }
            = new List<Question>();
    }
}
