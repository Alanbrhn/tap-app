using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPopulationService _populationService;

        public HomeController(IPopulationService populationService)
        {
            _populationService = populationService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _populationService.GetPopulationDataAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var populationData = JsonSerializer.Deserialize<PopulationData>(data, options);

            if (populationData == null || populationData.Tahun == null || populationData.DataContent == null)
            {
                return View("Error");
            }

            return View(populationData);
        }

    }
}
