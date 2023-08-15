using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.CameraService.Components
{
    public interface ICameraAnchor
    {
        IBindable<bool> Destroyed { get; }
        IBindable<Vector3> Position { get; }
        IBindable<Quaternion> Orientation { get; }
    }
}