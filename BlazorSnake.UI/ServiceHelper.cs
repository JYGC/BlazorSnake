using Blazored.Modal;
using BlazorSnake.UI.App.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSnake.UI
{
    public class ServiceHelper
    {
        public static void AddService(IServiceCollection builderServices)
        {
            builderServices.AddSingleton<WeatherForecastService>();
            builderServices.AddBlazoredModal();
        }
    }
}
