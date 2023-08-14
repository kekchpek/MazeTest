namespace MazeTest.MVVM.Models.MazeCore.Domain
{
    public interface IMaze
    {
        int Width { get; }
        int Height { get; }
        IMazeCell this[int x, int y] { get; }
    }
}