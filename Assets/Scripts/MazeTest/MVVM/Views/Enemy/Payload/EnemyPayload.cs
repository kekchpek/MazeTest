using AsyncReactAwait.Bindable;
using UnityEngine;

namespace MazeTest.MVVM.Views.Enemy.Payload
{
    public class EnemyPayload : IEnemyPayload
    {
        public Vector3[] PatrolPath { get; }
        public Vector3 SpawnPosition { get; }
        public IBindable<Vector3> FetchedPlayerPosition { get; }

        public EnemyPayload(Vector3 spawnPosition, IBindable<Vector3> fetchedPlayerPosition, Vector3[] patrolPath)
        {
            SpawnPosition = spawnPosition;
            FetchedPlayerPosition = fetchedPlayerPosition;
            PatrolPath = patrolPath;
        }
    }
}