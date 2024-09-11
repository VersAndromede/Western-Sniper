using Scripts.ShootingSystem.PlayerWeaponSystem;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class EnemyStateHandler : MonoBehaviour
    {
        [SerializeField] private MainEnemy _mainTarget;

        private Enemy[] _enemies;
        private PlayerWeapon _playerWeapon;

        private bool _isHandled;

        private void OnValidate()
        {
            if (_mainTarget == null)
                _mainTarget = FindAnyObjectByType<MainEnemy>();
        }

        private void OnDestroy()
        {
            _playerWeapon.ShotFired -= OnShotFired;
        }

        public void Init(Enemy[] enemies, PlayerWeapon playerWeapon)
        {
            _enemies = enemies;
            _playerWeapon = playerWeapon;

            _playerWeapon.ShotFired += OnShotFired;
        }

        private void OnShotFired(Vector3 _)
        {
            if (_isHandled)
                return;

            foreach (Enemy enemy in _enemies)
                enemy.RunAlgorithm();

            _mainTarget.RunAlgorithm();
            _isHandled = true;
        }
    }
}
