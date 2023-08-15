using System;
using System.Collections.Generic;
using System.Linq;
using AsyncReactAwait.Bindable;
using AsyncReactAwait.Promises;
using MazeTest.Core;
using MazeTest.MVVM.Models.CameraService;
using MazeTest.MVVM.Models.MazeCore;
using MazeTest.MVVM.Views.Enemy.Payload;
using MazeTest.MVVM.Views.MazeView;
using MazeTest.MVVM.Views.MazeView.Payload;
using MazeTest.MVVM.Views.Player;
using MazeTest.MVVM.Views.Player.Payload;
using MazeTest.MVVM.Views.WinTrigger.Payload;
using UnityEngine;
using UnityMVVM.ViewManager;
using UnityMVVM.ViewModelCore;
using Random = UnityEngine.Random;

namespace MazeTest.MVVM.Models.Game
{
    public class GameService : IGameService
    {
        private const int MinEnemiesCount = 5;
        private const int MaxEnemiesCount = 20;
        
        private const float MazeRealWidth = 100f;
        private const float MazeRealHeight = 100f;
        
        private const int MazeWidth = 10;
        private const int MazeHeight = 10;

        private const float CellPatrolFactor = 0.25f;

        private static readonly SortedSet<(int x, int y)> ForbiddenCells = new()
        {
            (0,0),
            (MazeWidth - 1, MazeHeight - 1)
        };
        
        private readonly IViewManager _viewManager;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IGameMutableModel _model;
        private readonly ICameraService _cameraService;

        public GameService(
            IViewManager viewManager,
            IMazeGenerator mazeGenerator,
            IGameMutableModel model,
            ICameraService cameraService)
        {
            _viewManager = viewManager;
            _mazeGenerator = mazeGenerator;
            _model = model;
            _cameraService = cameraService;
        }
        
        public async IPromise StartLevel()
        {
            await _viewManager.Open(ViewLayers.GameLayer, ViewNames.Game);
            await _viewManager.Open(ViewLayers.ScreenLayer, ViewNames.GameOverlay);
            var gameView = _viewManager.GetView(ViewLayers.GameLayer)!;
            var maze = _viewManager.Create<IMazeViewModel>(gameView, ViewNames.Maze, 
                gameView.Layer.Container, 
                new MazePayload(
                    _mazeGenerator.GenerateRandom(MazeWidth, MazeHeight),
                    MazeRealWidth,
                    MazeRealHeight));
            var startPosition = maze.GetCellCenter(0, 0);
            var endPosition = maze.GetCellCenter(MazeWidth - 1, MazeHeight - 1);
            var player = _viewManager.Create<IPlayerViewModel>(gameView, ViewNames.Player,
                gameView.Layer.Container,
                new PlayerPayload(startPosition));
            _viewManager.Create(gameView, ViewNames.WinTrigger,
                gameView.Layer.Container,
                new WinTriggerPayload(endPosition));
            _cameraService.FetchCameraAnchors();
            CreateEnemies(gameView, maze, player.Position);
            _model.SetPlaying(true);
        }

        private void CreateEnemies(IViewModel gameView, IMazeViewModel maze,
            IBindable<Vector3> playerPos)
        {
            var enemiesCount = Random.Range(MinEnemiesCount, MaxEnemiesCount + 1);
            Debug.Log($"Enemies count = {enemiesCount}");
            var enemyCells = new List<(int x, int y)>(enemiesCount);
            for (var i = 0; i < enemiesCount; i++)
            {
                (int x, int y) cell = (Random.Range(0, MazeWidth), Random.Range(0, MazeHeight));
                int cellsCriticalCounter = 0;
                while (ForbiddenCells.Contains(cell) || enemyCells.Any(c => c.x == cell.x && c.y == cell.y))
                {
                    cellsCriticalCounter++;
                    if (cellsCriticalCounter == MazeWidth * MazeHeight - ForbiddenCells.Count)
                        throw new Exception($"Not enough maze rooms to create {enemiesCount} enemies.");
                    if (cell.x == MazeWidth - 1)
                    {
                        cell.y = (cell.y + 1) % MazeHeight;
                        cell.x = 0;
                    }
                    else
                    {
                        cell.x++;
                    }
                }
                enemyCells.Add(cell);
            }
            for (var i = 0; i < enemiesCount; i++)
            {
                var (x, y) = enemyCells[i];
                var patrolPoint1 =
                    maze.GetCellCenter(x, y) +
                    new Vector3(maze.CellWidth.Value * CellPatrolFactor, 0f, maze.CellHeight.Value * CellPatrolFactor);
                var patrolPoint2 =
                    maze.GetCellCenter(x, y) -
                    new Vector3(maze.CellWidth.Value * CellPatrolFactor, 0f, maze.CellHeight.Value * CellPatrolFactor);
                _viewManager.Create(gameView, ViewNames.Enemy, gameView.Layer.Container,
                    new EnemyPayload(
                        maze.GetCellCenter(x, y),
                        playerPos, 
                        new [] {patrolPoint1, patrolPoint2}));
            }
        }

        public async IPromise WinGame()
        {
            _model.SetPlaying(false);
            await _viewManager.Open(ViewLayers.PopupLayer, ViewNames.WinPopup);
        }

        public async IPromise LoseGame()
        {
            _model.SetPlaying(false);
            await _viewManager.Open(ViewLayers.PopupLayer, ViewNames.LosePopup);
        }

        public async IPromise QuitLevel()
        {
            _model.SetPlaying(false);
            await _viewManager.CloseAbove(ViewLayers.GameLayer);
            await _viewManager.Open(ViewLayers.ScreenLayer, ViewNames.MainMenu);
        }
    }
}