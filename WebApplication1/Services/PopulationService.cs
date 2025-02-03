using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Services
{
    public interface IPopulationService
    {
        Task<string> GetPopulationDataAsync();
    }

    public class PopulationService : IPopulationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PopulationService> _logger;

        public PopulationService(HttpClient httpClient, ILogger<PopulationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetPopulationDataAsync()
        {
            var url = "https://webapi.bps.go.id/v1/api/list/model/data/lang/ind/domain/0000/var/1975/key/ae4382e50dd3853375f7cd3aff0156e7";
            _logger.LogInformation($"Fetching population data from: {url}");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
