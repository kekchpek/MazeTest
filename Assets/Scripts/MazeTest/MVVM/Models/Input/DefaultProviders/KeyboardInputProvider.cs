using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using UnityEngine;
using Zenject;

namespace MazeTest.MVVM.Models.Input.DefaultProviders
{
    public class KeyboardInputProvider : MonoBehaviour, IVectorsInputProvider, IBinaryInputsProvider
    {

        private readonly IMutable<bool> _cameraSwap = new Mutable<bool>();
        private readonly IMutable<Vector3> _direction = new Mutable<Vector3>();

        private IInputProvidersRegister _register;

        [Inject]
        public void Construct(IInputProvidersRegister register)
        {
            _register = register;
            DontDestroyOnLoad(gameObject);
            _register.Register(this);
        }

        IEnumerable<(InputUnit unit, IBindable<bool> providingInputValue)> IBinaryInputsProvider.GetControlUnits()
        {
            return new[] { (InputUnit.CameraSwap, (IBindable<bool>)_cameraSwap) };
        }

        IEnumerable<(InputUnit unit, IBindable<Vector3> providingInputValue)> IVectorsInputProvider.GetControlUnits()
        {
            return new[] { (InputUnit.Direction, (IBindable<Vector3>)_direction) };
        }

        private void Update()
        {
            HandleDirection();
            HandleBinaryUnits();
        }

        private void HandleBinaryUnits()
        {
            _cameraSwap.Set(UnityEngine.Input.GetKey(KeyCode.Q));
        }

        private void HandleDirection()
        {
            var direction = new Vector3();
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }
            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }

            _direction.Value = direction.normalized;
        }

        private void OnDestroy()
        {
            _register.Unregister(this);
        }
    }
}