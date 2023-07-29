namespace BlazorSnake.Engine
{
    public class Grid
    {
        public int Size { get; private set; }

        public Grid(int size)
        {
            Size = size;
        }

        public bool IsPositionOutside(Position position)
        {
            return position.Left < 0 
                || position.Left > Size - 1
                || position.Top < 0 
                || position.Top > Size - 1;
        }
    }
}
