namespace BlazorSnake.DTOs
{
    public class GameSettings
    {
        public int GridSize { get; set; } = 34;
        public int StartingTimeBetweenIntervals { get; set; } = 700;
        public int GrowthRate { get; set; } = 1;
    }
}