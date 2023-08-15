using System.Linq;
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

        private const string ViewsPath = "Views/";

        [SerializeField] private Transform[] _viewLayers;
        
        public override void InstallBindings()
        {
            Container.UseAsMvvmContainer(_viewLayers.Select(x => (x.name, x)).ToArray());

            Container.Bind<IStartupService>().To<StartupService>().AsTransient()
                .WhenInjectedInto<StartupBehaviour>();
            
            Container.InstallView<MainMenuView, IMainMenuViewModel, MainMenuViewModel>(
                ViewNames.MainMenu, 
                () => Resources.Load<GameObject>(GetViewPath("MainMenuView")));
            Container.InstallView<PlayerView, IPlayerViewModel, PlayerViewModel>(
                ViewNames.Player,
                () => Resources.Load<GameObject>(GetViewPath("PlayerView")));
            Container.InstallView<WinTriggerView, IWinTriggerViewModel, WinTriggerViewModel>(
                ViewNames.WinTrigger,
                () => Resources.Load<GameObject>(GetViewPath("WinTriggerView")));
            Container.InstallView<MazeView, IMazeViewModel, MazeViewModel>(
                ViewNames.Maze,
                () => Resources.Load<GameObject>(GetViewPath("MazeView")));
            
            Container.InstallView<WinPopupView, IWinPopupViewModel, WinPopupViewModel>(
                ViewNames.WinPopup,
                () => Resources.Load<GameObject>(GetViewPath("WinPopupView")));
            Container.InstallView<LosePopupView, ILosePopupViewModel, LosePopupViewModel>(
                ViewNames.LosePopup,
                () => Resources.Load<GameObject>(GetViewPath("LosePopupView")));
            Container.InstallView<EnemyView, IEnemyViewModel, EnemyViewModel>(
                ViewNames.Enemy,
                () => Resources.Load<GameObject>(GetViewPath("EnemyView")));
            
            Container.InstallView<GameOverlayView, IViewModel, ViewModel>(
                ViewNames.GameOverlay,
                () => Resources.Load<GameObject>(GetViewPath("GameOverlayView")));
            Container.InstallView<GameView, IGameViewModel, GameViewModel>(
                ViewNames.Game, 
                () => Resources.Load<GameObject>(GetViewPath("GameView")));
            Container.InstallView<LoseTimerView, ILoseTimerViewModel, LoseTimerViewModel>();
            
            Container.FastBind<IGameService, GameService>();
            Container.FastBind<IGameMutableModel, IGameModel, GameModel>();
            
            Container.FastBind<IMazeGenerator, MazeGenerator>();
            Container.FastBindMono<IInputController, InputController>();
            
            Container.FastBind<ICameraService, CameraService>();
            
            Container.FastBind<INavigationService, NavigationService>();
            
            Container.FastBindMono<ILoseService, LoseService>();
            Container.FastBind<ILoseMutableModel, ILoseModel, LoseModel>();
        }

        private static string GetViewPath(string viewName)
        {
            return ViewsPath + viewName;
        }
    }
}
