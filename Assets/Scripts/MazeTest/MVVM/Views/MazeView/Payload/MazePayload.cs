using MazeTest.MVVM.Models.MazeCore.Domain;

namespace MazeTest.MVVM.Views.MazeView.Payload
{
    public class MazePayload : IMazePayload
    {
        public float Width { get; }
        public float Height { get; }
        public IMaze Maze { get; }
        public MazePayload(IMaze maze, float width, float height)
        {
            Maze = maze;
            Width = width;
            Height = height;
        }
    }
}