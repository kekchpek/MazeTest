using System;
using System.Collections.Generic;
using System.Linq;
using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;
using MazeTest.MVVM.Models.CameraService.Components;
using MazeTest.MVVM.Models.Input;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace MazeTest.MVVM.Models.CameraService
{
    public class CameraService : ICameraService, IInitializable
    {
        private readonly IInputController _inputController;

        private readonly List<ICameraAnchor> _anchors = new();
        private Camera _camera;
        private Transform _cameraTransform;

        private int _activeAnchorIndex = -1;
        private ICameraAnchor _activeAnchor;

        private IBindable<bool> _swapPressed;

        public CameraService(IInputController inputController)
        {
            _inputController = inputController;
        }
        
        public void Initialize()
        {
            _camera = Camera.main!;
            _cameraTransform = _camera.transform;
            _swapPressed = _inputController.GetBinaryInput(InputUnit.CameraSwap);
            _swapPressed.Bind(OnSwapPressedChanged);
        }

        private void OnSwapPressedChanged(bool pressed)
        {
            if (pressed) SwapCamera();
        }

        public void SwapCamera()
        {
            ClearActiveAnchor();
            if (_anchors.Any())
            {
                _activeAnchorIndex = (_activeAnchorIndex + 1) % _anchors.Count;
                SetActiveAnchor(_anchors[_activeAnchorIndex]);
            }
        }

        void ClearActiveAnchor()
        {
            if (_activeAnchor != null)
            {
                _activeAnchor.Orientation.Unbind(UpdateCameraRotation);
                _activeAnchor.Position.Unbind(UpdateCameraPosition);
                _activeAnchor = null;
            }
        }

        void SetActiveAnchor(ICameraAnchor anchor)
        {
            if (_activeAnchor != null)
            {
                throw new Exception("Previous anchor is not cleared");
            }
            _activeAnchor = anchor;
            _activeAnchor.Position.Bind(UpdateCameraPosition);
            _activeAnchor.Orientation.Bind(UpdateCameraRotation);
        }

        void UpdateCameraPosition(Vector3 pos)
        {
            if (!_cameraTransform)
                return;
            _cameraTransform.position = pos;
        }

        void UpdateCameraRotation(Quaternion rotation)
        {
            if (!_cameraTransform)
                return;
            _cameraTransform.rotation = rotation;
        }

        public void FetchCameraAnchors()
        {
            ClearActiveAnchor();
            _activeAnchorIndex = -1;
            _anchors.AddRange(Object.FindObjectsOfType<MonoBehaviour>().OfType<ICameraAnchor>());
            foreach (var anchor in _anchors.ToArray())
            {
                void OnAnchorDestroyed(bool isDestroyed)
                {
                    if (!isDestroyed)
                        return;
                    _anchors.Remove(anchor);
                    anchor.Destroyed.Unbind(OnAnchorDestroyed);
                    if (anchor == _activeAnchor)
                    {
                        SwapCamera();
                    }
                }
                anchor.Destroyed.Bind(OnAnchorDestroyed);
            }
            if (_anchors.Any())
            {
                if (_activeAnchor == null)
                {
                    SwapCamera();
                }
            }
        }
    }
}