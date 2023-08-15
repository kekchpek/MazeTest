using System.Collections.Generic;
using AsyncReactAwait.Bindable;

namespace MazeTest.MVVM.Models.Input
{
    public interface IBinaryInputsProvider : IInputProvider
    {
        IEnumerable<(InputUnit unit, IBindable<bool> providingInputValue)> GetControlUnits();
    }
}