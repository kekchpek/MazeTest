using AsyncReactAwait.Bindable;
using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.Enemy.Payload
{
    public interface IEnemyPayload : IPayload
    {
        Vector3[] PatrolPath { get; }
        Vector3 SpawnPosition { get; }
        IBindable<Vector3> FetchedPlayerPosition { get; }
    }
}