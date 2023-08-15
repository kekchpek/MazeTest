using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using MazeTest.MVVM.Models.Input;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.MVVM.Views.CameraControl
{
    public class CameraControlViewModel : ViewModel, ICameraControlViewModel, IBinaryInputsProvider, IInitializable
    {
        private readonly IInputProvidersRegister _inputProvidersRegister;

        private readonly IMutable<bool> _cameraSwapInput = new Mutable<bool>();

        public CameraControlViewModel(IInputProvidersRegister inputProvidersRegister)
        {
            _inputProvidersRegister = inputProvidersRegister;
        }

        public void Initialize()
        {
            _inputProvidersRegister.Register(this);
        }

        public IEnumerable<(InputUnit unit, IBindable<bool> providingInputValue)> GetControlUnits()
        {
            return new[] { (InputUnit.CameraSwap, (IBindable<bool>)_cameraSwapInput) };
        }

        public void OnClick()
        {
            _cameraSwapInput.Set(true);
            _cameraSwapInput.Set(false);
        }

        protected override void OnDestroyInternal()
        {
            _inputProvidersRegister.Unregister(this);
            base.OnDestroyInternal();
        }
    }
}