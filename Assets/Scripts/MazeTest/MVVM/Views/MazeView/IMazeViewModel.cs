using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using MazeTest.MVVM.Views.MazeView.Domain;
using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace MazeTest.MVVM.Views.MazeView
{
    public interface IMazeViewModel : IViewModel
    {
        IBindable<IReadOnlyList<WallData>> Walls { get; }
        IBindable<float> FloorWidth { get; }
        IBindable<float> FloorHeight { get; }
        IBindable<float> CellWidth { get; }
        IBindable<float> CellHeight { get; }
        Vector3 GetCellCenter(int x, int y);
    }
}