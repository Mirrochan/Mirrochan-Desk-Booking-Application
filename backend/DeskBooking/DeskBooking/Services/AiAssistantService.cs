using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using DeskBookingAPI.DTOs;

public class AiAssistantService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _http;

    public AiAssistantService(IConfiguration config, HttpClient http)
    {
        _config = config;
        _http = http;
    }

    public async Task<string> AskQuestionAsync(string question, string bookings)
    {
        var prompt = BuildPrompt(question, bookings);
      
        var body = new
        {
            model = _config["OpenAI:Model"],
            messages = new[] { new { role = "user", content = prompt } },
            temperature = 0.7
        };

        var request = new HttpRequestMessage(HttpMethod.Post, _config["OpenAI:ApiUrl"]);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config["OpenAI:ApiKey"]);
        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Groq API error {response.StatusCode}: {error}");
        }

        using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return content ?? "No response from AI.";
    }

    private string BuildPrompt(string question,string bookings)
    {    

        return $"""
        You are a booking assistant. Answer questions based on the following information::
        {bookings}

        Question: {question}
        You must support:
        - Counting all bookings
        - Listing upcoming bookings
        - Showing bookings for a specific day
        - Listing bookings for the previous week
        - Filtering by workspace type

        Rules:
        - Be concise
        - Only use provided booking data
        - Format dates as MM/DD/YYYY
        - If unsure, say "Sorry, I didn`t understand that. Please try rephrasing your question."

        Answer like this:
        📅 May 18, 2025 — Private room for 2 people at WorkClub Pechersk (10:00 - 12:00)
        📅 May 20, 2025 — Private room for 2 people at UrbanSpace Podil (09:00 - 17:00)
        """;
    }

    private string FormatDate(DateTime date)
    {
        return date.ToString("MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
    }
}
