using AsyncReactAwait.Bindable;
using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.Player
{
    public interface IPlayerViewModel : IViewModel
    {
        IBindable<Vector3> MoveDirection { get; }
        IBindable<Vector3> Position { get; }
        void SetPosition(Vector3 position);
        Vector3 SpawnPosition { get; }
    }
}