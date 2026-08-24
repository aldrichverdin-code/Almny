using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Application.DTOs
{
    public class SubmitExamDto
    {
        public Guid ExamId { get; set; }

        public Dictionary<Guid, string> Answers
        { get; set; } = new();
    }
}
