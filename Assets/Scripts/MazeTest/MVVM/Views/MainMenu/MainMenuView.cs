using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;

namespace MazeTest.MVVM.Views.MainMenu
{
    public class MainMenuView : ViewBehaviour<IMainMenuViewModel>
    {
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            ViewModel?.OnPlayClicked();
        }
    }
}