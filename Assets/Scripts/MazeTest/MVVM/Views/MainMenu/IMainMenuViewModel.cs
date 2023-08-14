using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.MainMenu
{
    public interface IMainMenuViewModel : IViewModel
    {
        void OnPlayClicked();
    }
}