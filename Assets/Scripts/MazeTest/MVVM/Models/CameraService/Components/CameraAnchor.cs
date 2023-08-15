using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.CameraService.Components
{
    public class CameraAnchor : MonoBehaviour, ICameraAnchor
    {
        private readonly IMutable<Vector3> _position = new Mutable<Vector3>();
        private readonly IMutable<Quaternion> _orientation = new Mutable<Quaternion>();
        private readonly IMutable<bool> _destroyed = new Mutable<bool>();

        public IBindable<bool> Destroyed => _destroyed;
        public IBindable<Vector3> Position => _position;
        public IBindable<Quaternion> Orientation => _orientation;

        private void Update()
        {
            _position.Set(transform.position);
            _orientation.Set(transform.rotation);
        }
        
        private void OnDisable()
        {
            if (gameObject.activeInHierarchy)
            {
                _destroyed.Set(true);
            }
        }
    }
}