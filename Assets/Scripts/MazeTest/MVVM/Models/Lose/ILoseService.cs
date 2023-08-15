using System;

namespace MazeTest.MVVM.Models.Lose
{
    public interface ILoseService
    {
        event Action Lost;
        
        /// <summary>
        /// Starts a lose timer.
        /// </summary>
        /// <returns>Returns timer id to stop timer if needed.</returns>
        int StartLoseTimer();
        
        /// <summary>
        /// Stops the lose timer.
        /// </summary>
        void StopLoseTimer(int timerId);
    }
}