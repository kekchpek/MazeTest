using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.Game
{
    public interface IGameModel
    {
        IBindable<bool> IsPlaying { get; }
    }
}