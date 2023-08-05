using BlazorSnake.DTOs;

namespace BlazorSnake.UI.Services
{
    public class SettingsService
    {
        private GameSettings? __settings;

        private readonly object __padlock = new object();

        public GameSettings GetSettings()
        {
            lock (__padlock)
            {
                __settings ??= new GameSettings();
            }
            return new GameSettings
            {
                GridSize = __settings.GridSize,
                StartingTimeBetweenIntervals = __settings.StartingTimeBetweenIntervals,
                GrowthRate = __settings.GrowthRate,
            };
        }

        public void SetSettings(GameSettings settings)
        {
            lock (__padlock)
            {
                __settings ??= new GameSettings();
            }
            __settings.GridSize = settings.GridSize;
            __settings.StartingTimeBetweenIntervals = settings.StartingTimeBetweenIntervals;
            __settings.GrowthRate = settings.GrowthRate;
        }
    }
}
