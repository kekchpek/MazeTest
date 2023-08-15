using System.Linq;
using MazeTest.Core.Installers;
using MazeTest.Core.Startup;
using MazeTest.MVVM.Models.CameraService;
using MazeTest.MVVM.Models.Game;
using MazeTest.MVVM.Models.Input;
using MazeTest.MVVM.Models.Lose;
using MazeTest.MVVM.Models.MazeCore;
using MazeTest.MVVM.Models.NavigationService;
using MazeTest.MVVM.Views.Enemy;
using MazeTest.MVVM.Views.Game;
using MazeTest.MVVM.Views.GameOverlay;
using MazeTest.MVVM.Views.LosePopup;
using MazeTest.MVVM.Views.LoseTimer;
using MazeTest.MVVM.Views.MainMenu;
using MazeTest.MVVM.Views.MazeView;
using MazeTest.MVVM.Views.Player;
using MazeTest.MVVM.Views.WinPopup;
using MazeTest.MVVM.Views.WinTrigger;
using UnityEngine;
using UnityMVVM;
using UnityMVVM.DI;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _viewLayers;
        
        public override void InstallBindings()
        {
            Container.UseAsMvvmContainer(_viewLayers.Select(x => (x.name, x)).ToArray());

            Container.Bind<IStartupService>().To<StartupService>().AsTransient()
                .WhenInjectedInto<StartupBehaviour>();
            
            Container.InstallView<MainMenuView, IMainMenuViewModel, MainMenuViewModel>(
                ViewNames.MainMenu, 
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("MainMenuView")));
            
            Container.FastBind<ICameraService, CameraService>();

            Container.Install<InputInstaller>();
            Container.Install<GameInstaller>();
        }
    }
}
