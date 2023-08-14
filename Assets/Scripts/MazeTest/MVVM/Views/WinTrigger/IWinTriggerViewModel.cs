using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.WinTrigger
{
    public interface IWinTriggerViewModel : IViewModel
    {
        Vector3 SpawnPosition { get; }
        void OnPlayerEnters();
    }
}