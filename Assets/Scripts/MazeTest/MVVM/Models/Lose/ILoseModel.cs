using AsyncReactAwait.Bindable;

namespace MazeTest.MVVM.Models.Lose
{
    public interface ILoseModel
    {
        IBindable<float?> LoseLeftTime { get; }
    }
}