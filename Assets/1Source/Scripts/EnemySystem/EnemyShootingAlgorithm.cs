﻿using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Scripts.EnemySystem
{

    public class EnemyShootingAlgorithm : EnemyAlgorithm
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        [SerializeField] private Bullet _bullet;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _bulletsSpawnPoint;
        [SerializeField] private float _yOffset;
        [SerializeField] private float _shootingDelay;
        [SerializeField] private float _bulletSpread;

        public Vector3 PlayerPosition => _player.transform.position;

        private float TargetYCenter => PlayerPosition.y + _yOffset;

        private void OnValidate()
        {
            if (_player == null)
                _player = FindAnyObjectByType<Player>();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }

        public override async UniTask Run()
        {
            Animator.SetTrigger(ShootingTrigger);
            Mover.Rotate(PlayerPosition, _cancellationTokenSource);
            await StartShooting();
        }

        public override void Stop()
        {
            base.Stop();
            _cancellationTokenSource.Cancel();
        }

        private async UniTask StartShooting()
        {
            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                await UniTask.WaitForSeconds(_shootingDelay, cancellationToken: _cancellationTokenSource.Token);

                Bullet bullet = Instantiate(_bullet, _bulletsSpawnPoint.position, Quaternion.identity);
                Vector3 direction = GetRandomDirectionWithinRadius(PlayerPosition, _bulletSpread);

                if (direction.y < TargetYCenter)
                {
                    float invertedY = (TargetYCenter - direction.y) * 2;
                    direction.y += invertedY;
                }

                Vector3 velocity = (direction - _bulletsSpawnPoint.position).normalized;
                bullet.SetVelocity(velocity);
            }
        }

        private Vector3 GetRandomDirectionWithinRadius(Vector3 center, float maxRadius)
        {
            Vector3 randomPoint = Random.insideUnitCircle * maxRadius;
            center.y += _yOffset;

            return center + randomPoint;
        }
    }
}
