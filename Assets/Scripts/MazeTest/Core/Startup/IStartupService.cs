using AsyncReactAwait.Promises;

namespace MazeTest.Core.Startup
{
    public interface IStartupService
    {
        IPromise Startup();
    }
}