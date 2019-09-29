using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGreetingService _greetingService;
        private readonly IConfiguration _configuration;

        public IndexModel(IGreetingService greetingService, IConfiguration configuration)
        {
            _greetingService = greetingService;
            _configuration = configuration;
        }

        public string Greeting { get; private set; }
        public bool ShowGreeting => !string.IsNullOrEmpty(Greeting);
        public string ForecastSectionTitle { get; private set; }
        public string WeatherDescription { get; private set; }
        public bool ShowWeatherForecast { get; private set; }

        public async Task OnGet()
        {

            if (_configuration.GetValue<bool>("Features:HomePage:EnableGreeting"))
            {
                Greeting = _greetingService.GetRandomGreeting();
            }
        }
    }
}
