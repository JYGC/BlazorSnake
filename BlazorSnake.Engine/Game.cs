using BlazorSnake.DTOs;

namespace BlazorSnake.Engine
{
    public class Game
    {
        public int TimeBetweenInterval { get; private set; }
        public System.Timers.Timer Timer { get; set; }

        public Grid Grid { get; private set; }
        public Snake Snake { get; private set; }
        public Prey Prey { get; private set; }

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; } = true;

        public Game(GameSettings settings)
        {
            Grid = new Grid(settings.GridSize);
            Snake = new Snake(this);
            Prey = new Prey(this);

            TimeBetweenInterval = settings.StartingTimeBetweenIntervals;
        }

        public void IncreaseSpeed()
        {
            if (TimeBetweenInterval > 1)
            {
                TimeBetweenInterval = TimeBetweenInterval * 14 / 15;
                Timer.Interval = TimeBetweenInterval;
            }
        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void Start()
        {
            Timer = new System.Timers.Timer(TimeBetweenInterval);
            Timer.Elapsed += __GoToNextInterval;
            Timer.Enabled = true;
        }

        public Action StateUpdateAction { get; set; }

        public Action GameOverAction { get; set; }

        private void __GoToNextInterval(object? source, System.Timers.ElapsedEventArgs e)
        {
            Prey.Move();
            Snake.Move();
            if (!Snake.IsAlive)
            {
                Timer.Enabled = false;
                GameOverAction();
            }
            StateUpdateAction();
        }

        public void End()
        {
            Timer.Stop();
        }
    }
}
