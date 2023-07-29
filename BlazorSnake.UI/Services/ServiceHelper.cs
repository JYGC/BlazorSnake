using Blazored.Modal;
using BlazorSnake.UI.App.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSnake.UI.Services
{
    public class ServiceHelper
    {
        public static void ConfigureServices(IServiceCollection builderServices)
        {
            builderServices.AddSingleton<WeatherForecastService>();
            builderServices.AddSingleton<SettingsService>();
            builderServices.AddBlazoredModal();
        }
    }
}
