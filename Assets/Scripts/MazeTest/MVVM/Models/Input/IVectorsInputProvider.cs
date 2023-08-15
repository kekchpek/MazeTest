using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Models.Input
{
    public interface IVectorsInputProvider : IInputProvider
    {
        IEnumerable<(InputUnit unit, IBindable<Vector3> providingInputValue)> GetControlUnits();
    }
}