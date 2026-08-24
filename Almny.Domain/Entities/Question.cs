using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string OptionA { get; set; } = string.Empty;

        public string OptionB { get; set; } = string.Empty;

        public string OptionC { get; set; } = string.Empty;

        public string OptionD { get; set; } = string.Empty;

        public string CorrectAnswer { get; set; } = string.Empty;

        public Guid ExamId { get; set; }

        public Exam Exam { get; set; } = null!;
    }
}
