using Almny.Application.Interfaces;

using Microsoft.Extensions.Configuration;

using OpenAI;
using OpenAI.Chat;

namespace Almny.Infrastructure.Services;

public class AIQuizService : IAIQuizService
{
    private readonly IConfiguration _configuration;

    public AIQuizService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<string>> GenerateQuestions(
    string content)
    {
        try
        {
            var apiKey = _configuration["OpenAI:ApiKey"];

            var client = new OpenAIClient(apiKey);

            var prompt = $"""
            Generate 5 MCQ quiz questions from this lesson:

            {content}

            Return only the questions.
            """;

            var chatClient = client.GetChatClient("gpt-4o-mini");

            var response =
                await chatClient.CompleteChatAsync(prompt);

            var text = response.Value.Content[0].Text;

            return text.Split("\n")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }
        catch (Exception ex)
        {
            return new List<string>
        {
            $"AI Error: {ex.Message}"
        };
        }
    }
}