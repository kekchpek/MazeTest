using AsyncReactAwait.Bindable;
using MazeTest.Core;
using MazeTest.MVVM.Views.Enemy.Payload;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.MVVM.Views.Enemy
{
    public class EnemyViewModel : ViewModel, IEnemyViewModel, IInitializable
    {
        private const float DamageAreaAngle = 60f;
        private const float DamageAreaDistance = 10f;
        
        private const float DistanceEpsilon = 0.3f;
        
        private readonly IEnemyPayload _payload;

        private readonly IMutable<Vector3?> _destination = new Mutable<Vector3?>(Vector3.zero);

        private Vector3 _forward;
        private Vector3 _playerPosition;
        private Vector3? _position;

        private bool _playerInRange;

        private int _currentPatrolPoint;
        
        public IBindable<Vector3?> Destination => _destination;

        public Vector3 SpawnPosition => _payload.SpawnPosition;

        public EnemyViewModel(IEnemyPayload payload)
        {
            _payload = payload;
        }

        public void Initialize()
        {
            _payload.FetchedPlayerPosition.Bind(OnPlayerPositionChanged);
            GoToNextPatrolPoint();
        }
        
        public void SetOrientation(Vector3 forward)
        {
            _forward = forward;
            UpdateTarget();
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            UpdateTarget();
            CheckDestinationReached();
        }

        private void OnPlayerPositionChanged(Vector3 pos)
        {
            _playerPosition = pos;
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            if(!_position.HasValue)
                return;
            var playerFlatPos = new Vector2(_playerPosition.x, _playerPosition.z);
            var enemyFlatPos = new Vector2(_position.Value.x, _position.Value.z);
            var flatForward = new Vector2(_forward.x, _forward.z).normalized;
            var playerDir = playerFlatPos - enemyFlatPos;
            var angle = Vector3.Angle(playerDir, flatForward);
            var distance = Vector3.Distance(enemyFlatPos, playerFlatPos);
            if (angle < DamageAreaAngle &&
                distance < DamageAreaDistance &&
                Physics.Raycast(_position.Value, _playerPosition - _position.Value, out var raycastHit, DamageAreaDistance) &&
                raycastHit.collider.CompareTag(Tags.Player))
            {
                _playerInRange = true;
                UpdateDestination(_playerPosition);
            }
            else
            {
                _playerInRange = false;
            }
        }

        private void UpdateDestination(Vector3 destination)
        {
            _destination.Set(destination);
            CheckDestinationReached();
        }

        private void CheckDestinationReached()
        {
            if (_playerInRange || !_position.HasValue)
                return;
            if (_destination.Value == null)
            {
                GoToNextPatrolPoint();
                return;
            }

            var flatDestination = new Vector2(_destination.Value.Value.x, _destination.Value.Value.z);
            var enemyFlatPos = new Vector2(_position.Value.x, _position.Value.z);
            if (Vector2.Distance(flatDestination, enemyFlatPos) < DistanceEpsilon)
            {
                GoToNextPatrolPoint();
            }
        }

        private void GoToNextPatrolPoint()
        {
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _payload.PatrolPath.Length;
            UpdateDestination(_payload.PatrolPath[_currentPatrolPoint]);
        }

        protected override void OnDestroyInternal()
        {
            _payload.FetchedPlayerPosition.Unbind(OnPlayerPositionChanged);
            base.OnDestroyInternal();
        }
    }
}