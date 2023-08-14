using System;
using System.Collections.Generic;
using MazeTest.MVVM.Views.MazeView.Domain;
using UnityEngine;
using UnityMVVM;

namespace MazeTest.MVVM.Views.MazeView
{
    public class MazeView : ViewBehaviour<IMazeViewModel>
    {
        [SerializeField] private Transform _floor;
        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private Transform _wallsContainer;

        private GameObject[] _walls = Array.Empty<GameObject>();

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SmartBind(ViewModel!.FloorHeight, UpdateFloorSize);
            SmartBind(ViewModel!.FloorWidth, UpdateFloorSize);
            SmartBind(ViewModel!.Walls, UpdateWalls);
        }

        private void UpdateWalls(IReadOnlyList<WallData> wallsData)
        {
            foreach (var wall in _walls)
            {
                Destroy(wall);
            }
            _walls = new GameObject[wallsData.Count];
            for (var i = 0; i < wallsData.Count; i++)
            {
                var wallData = wallsData[i];
                _walls[i] = Instantiate(_wallPrefab, _wallsContainer);
                _walls[i].transform.localScale = wallData.Size;
                _walls[i].transform.position = wallData.Position;
            }
        }

        private void UpdateFloorSize()
        {
            _floor.localScale = new Vector3(
                ViewModel!.FloorWidth.Value,
                1f,
                ViewModel!.FloorHeight.Value);
        }
    }
}