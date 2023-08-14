using AsyncReactAwait.Promises;

namespace MazeTest.MVVM.Models.Game
{
    public interface IGameService
    {
        IPromise StartLevel();
        IPromise WinGame();
        IPromise LoseGame();
        IPromise QuitLevel();
    }
}