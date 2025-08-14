using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

public class LocationIQService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public LocationIQService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["LocationIQ:ApiKey"] ?? throw new Exception("API Key no configurada");
    }

    // Autocomplete
    public async Task<string> AutocompleteAsync(string query)
    {
        var url = $"https://us1.locationiq.com/v1/autocomplete?key={_apiKey}&q={Uri.EscapeDataString(query)}&limit=5";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Nearby
    public async Task<string> NearbyAsync(double lat, double lon, string? query = null, int radius = 1000)
    {
        var defaultQuery = "veterinary";
        var searchQuery = string.IsNullOrWhiteSpace(query) ? defaultQuery : query;
        var url = $"https://us1.locationiq.com/v1/search.php" +
                  $"?key={_apiKey}" +
                  $"&q={Uri.EscapeDataString(searchQuery)}" +
                  $"&format=json" +
                  $"&limit=50" +
                  $"&dedupe=1" +
                  $"&radius={radius}"; 
        
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error en LocationIQ Nearby: {response.StatusCode} - {errorBody}");
        }

        return await response.Content.ReadAsStringAsync();
    }

}