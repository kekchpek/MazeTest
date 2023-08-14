using MazeTest.MVVM.Models.MazeCore.Domain;
using UnityEngine;

namespace MazeTest.MVVM.Models.MazeCore
{
    public class MazeGenerator : IMazeGenerator
    {
        private const float WallProbability = 0.15f;
        
        public IMaze GenerateRandom(int width, int height)
        {
            var cells = new MazeCell[width * height];
            for (var j = 0; j < height; j++)
            {
                var groupStart = 0;
                for (var i = 0; i < width; i++)
                {
                    if ((Random.value < WallProbability && j != 0) || i == width - 1)
                    {
                        var groupEnd = i;
                        var enterIndex = Random.Range(groupStart, groupEnd + 1);
                        for (var k = groupStart; k <= groupEnd; k++)
                        {
                            cells[k + j * width] = new MazeCell(
                                true, 
                                k != enterIndex || j == 0,
                                k == groupStart,
                                k == groupEnd);
                            if (k == enterIndex && j > 0)
                            {
                                cells[k + (j - 1) * width].UpperWall = false;
                            }
                        }

                        groupStart = i + 1;
                    }
                }
            }
            return new Maze(cells, width, height);
        }
    }
}