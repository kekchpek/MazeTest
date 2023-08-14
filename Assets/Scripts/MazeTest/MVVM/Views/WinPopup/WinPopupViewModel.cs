using MazeTest.MVVM.Models.Game;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.WinPopup
{
    public class WinPopupViewModel : ViewModel, IWinPopupViewModel
    {
        private readonly IGameService _gameService;

        public WinPopupViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }
        
        public async void OnPlayAgainClick()
        {
            await _gameService.StartLevel();
        }

        public async void OnExitClick()
        {
            await _gameService.QuitLevel();
        }
    }
}