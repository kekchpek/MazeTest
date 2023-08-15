using MazeTest.MVVM.Models.Game;
using MazeTest.MVVM.Models.Lose;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.MVVM.Views.Game
{
    public class GameViewModel : ViewModel, IGameViewModel, IInitializable
    {
        private readonly ILoseService _loseService;
        private readonly IGameService _gameService;

        public GameViewModel(
            ILoseService loseService,
            IGameService gameService)
        {
            _loseService = loseService;
            _gameService = gameService;
        }

        public void Initialize()
        {
            _loseService.Lost += OnLost;
        }

        private async void OnLost()
        {
            await _gameService.LoseGame();
        }

        protected override void OnDestroyInternal()
        {
            _loseService.Lost -= OnLost;
            base.OnDestroyInternal();
        }
    }
}