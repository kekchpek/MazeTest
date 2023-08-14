using AsyncReactAwait.Bindable;
using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.Enemy
{
    public interface IEnemyViewModel : IViewModel
    {
        Vector3 SpawnPosition { get; }
        IBindable<Vector3?> Destination { get; }
        void SetOrientation(Vector3 forward);
        void SetPosition(Vector3 position);
    }
}