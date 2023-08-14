using AsyncReactAwait.Promises;
using UnityMVVM.ViewManager;

namespace MazeTest.Core.Startup
{
    public class StartupService : IStartupService
    {
        private readonly IViewManager _viewManager;

        public StartupService(
            IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
        
        public IPromise Startup()
        {
            return _viewManager.Open(ViewLayers.ScreenLayer, ViewNames.MainMenu);
        }
    }
}