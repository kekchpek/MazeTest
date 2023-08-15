namespace MazeTest.MVVM.Models.Input
{
    public interface IInputProvidersRegister
    {
        void Register(IInputProvider provider);
        void Unregister(IInputProvider provider);
    }
}