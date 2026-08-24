using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Application.DTOs
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int Order { get; set; }

        public bool IsFreePreview { get; set; }

        public Guid SectionId { get; set; }
    }
}
