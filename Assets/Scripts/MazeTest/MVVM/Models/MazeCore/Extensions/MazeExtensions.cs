using MazeTest.MVVM.Models.MazeCore.Domain;

namespace MazeTest.MVVM.Models.MazeCore.Extensions
{
    public static class MazeExtensions
    {
        public static IMazeCell GetCell(this IMaze maze, int x, int y)
        {
            return maze[x, y];
        }
    }
}