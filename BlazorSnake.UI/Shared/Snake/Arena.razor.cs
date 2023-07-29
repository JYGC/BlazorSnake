using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions;
using BlazorSnake.Engine;

namespace BlazorSnake.UI.Shared.Snake
{
    partial class Arena
    {
        private const int __arenaSize = 34;
        private const int __cellSize = 10;
        private const string __arenaBackgroundColor = "#434d5c";
        private const string __preyColor = "#d15f13";
        private const string __snakeColor = "#62DE2C";

        private Canvas2DContext? __context;
        private BECanvasComponent? __canvasReference;

        private int __startingGameSpeed = 700;

        private System.Timers.Timer __timer;

        private Grid __grid;
        private Prey __prey;
        private Engine.Snake __snake;

        public Arena()
        {
            __grid = new Grid(__arenaSize, out __snake, out __prey);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            __timer = new System.Timers.Timer(__startingGameSpeed);
            __timer.Elapsed += __GoToNextInterval;
            __timer.Enabled = true;

            //var timer = new System.Threading.Timer((_) =>
            //{
            //    InvokeAsync(() =>
            //    {
            //        __prey.Move();
            //        __snake.Move();
            //        StateHasChanged();
            //    });
            //}, null, 0, __startingGameSpeed);
        }

        private void __GoToNextInterval(object? source, System.Timers.ElapsedEventArgs e)
        {
            __prey.Move();
            __snake.Move();
            __timer.Enabled = __snake.IsAlive;
            InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            __context = await __canvasReference.CreateCanvas2DAsync();
            await __DrawArena();
            await __DrawPrey();
            await __DrawSnake();
        }

        private async Task __DrawArena()
        {
            if (__context == null) return;
            await __context.SetFillStyleAsync(__arenaBackgroundColor);
            await __context.FillRectAsync(0, 0, __grid.Size * __cellSize, __grid.Size * __cellSize);

        }

        private async Task __DrawPrey()
        {
            if (__context == null) return;
            await __context.SetFillStyleAsync(__preyColor);
            await __context.FillRectAsync(__prey.Position.Left * __cellSize, __prey.Position.Top * __cellSize, __cellSize, __cellSize);
        }

        private async Task __DrawSnake()
        {
            if (__context == null) return;
            await __context.SetFillStyleAsync(__snakeColor);
            foreach (var position in __snake.Positions)
                await __context.FillRectAsync(position.Left * __cellSize, position.Top * __cellSize, __cellSize, __cellSize);
        }

        private void __GoUp()
        {
            __snake.GoUp();
        }

        private void __GoDown()
        {
            __snake.GoDown();
        }

        private void __GoLeft()
        {
            __snake.GoLeft();
        }

        private void __GoRight()
        {
            __snake.GoRight();
        }
    }
}
