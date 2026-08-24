using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Application.DTOs
{
    public class CreateExamDto
    {
        public string Title { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public Guid CourseId { get; set; }
    }
}
