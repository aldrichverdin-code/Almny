using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Domain.Entities
{
    public class ExamAttempt
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public User Student { get; set; } = null!;

        public Guid ExamId { get; set; }

        public Exam Exam { get; set; } = null!;

        public int Score { get; set; }

        public DateTime SubmittedAt { get; set; }
            = DateTime.UtcNow;
    }
}
