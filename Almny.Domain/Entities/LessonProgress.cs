using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Domain.Entities
{
    public class LessonProgress
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public User Student { get; set; } = null!;

        public Guid LessonId { get; set; }

        public Lesson Lesson { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
