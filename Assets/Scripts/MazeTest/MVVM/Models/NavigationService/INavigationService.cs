using AsyncReactAwait.Promises;
using UnityEngine;

namespace MazeTest.MVVM.Models.NavigationService
{
    public interface INavigationService
    {

        void BakeNavigationData(Bounds area, Bounds[] obstacles);

    }
}