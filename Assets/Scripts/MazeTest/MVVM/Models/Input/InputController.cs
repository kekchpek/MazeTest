using System;
using System.Collections.Generic;
using System.Linq;
using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.Input
{
    public class InputController : IInputController, IInputProvidersRegister
    {
        private readonly Dictionary<InputUnit, IMutable<Vector3>> _vectorInputs = new();
        private readonly Dictionary<InputUnit, IMutable<bool>> _binaryInputs = new();

        private readonly Dictionary<InputUnit, Action> _inputUpdateCallbacks = new();
        private readonly Dictionary<InputUnit, List<IBindable<Vector3>>> _vectorInputSources = new();
        private readonly Dictionary<InputUnit, List<IBindable<bool>>> _binaryInputSources = new();

        public InputController()
        {
            _vectorInputs.Add(InputUnit.Direction, new Mutable<Vector3>());
            _binaryInputs.Add(InputUnit.CameraSwap, new Mutable<bool>());
            foreach (InputUnit unit in Enum.GetValues(typeof(InputUnit)))
            {
                if (_vectorInputs.ContainsKey(unit))
                {
                    _vectorInputSources.Add(unit, new List<IBindable<Vector3>>());
                }
                if (_binaryInputs.ContainsKey(unit))
                {
                    _binaryInputSources.Add(unit, new List<IBindable<bool>>());
                }

                void UpdateUnit()
                {
                    if (_vectorInputs.TryGetValue(unit, out var vectorInputValue))
                    {
                        vectorInputValue.Value = _vectorInputSources[unit]
                            .Aggregate(Vector3.zero, (r, x) =>
                            {
                                if (x.Value.magnitude > r.magnitude)
                                {
                                    return x.Value;
                                }
                                return r;
                            });
                    }
                    if (_binaryInputs.TryGetValue(unit, out var binaryInputValue))
                    {
                        binaryInputValue.Value = _binaryInputSources[unit]
                            .Aggregate(false, (r, x) => r || x.Value);
                    }
                }
                _inputUpdateCallbacks.Add(unit, UpdateUnit);
            }
        }
        
        public void Register(IInputProvider provider)
        {
            if (provider is IVectorsInputProvider vectorsInputProvider)
            {
                foreach (var controlUnit in vectorsInputProvider.GetControlUnits())
                {
                    controlUnit.providingInputValue.Bind(_inputUpdateCallbacks[controlUnit.unit]);
                    _vectorInputSources[controlUnit.unit].Add(controlUnit.providingInputValue);
                }
            }

            if (provider is IBinaryInputsProvider binaryInputsProvider)
            {
                foreach (var controlUnit in binaryInputsProvider.GetControlUnits())
                {
                    controlUnit.providingInputValue.Bind(_inputUpdateCallbacks[controlUnit.unit]);
                    _binaryInputSources[controlUnit.unit].Add(controlUnit.providingInputValue);
                }
            }
        }

        public void Unregister(IInputProvider provider)
        {
            if (provider is IVectorsInputProvider vectorsInputProvider)
            {
                foreach (var controlUnit in vectorsInputProvider.GetControlUnits())
                {
                    controlUnit.providingInputValue.Unbind(_inputUpdateCallbacks[controlUnit.unit]);
                    _vectorInputSources[controlUnit.unit].Remove(controlUnit.providingInputValue);
                }
            }
            else if (provider is IBinaryInputsProvider binaryInputsProvider)
            {
                foreach (var controlUnit in binaryInputsProvider.GetControlUnits())
                {
                    controlUnit.providingInputValue.Unbind(_inputUpdateCallbacks[controlUnit.unit]);
                    _binaryInputSources[controlUnit.unit].Remove(controlUnit.providingInputValue);
                }
            }
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