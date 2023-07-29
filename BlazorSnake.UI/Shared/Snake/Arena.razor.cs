using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions;
using BlazorSnake.Engine;
using Microsoft.AspNetCore.Components;
using BlazorSnake.UI.Services;

namespace BlazorSnake.UI.Shared.Snake
{
    partial class Arena
    {
        [Parameter]
        public Action ShowGameOverModal { get; set; }

        private const int __cellSize = 10;
        private const string __arenaBackgroundColor = "#434d5c";
        private const string __preyColor = "#d15f13";
        private const string __snakeColor = "#62DE2C";

        private Canvas2DContext? __context;
        private BECanvasComponent? __canvasReference;

        private Game __game;

        [Inject]
        SettingsService SettingsService { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            __game = new Game(SettingsService.GetSettings());
            __game.StateUpdateAction = () => { InvokeAsync(StateHasChanged); };
            __game.GameOverAction = () => { ShowGameOverModal?.Invoke(); };
            __game.Start();
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
            await __context.FillRectAsync(0, 0, __game.Grid.Size * __cellSize, __game.Grid.Size * __cellSize);

        }

        private async Task __DrawPrey()
        {
            if (__context == null) return;
            await __context.SetFillStyleAsync(__preyColor);
            await __context.FillRectAsync(__game.Prey.Position.Left * __cellSize, __game.Prey.Position.Top * __cellSize, __cellSize, __cellSize);
        }

        private async Task __DrawSnake()
        {
            if (__context == null) return;
            await __context.SetFillStyleAsync(__snakeColor);
            foreach (var position in __game.Snake.Positions)
                await __context.FillRectAsync(position.Left * __cellSize, position.Top * __cellSize, __cellSize, __cellSize);
        }

        private void __GoUp()
        {
            __game.Snake.GoUp();
        }

        private void __GoDown()
        {
            __game.Snake.GoDown();
        }

        private void __GoLeft()
        {
            __game.Snake.GoLeft();
        }

        private void __GoRight()
        {
            __game.Snake.GoRight();
        }
        private void __BackToMenu()
        {
            __game.End();
            NavManager.NavigateTo("/");
        }
    }
}
