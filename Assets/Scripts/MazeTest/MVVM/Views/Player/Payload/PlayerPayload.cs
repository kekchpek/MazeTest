
using UnityEngine;

namespace MazeTest.MVVM.Views.Player.Payload
{
    public class PlayerPayload : IPlayerPayload
    {
        public Vector3 SpawnPosition { get; }
        public PlayerPayload(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
    }
}