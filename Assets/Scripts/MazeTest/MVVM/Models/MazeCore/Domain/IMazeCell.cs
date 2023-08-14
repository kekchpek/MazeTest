namespace MazeTest.MVVM.Models.MazeCore.Domain
{
    public interface IMazeCell
    {
        bool UpperWall { get; }
        bool BottomWall { get; }
        bool LeftWall { get; }
        bool RightWall { get; }
    }
}