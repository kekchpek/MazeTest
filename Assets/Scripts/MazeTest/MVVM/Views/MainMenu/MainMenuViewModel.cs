using MazeTest.MVVM.Models.Game;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.MainMenu
{
    public class MainMenuViewModel : ViewModel, IMainMenuViewModel
    {
        private readonly IGameService _gameService;

        public MainMenuViewModel(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        public async void OnPlayClicked()
        {
            await _gameService.StartLevel();
        }
    }
}