using AsyncReactAwait.Bindable;

namespace MazeTest.MVVM.Models.Lose
{
    public class LoseModel : ILoseMutableModel
    {
        private readonly IMutable<float?> _loseLeftTime = new Mutable<float?>();
        
        public IBindable<float?> LoseLeftTime => _loseLeftTime;
        
        public void SetLoseLeftTime(float? loseTime)
        {
            _loseLeftTime.Value = loseTime;
        }
    }
}