﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGreetingService _greetingService;
        private readonly HomePageConfiguration _homePageConfiguration;
        private readonly IWeatherForecaster _weatherForecaster;

        public IndexModel(IGreetingService greetingService, IOptionsSnapshot<HomePageConfiguration> options,
            IWeatherForecaster weatherForecaster)
        {
            _greetingService = greetingService;
            _homePageConfiguration = options.Value;
            _weatherForecaster = weatherForecaster;

            GreetingColor = _greetingService.GreetingColor ?? "black";
        }

        public string Greeting { get; private set; }
        public bool ShowGreeting => !string.IsNullOrEmpty(Greeting);
        public string GreetingColor { get; private set; }
        public string ForecastSectionTitle { get; private set; }
        public string WeatherDescription { get; private set; }
        public bool ShowWeatherForecast { get; private set; }

        public async Task OnGet()
        {

            if (_homePageConfiguration.EnableGreeting)
            {
                Greeting = _greetingService.GetRandomGreeting();
            }

            ShowWeatherForecast = _homePageConfiguration.EnableWeatherForecast
                && _weatherForecaster.ForecastEnabled;

            if (ShowWeatherForecast)
            {
                var title = _homePageConfiguration.ForecastSectionTitle;
                ForecastSectionTitle = string.IsNullOrEmpty(title) ? "How's the weather?" : title;

                var currentWeather = await _weatherForecaster.GetCurrentWeatherAsync();

                if (currentWeather != null)
                {
                    switch (currentWeather.Description)
                    {
                        case "Sun":
                            WeatherDescription = "It's sunny right now. A great day for tennis!";
                            break;

                        case "Cloud":
                            WeatherDescription = "It's cloudy at the moment and the outdoor courts are in use.";
                            break;

                        case "Rain":
                            WeatherDescription = "We;re sorry but its raining here. No outdoor courts in use,";
                            break;

                    }
                }

            }
        }

    }
}
