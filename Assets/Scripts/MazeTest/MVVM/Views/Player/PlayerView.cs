using System;
using UnityEngine;
using UnityMVVM;

namespace MazeTest.MVVM.Views.Player
{
    public class PlayerView : ViewBehaviour<IPlayerViewModel>
    {

        [SerializeField] private Rigidbody _rigidbody;

        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            transform.position = ViewModel!.SpawnPosition;
        }

        private void Update()
        {
            if (ViewModel != null)
            {
                var newVelocity = ViewModel.MoveDirection.Value;
                newVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = newVelocity;
                ViewModel.SetPosition(_cachedTransform.position);
            }
        }
    }
}