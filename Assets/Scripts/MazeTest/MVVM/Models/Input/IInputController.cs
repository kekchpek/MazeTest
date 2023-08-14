using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.Input
{
    public interface IInputController
    {
        IBindable<Vector3> GetVectorInput(InputUnit unit);
        IBindable<bool> GetBinaryInput(InputUnit unit);
    }
}