using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almny.Application.Interfaces;

public interface IAIQuizService
{
    Task<List<string>> GenerateQuestions(
        string content);
}
