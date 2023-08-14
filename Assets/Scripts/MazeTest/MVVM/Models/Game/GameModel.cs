using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.Game
{
    public class GameModel : IGameMutableModel
    {
        private readonly IMutable<bool> _isPlaying = new Mutable<bool>();
        public IBindable<bool> IsPlaying => _isPlaying;

        public void SetPlaying(bool isPlaying)
        {
            _isPlaying.Set(isPlaying);
        }
    }
}