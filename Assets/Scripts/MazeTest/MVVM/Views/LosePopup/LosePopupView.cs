using System;
using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;

namespace MazeTest.MVVM.Views.LosePopup
{
    public class LosePopupView : ViewBehaviour<ILosePopupViewModel>
    {

        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _tryAgainButton.onClick.AddListener(OnTryAgainClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnTryAgainClicked()
        {
            ViewModel?.OnTryAgainClick();
        }

        private void OnExitClicked()
        {
            ViewModel?.OnExitClick();
        }
    }
}