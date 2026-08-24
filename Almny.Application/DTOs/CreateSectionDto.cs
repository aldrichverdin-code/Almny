using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Application.DTOs
{
    public class CreateSectionDto
    {
        public string Title { get; set; } = string.Empty;

        public int Order { get; set; }

        public Guid CourseId { get; set; }
    }
}
