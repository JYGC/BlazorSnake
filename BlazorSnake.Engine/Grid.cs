namespace BlazorSnake.Engine
{
    public class Grid
    {
        public int Size { get; private set; }

        public Snake Snake { get; private set; }
        public Prey Prey { get; private set; }

        public Grid(int size, out Snake snake, out Prey prey)
        {
            Size = size;
            
            Snake = new Snake(this);
            Prey = new Prey(this);
            snake = Snake;
            prey = Prey;
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
