namespace MazeTest.MVVM.Models.Lose
{
    public interface ILoseMutableModel : ILoseModel
    {
        void SetLoseLeftTime(float? loseTime);
    }
}