using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityMVVM;

namespace MazeTest.MVVM.Views.WinPopup
{
    public class WinPopupView : ViewBehaviour<IWinPopupViewModel>
    {
        
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _playAgainButton.onClick.AddListener(OnTryAgainClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnTryAgainClicked()
        {
            ViewModel?.OnPlayAgainClick();
        }

        private void OnExitClicked()
        {
            ViewModel?.OnExitClick();
        }
    }
}