using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.Player.Payload
{
    public interface IPlayerPayload : IPayload
    {
        Vector3 SpawnPosition { get; }
    }
}