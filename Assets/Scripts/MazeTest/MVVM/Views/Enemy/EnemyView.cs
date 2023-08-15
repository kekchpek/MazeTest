using System;
using UnityEngine;
using UnityEngine.AI;
using UnityMVVM;

namespace MazeTest.MVVM.Views.Enemy
{
    public class EnemyView : ViewBehaviour<IEnemyViewModel>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            transform.position = ViewModel!.SpawnPosition;
            _navMeshAgent.enabled = true;
            SmartBind(ViewModel!.Destination, OnDestinationChanged);
            SmartBind(ViewModel!.Speed, x => _navMeshAgent.speed = x);
        }

        private void OnDestinationChanged(Vector3? destination)
        {
            _navMeshAgent.destination = destination ?? transform.position;
        }

        private void Update()
        {
            ViewModel?.SetOrientation(_cachedTransform.forward);
            ViewModel?.SetPosition(_cachedTransform.position);
        }
    }
}