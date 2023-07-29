namespace BlazorSnake.Engine
{
    public enum SnakeDirection
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Snake
    {
        private readonly int __startingLength = 3;

        private readonly Game __game;

        public Snake(Game game)
        {
            __game = game;
            __Spawn();
        }

        public List<Position> Positions { get; private set; }

        private void __Spawn()
        {
            Positions = new();
            for (int i = 0; i < __startingLength; i++)
            {
                Positions.Add(new Position
                {
                    Left = __game.Grid.Size / 2 - __startingLength / 2 + i,
                    Top = __game.Grid.Size / 2,
                });
            }
        }

        #region State
        public bool IsAlive { get; private set; } = true;
        private void __Die()
        {
            IsAlive = false;
        }

        private Position __Head
        {
            get
            {
                return Positions[0];
            }
        }
        #endregion

        #region Growth
        private const int __growthRate = 1;
        private int __growth = 0;

        private void __Grow()
        {
            __growth--;
        }

        private void __Eat()
        {
            __game.Prey.Eaten();
            __growth = __growthRate;
        }
        #endregion

        #region Movement
        private bool __blockChangingDirection = false;
        public void Move()
        {
            Position newHead = CalculateNextHead();
            Positions.Insert(0, newHead);

            if (__HasHitWall() || __HasHitSelf())
            {
                __Die();
                return;
            }
            
            if (__growth > 0) __Grow();
            else Positions.Remove(Positions[Positions.Count() - 1]);
            
            __blockChangingDirection = false;

            if (__HasCaughtPrey())
            {
                __Eat();
                __game.IncreaseScore();
                __game.IncreaseSpeed();
            }
        }

        private bool __HasCaughtPrey()
        {
            return __Head.Left == __game.Prey.Position.Left && __Head.Top == __game.Prey.Position.Top;
        }

        private bool __HasHitWall()
        {
            return __game.Grid.IsPositionOutside(Positions[0]);
        }

        private bool __HasHitSelf()
        {
            return Positions.Skip(1).Any(p => p.Left == __Head.Left && p.Top == __Head.Top);
        }

        private SnakeDirection __direction = SnakeDirection.Left;

        public void GoUp()
        {
            if (__blockChangingDirection || __direction == SnakeDirection.Down) return;
            __direction = SnakeDirection.Up;
            __blockChangingDirection = true;
        }

        public void GoDown()
        {
            if (__blockChangingDirection || __direction == SnakeDirection.Up) return;
            __direction = SnakeDirection.Down;
            __blockChangingDirection = true;
        }

        public void GoLeft()
        {
            if (__blockChangingDirection || __direction == SnakeDirection.Right) return;
            __direction = SnakeDirection.Left;
            __blockChangingDirection = true;
        }

        public void GoRight()
        {
            if (__blockChangingDirection || __direction == SnakeDirection.Left) return;
            __direction = SnakeDirection.Right;
            __blockChangingDirection = true;
        }

        public Position CalculateNextHead()
        {
            switch (__direction)
            {
                case SnakeDirection.Up:
                    return new Position
                    {
                        Left = Positions[0].Left,
                        Top = Positions[0].Top - 1,
                    };
                case SnakeDirection.Right:
                    return new Position
                    {
                        Left = Positions[0].Left + 1,
                        Top = Positions[0].Top,
                    };
                case SnakeDirection.Down:
                    return new Position
                    {
                        Left = Positions[0].Left,
                        Top = Positions[0].Top + 1,
                    };
                default: // SnakeDirection.Left:
                    return new Position
                    {
                        Left = Positions[0].Left - 1,
                        Top = Positions[0].Top,
                    };
            }
        }
        #endregion
    }
}
