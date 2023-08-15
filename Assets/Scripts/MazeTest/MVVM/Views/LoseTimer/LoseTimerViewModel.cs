using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;
using MazeTest.MVVM.Models.Lose;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.LoseTimer
{
    public class LoseTimerViewModel : ViewModel, ILoseTimerViewModel
    {
        private readonly ILoseModel _loseModel;

        public IBindable<bool> Shown => _loseModel.LoseLeftTime.ConvertTo(x => x.HasValue);
        public IBindable<float> TimeInSeconds => _loseModel.LoseLeftTime.ConvertTo(x => x ?? 0f);

        public LoseTimerViewModel(
            ILoseModel loseModel)
        {
            _loseModel = loseModel;
        }
    }
}