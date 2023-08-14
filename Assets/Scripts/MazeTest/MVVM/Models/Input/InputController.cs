using System;
using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.Input
{
    public class InputController : MonoBehaviour, IInputController
    {
        private readonly Dictionary<InputUnit, IMutable<Vector3>> _vectorInputs = new();
        private readonly Dictionary<InputUnit, IMutable<bool>> _binaryInputs = new();

        public InputController()
        {
            _vectorInputs.Add(InputUnit.Direction, new Mutable<Vector3>());
            
            _binaryInputs.Add(InputUnit.CameraSwap, new Mutable<bool>());
        }

        private void Update()
        {
            HandleDirection();
            HandleBinaryUnits();
        }

        private void HandleBinaryUnits()
        {
            _binaryInputs[InputUnit.CameraSwap].Set(UnityEngine.Input.GetKey(KeyCode.Q));
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

            _vectorInputs[InputUnit.Direction].Value = direction.normalized;
        }

        public IBindable<Vector3> GetVectorInput(InputUnit unit)
        {
            if (_vectorInputs.TryGetValue(unit, out var value))
            {
                return value;
            }

            throw new Exception($"{unit} is not a vector input");
        }

        public IBindable<bool> GetBinaryInput(InputUnit unit)
        {
            if (_binaryInputs.TryGetValue(unit, out var value))
            {
                return value;
            }

            throw new Exception($"{unit} is not a binary input");
        }
    }
}