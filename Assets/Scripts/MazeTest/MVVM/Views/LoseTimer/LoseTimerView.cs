using TMPro;
using UnityEngine;
using UnityMVVM;

namespace MazeTest.MVVM.Views.LoseTimer
{
    public class LoseTimerView : ViewBehaviour<ILoseTimerViewModel>
    {

        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private GameObject _layout;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SmartBind(ViewModel!.Shown, _layout.gameObject.SetActive);
            SmartBind(ViewModel!.TimeInSeconds, x=> _timeText.text = x.ToString("0.00"));
        }
    }
}