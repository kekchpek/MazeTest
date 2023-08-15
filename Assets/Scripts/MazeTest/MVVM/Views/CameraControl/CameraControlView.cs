using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;

namespace MazeTest.MVVM.Views.CameraControl
{
    public class CameraControlView : ViewBehaviour<ICameraControlViewModel>
    {
        [SerializeField] private Button _cameraButton;

        private void Awake()
        {
            _cameraButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ViewModel?.OnClick();
        }
    }
}