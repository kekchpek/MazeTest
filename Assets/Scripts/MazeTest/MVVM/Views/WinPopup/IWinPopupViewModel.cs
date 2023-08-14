using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.WinPopup
{
    public interface IWinPopupViewModel : IViewModel
    {
        void OnPlayAgainClick();
        void OnExitClick();
    }
}