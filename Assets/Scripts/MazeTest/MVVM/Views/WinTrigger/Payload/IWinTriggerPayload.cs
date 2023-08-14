using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.WinTrigger.Payload
{
    public interface IWinTriggerPayload : IPayload
    {
        Vector3 SpawnPosition { get; }
    }
}