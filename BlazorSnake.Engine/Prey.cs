﻿namespace BlazorSnake.Engine
{
    public enum PreyDirection
    {
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,
    }

    public class Prey
    {
        private readonly Random __random;

        private readonly Game __game;

        public Prey(Game game)
        {
            __random = new Random();
            __game = game;
            __SetRandomPosition();
            __SetRandomDirection();
        }

        private void __SetRandomPosition()
        {
            var position = new Position
            {
                Left = __random.Next(0, __game.Grid.Size - 1),
                Top = __random.Next(0, __game.Grid.Size - 1)
            };
            Position = position;
        }

        public Position Position { get; private set; }

        private bool __AvoidBorderAndSnake()
        {
            var __nextPosition = __CalculateNextPosition();
            var __nextSnakeHead = __game.Snake.CalculateNextHead();
            return __game.Grid.IsPositionOutside(__nextPosition)
                || __nextPosition.Left == __nextSnakeHead.Left && __nextPosition.Top == __nextSnakeHead.Top
                || __game.Snake.Positions.Any(p => __nextPosition.Left == p.Left && __nextPosition.Top == p.Top);
        }

        public void Eaten()
        {
            bool tryCreateFoodAgain = true;
            while (tryCreateFoodAgain)
            {
                __SetRandomPosition();
                tryCreateFoodAgain = __game.Grid.IsPositionOutside(Position)
                    || __game.Snake.Positions.Any(p => p.Left == Position.Left && p.Top == Position.Top);
            }
        }

        private PreyDirection __direction;
        private void __SetRandomDirection()
        {
            var directions = Enum.GetValues(typeof(PreyDirection));
            __direction = (PreyDirection)directions.GetValue(__random.Next(directions.Length));
        }

        private Position __CalculateNextPosition()
        {
            switch (__direction)
            {
                case PreyDirection.Up:
                    return new Position
                    {
                        Left = Position.Left,
                        Top = Position.Top - 1,
                    };
                case PreyDirection.UpRight:
                    return new Position
                    {
                        Left = Position.Left + 1,
                        Top = Position.Top - 1,
                    };
                case PreyDirection.Right:
                    return new Position
                    {
                        Left = Position.Left + 1,
                        Top = Position.Top,
                    };
                case PreyDirection.DownRight:
                    return new Position
                    {
                        Left = Position.Left + 1,
                        Top = Position.Top + 1,
                    };
                case PreyDirection.Down:
                    return new Position
                    {
                        Left = Position.Left,
                        Top = Position.Top + 1,
                    };
                case PreyDirection.DownLeft:
                    return new Position
                    {
                        Left = Position.Left - 1,
                        Top = Position.Top + 1,
                    };
                case PreyDirection.Left:
                    return new Position
                    {
                        Left = Position.Left - 1,
                        Top = Position.Top,
                    };
                default: // SnakeDirection.UpLeft:
                    return new Position
                    {
                        Left = Position.Left - 1,
                        Top = Position.Top - 1,
                    };
            }
        }

        // Movement code

        private const int __moveDelay = 2;
        private int __moveCountDown = 0;
        public void Move()
        {
            if (__moveCountDown > 0)
            {
                __moveCountDown--;
                return;
            }

            if (__random.Next(100) < 20) __SetRandomDirection();
            bool changeDirection;

            do
            {
                changeDirection = __AvoidBorderAndSnake();
                if (changeDirection) __SetRandomDirection();
            }
            while (changeDirection);

            Position = __CalculateNextPosition();
            __moveCountDown = __moveDelay;
        }
    }
}
