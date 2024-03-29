﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.External;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class WeatherServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherForecasting(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("Features:WeatherForecasting:EnableWeatherForecast"))
            {
                services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();
                services.TryAddSingleton<IWeatherForecaster, WeatherForecaster>();
                services.Decorate<IWeatherForecaster, CachedWeatherForecaster>();
            }
            else
            {
                services.TryAddSingleton<IWeatherForecaster, DisabledWeatherForecaster>();
            }


            return services;
        }
    }
}
