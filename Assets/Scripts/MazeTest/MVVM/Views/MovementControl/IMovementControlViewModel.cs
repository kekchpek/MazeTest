using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.MovementControl
{
    public interface IMovementControlViewModel : IViewModel
    {
        void SetDownPosition(Vector2 downPos);
        void SetPressPosition(Vector2 pressPos);
        void OnUp();
    }
}