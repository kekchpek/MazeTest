using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace MazeTest.MVVM.Models.MazeCore.Domain
{
    public class Maze : IMaze
    {
        private readonly IReadOnlyList<IMazeCell> _cells;
        public int Width { get; }
        public int Height { get; }
        public IMazeCell this[int x, int y] => _cells[y * Width + x];

        public Maze(IEnumerable<IMazeCell> cells, int width, int height)
        {
            _cells = cells.ToArray();
            Assert.AreEqual(_cells.Count, width * height);
            Width = width;
            Height = height;
        }
        
    }
}