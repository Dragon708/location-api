using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AutocompleteController : ControllerBase
{
    private readonly LocationIQService _locationService;

    public AutocompleteController(LocationIQService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("El parámetro 'query' es requerido.");

        var result = await _locationService.AutocompleteAsync(query);
        return Content(result, "application/json");
    }
}