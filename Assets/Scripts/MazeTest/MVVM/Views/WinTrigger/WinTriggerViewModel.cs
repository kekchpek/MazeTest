using MazeTest.MVVM.Models.Game;
using MazeTest.MVVM.Views.WinTrigger.Payload;
using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.WinTrigger
{
    public class WinTriggerViewModel : ViewModel, IWinTriggerViewModel
    {
        private readonly IWinTriggerPayload _payload;
        private readonly IGameService _gameService;
        public Vector3 SpawnPosition => _payload.SpawnPosition;

        public WinTriggerViewModel(
            IWinTriggerPayload payload,
            IGameService gameService)
        {
            _payload = payload;
            _gameService = gameService;
        }
        
        public async void OnPlayerEnters()
        {
            await _gameService.WinGame();
        }
    }
}