using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using MazeTest.MVVM.Models.Input;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.MVVM.Views.MovementControl
{
    public class MovementControlViewModel : ViewModel, IMovementControlViewModel, IVectorsInputProvider, IInitializable
    {
        private readonly IInputProvidersRegister _inputProvidersRegister;

        private readonly IMutable<Vector3> _direction = new Mutable<Vector3>();

        private Vector2? _downPos;

        public MovementControlViewModel(IInputProvidersRegister inputProvidersRegister)
        {
            _inputProvidersRegister = inputProvidersRegister;
        }

        public void Initialize()
        {
            _inputProvidersRegister.Register(this);
        }
        
        public void SetDownPosition(Vector2 downPos)
        {
            _downPos = downPos;
        }

        public void SetPressPosition(Vector2 pressPos)
        {
            if (!_downPos.HasValue)
                return;
            var dir = pressPos - _downPos.Value;
            dir.Normalize();
            _direction.Set(new Vector3(dir.x, 0f, dir.y));
        }

        public void OnUp()
        {
            _downPos = null;
            _direction.Set(new Vector3());
        }

        public IEnumerable<(InputUnit unit, IBindable<Vector3> providingInputValue)> GetControlUnits()
        {
            return new[] { (InputUnit.Direction, (IBindable<Vector3>)_direction) };
        }

        protected override void OnDestroyInternal()
        {
            _inputProvidersRegister.Unregister(this);
            base.OnDestroyInternal();
        }
    }
}