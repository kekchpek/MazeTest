namespace MazeTest.MVVM.Models.MazeCore.Domain
{
    public class MazeCell : IMazeCell
    {
        public bool UpperWall { get; set; }
        public bool BottomWall { get; set; }
        public bool LeftWall { get; set; }
        public bool RightWall { get; set; }

        public MazeCell(bool upperWall, bool bottomWall, bool leftWall, bool rightWall)
        {
            UpperWall = upperWall;
            BottomWall = bottomWall;
            LeftWall = leftWall;
            RightWall = rightWall;
        }
    }
}