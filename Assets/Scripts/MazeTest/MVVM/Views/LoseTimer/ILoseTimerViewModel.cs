using AsyncReactAwait.Bindable;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.LoseTimer
{
    public interface ILoseTimerViewModel : IViewModel
    {
        IBindable<bool> Shown { get; }
        IBindable<float> TimeInSeconds { get; }
    }
}