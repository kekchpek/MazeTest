using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.LosePopup
{
    public interface ILosePopupViewModel : IViewModel
    {
        void OnTryAgainClick();
        void OnExitClick();
    }
}