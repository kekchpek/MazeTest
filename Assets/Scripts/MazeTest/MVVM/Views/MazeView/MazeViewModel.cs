using System.Collections.Generic;
using System.Linq;
using AsyncReactAwait.Bindable;
using MazeTest.MVVM.Models.NavigationService;
using MazeTest.MVVM.Views.MazeView.Domain;
using MazeTest.MVVM.Views.MazeView.Payload;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.MVVM.Views.MazeView
{
    public class MazeViewModel : ViewModel, IMazeViewModel, IInitializable
    {
        
        private const float WallHeight = 10f;
        private const float WallThickness = 1f;
        private const float HoleRelativeSize = 0.5f;
        
        private readonly IMazePayload _payload;
        private readonly INavigationService _navigationService;

        private readonly IMutable<IReadOnlyList<WallData>> _walls = new Mutable<IReadOnlyList<WallData>>();
        private readonly IMutable<float> _width = new Mutable<float>();
        private readonly IMutable<float> _height = new Mutable<float>();

        private readonly IMutable<float> _cellWidth = new Mutable<float>();
        private readonly IMutable<float> _cellHeight = new Mutable<float>();
        private readonly IMutable<Matrix4x4> _mazeOverviewCameraTransform = new Mutable<Matrix4x4>();

        public IBindable<IReadOnlyList<WallData>> Walls => _walls;
        public IBindable<Matrix4x4> MazeOverviewCameraTransform => _mazeOverviewCameraTransform;
        public IBindable<float> FloorWidth => _width;
        public IBindable<float> FloorHeight => _height;
        public IBindable<float> CellWidth => _cellWidth;
        public IBindable<float> CellHeight => _cellHeight;

        public MazeViewModel(
            IMazePayload payload,
            INavigationService navigationService)
        {
            _payload = payload;
            _navigationService = navigationService;
        }

        public void Initialize()
        {
            _width.Value = _payload.Width;
            _height.Value = _payload.Height;
            var maze = _payload.Maze;
            _cellWidth.Value = _payload.Width / maze.Width;
            _cellHeight.Value = _payload.Height / maze.Height;
            _mazeOverviewCameraTransform.Value = Matrix4x4.TRS(
                new Vector3(_width.Value / 2f, Mathf.Max(_width.Value / 2f, _height.Value), _height.Value / 2f),
                Quaternion.LookRotation(Vector3.down),
                Vector3.one
            );
            var walls = new List<WallData>(maze.Width * maze.Height * 4);
            for (var i = 0; i < maze.Width; i++)
            for (var j = 0; j < maze.Height; j++)
            {
                var cell = maze[i, j];
                walls.AddRange(cell.UpperWall ? 
                    new [] {GenerateWall(Vector3.up, i, j)} : 
                    GenerateHole(Vector3.up, i, j));
                
                walls.AddRange(cell.BottomWall ? 
                    new [] {GenerateWall(Vector3.down, i, j)} : 
                    GenerateHole(Vector3.down, i, j));
                
                walls.AddRange(cell.LeftWall ? 
                    new [] {GenerateWall(Vector3.left, i, j)} : 
                    GenerateHole(Vector3.left, i, j));
                
                walls.AddRange(cell.RightWall ? 
                    new [] {GenerateWall(Vector3.right, i, j)} : 
                    GenerateHole(Vector3.right, i, j));
            }
            _walls.Set(walls);
            
            var mazeSize = new Vector3(_width.Value, 0, _height.Value);
            _navigationService.BakeNavigationData(
                new Bounds(mazeSize / 2f, mazeSize + new Vector3(0f, 10f, 0f)),
                walls.Select(x => new Bounds(x.Position, x.Size)).ToArray());
            Debug.Log("Maze initialized.");
        }

        private WallData GenerateWall(Vector3 orientation, int x, int y)
        {
            return new WallData(
                new Vector3(
                    (x + 0.5f * Mathf.Abs(orientation.y) + Mathf.Max(0, orientation.x)) * _cellWidth.Value, 
                    0f, 
                    (y + 0.5f * Mathf.Abs(orientation.x) + Mathf.Max(0, orientation.y)) * _cellHeight.Value),
                new Vector3(
                    _cellWidth.Value * Mathf.Abs(orientation.y) + WallThickness * Mathf.Abs(orientation.x), 
                    WallHeight,
                    WallThickness * Mathf.Abs(orientation.y) + _cellHeight.Value * Mathf.Abs(orientation.x)));
        }
        

        private WallData[] GenerateHole(Vector3 orientation, int x, int y)
        {
            var wall1 = new WallData(
                new Vector3(
                    (x + (0.5f - HoleRelativeSize / 2f - (1 - HoleRelativeSize) / 4f) * Mathf.Abs(orientation.y) + Mathf.Max(0, orientation.x)) * _cellWidth.Value, 
                    0f, 
                    (y + (0.5f - HoleRelativeSize / 2f - (1 - HoleRelativeSize) / 4f) * Mathf.Abs(orientation.x) + Mathf.Max(0, orientation.y)) * _cellHeight.Value),
                new Vector3(
                    _cellWidth.Value * Mathf.Abs(orientation.y) * (1f - HoleRelativeSize) / 2f + WallThickness * Mathf.Abs(orientation.x), 
                    WallHeight,
                    WallThickness * Mathf.Abs(orientation.y) + _cellHeight.Value * Mathf.Abs(orientation.x) * (1f - HoleRelativeSize) / 2f));
            var wall2 = new WallData(
                new Vector3(
                    (x + (0.5f + HoleRelativeSize / 2f + (1 - HoleRelativeSize) / 4f) * Mathf.Abs(orientation.y) + Mathf.Max(0, orientation.x)) * _cellWidth.Value, 
                    0f, 
                    (y + (0.5f + HoleRelativeSize / 2f + (1 - HoleRelativeSize) / 4f) * Mathf.Abs(orientation.x) + Mathf.Max(0, orientation.y)) * _cellHeight.Value),
                new Vector3(
                    _cellWidth.Value * Mathf.Abs(orientation.y) * (1f - HoleRelativeSize) / 2f + WallThickness * Mathf.Abs(orientation.x), 
                    WallHeight,
                    WallThickness * Mathf.Abs(orientation.y) + _cellHeight.Value * Mathf.Abs(orientation.x) * (1f - HoleRelativeSize) / 2f));
            return new[] { wall1, wall2 };
        }
        
        public Vector3 GetCellCenter(int x, int y)
        {
            return new Vector3(
                _cellWidth.Value * (x + 0.5f), 
                WallHeight / 2f, 
                _cellHeight.Value * (y + 0.5f));
        }
    }
}