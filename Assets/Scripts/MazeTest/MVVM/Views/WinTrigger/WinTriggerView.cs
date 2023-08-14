using MazeTest.Core;
using UnityEngine;
using UnityMVVM;

namespace MazeTest.MVVM.Views.WinTrigger
{
    public class WinTriggerView : ViewBehaviour<IWinTriggerViewModel>
    {
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            transform.position = ViewModel!.SpawnPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Player)) 
                return;
            ViewModel?.OnPlayerEnters();
        }
    }
}