namespace BlazorSnake.Engine
{
    enum CellType
    {
        Empty,
        Food,
        Snake,
        Border
    }

    public class Position
    {
        public int Left { get; set; }
        public int Top { get; set; }
    }

    public class Cell : Position
    {

    }
}