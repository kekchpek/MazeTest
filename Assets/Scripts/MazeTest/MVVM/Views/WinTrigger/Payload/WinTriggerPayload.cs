using UnityEngine;

namespace MazeTest.MVVM.Views.WinTrigger.Payload
{
    public class WinTriggerPayload : IWinTriggerPayload
    {
        public Vector3 SpawnPosition { get; }
        public WinTriggerPayload(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
    }
}