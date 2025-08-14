using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NearbyController : ControllerBase
{
    private readonly LocationIQService _locationService;

    public NearbyController(LocationIQService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] double lat,
        [FromQuery] double lon,
        [FromQuery] string? search = null,
        [FromQuery] int radius = 1000)
    {
        var result = await _locationService.NearbyAsync(lat, lon, search, radius);
        return Content(result, "application/json");
    }
}