using MazeTest.MVVM.Models.Game;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.LosePopup
{
    public class LosePopupViewModel : ViewModel, ILosePopupViewModel
    {
        private readonly IGameService _gameService;

        public LosePopupViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }
        
        public async void OnTryAgainClick()
        {
            _gameService.StartLevel();
        }

        public async void OnExitClick()
        {
            _gameService.QuitLevel();
        }
    }
}