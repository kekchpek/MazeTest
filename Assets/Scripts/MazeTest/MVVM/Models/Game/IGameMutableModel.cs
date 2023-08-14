namespace MazeTest.MVVM.Models.Game
{
    public interface IGameMutableModel : IGameModel
    {

        void SetPlaying(bool isPlaying);

    }
}