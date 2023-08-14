using MazeTest.MVVM.Models.MazeCore.Domain;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.MazeView.Payload
{
    public interface IMazePayload : IPayload
    {
        float Width { get; }
        float Height { get; }
        IMaze Maze { get; }
    }
}