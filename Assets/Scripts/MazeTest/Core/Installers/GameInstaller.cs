using MazeTest.MVVM.Models.Game;
using MazeTest.MVVM.Models.Lose;
using MazeTest.MVVM.Models.MazeCore;
using MazeTest.MVVM.Models.NavigationService;
using MazeTest.MVVM.Views.Enemy;
using MazeTest.MVVM.Views.Game;
using MazeTest.MVVM.Views.GameOverlay;
using MazeTest.MVVM.Views.LosePopup;
using MazeTest.MVVM.Views.LoseTimer;
using MazeTest.MVVM.Views.MazeView;
using MazeTest.MVVM.Views.Player;
using MazeTest.MVVM.Views.WinPopup;
using MazeTest.MVVM.Views.WinTrigger;
using UnityEngine;
using UnityMVVM.DI;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.Core.Installers
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallView<GameOverlayView, IViewModel, ViewModel>(
                ViewNames.GameOverlay,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("GameOverlayView")));
            Container.InstallView<GameView, IGameViewModel, GameViewModel>(
                ViewNames.Game, 
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("GameView")));
            Container.InstallView<PlayerView, IPlayerViewModel, PlayerViewModel>(
                ViewNames.Player,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("PlayerView")));
            Container.InstallView<WinTriggerView, IWinTriggerViewModel, WinTriggerViewModel>(
                ViewNames.WinTrigger,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("WinTriggerView")));
            Container.InstallView<MazeView, IMazeViewModel, MazeViewModel>(
                ViewNames.Maze,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("MazeView")));
            Container.InstallView<WinPopupView, IWinPopupViewModel, WinPopupViewModel>(
                ViewNames.WinPopup,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("WinPopupView")));
            Container.InstallView<LosePopupView, ILosePopupViewModel, LosePopupViewModel>(
                ViewNames.LosePopup,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("LosePopupView")));
            Container.InstallView<EnemyView, IEnemyViewModel, EnemyViewModel>(
                ViewNames.Enemy,
                () => Resources.Load<GameObject>(AssetPaths.GetViewPath("EnemyView")));
            Container.InstallView<LoseTimerView, ILoseTimerViewModel, LoseTimerViewModel>();
            
            Container.FastBind<IGameService, GameService>();
            Container.FastBind<IGameMutableModel, IGameModel, GameModel>();
            
            Container.FastBind<IMazeGenerator, MazeGenerator>();
            
            Container.FastBindMono<ILoseService, LoseService>();
            Container.FastBind<ILoseMutableModel, ILoseModel, LoseModel>();
            
            Container.FastBind<INavigationService, NavigationService>();
        }
    }
}