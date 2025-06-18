using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly AiAssistantService _aiService;

    public AiController(AiAssistantService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] AiRequestDto dto)
    {
       
        if (string.IsNullOrWhiteSpace(dto.Question))
            return BadRequest("Question is required");

        var answer = await _aiService.AskQuestionAsync(dto.Question, dto.Bookings);
        return Ok(new { answer });
    }
}
