using MazeTest.MVVM.Models.MazeCore.Domain;

namespace MazeTest.MVVM.Models.MazeCore
{
    public interface IMazeGenerator
    {
        IMaze GenerateRandom(int width, int height);
    }
}