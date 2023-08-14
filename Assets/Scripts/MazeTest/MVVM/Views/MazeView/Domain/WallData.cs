
using UnityEngine;

namespace MazeTest.MVVM.Views.MazeView.Domain
{
    public readonly struct WallData
    {
        public Vector3 Position { get; }
        public Vector3 Size { get; }
        public WallData(Vector3 position, Vector3 size)
        {
            Position = position;
            Size = size;
        }
    }
}