using MazeTest.MVVM.Models.Input;
using MazeTest.MVVM.Models.Input.DefaultProviders;
using UnityMVVM.DI;
using Zenject;

namespace MazeTest.Core.Installers
{
    public class InputInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.FastBind<InputController>(new []
            {
                typeof(IInputController),
                typeof(IInputProvidersRegister)
            });
            Container.InstantiateComponentOnNewGameObject<KeyboardInputProvider>();
        }
    }
}