using Scripts.CameraSystem;
using Scripts.EnemySystem;
using UnityEngine;
using VContainer;

namespace Scripts.ShootingSystem
{
    public class ShotHandler : MonoBehaviour
    {
        private readonly HitChecker _hitChecker = new HitChecker();

        [SerializeField] private PointerObserver _pointerObserver;
        
        private PlayerWeapon _weapon;

        private void OnDestroy()
        {
            _pointerObserver.DragEnded -= OnDragEnded;
        }

        [Inject]
        private void Construct(PlayerWeapon weapon)
        {
            _weapon = weapon;

            _pointerObserver.DragEnded += OnDragEnded;
        }

        private void OnDragEnded(PointerObserverType pointerObserverType)
        {
            if (pointerObserverType != PointerObserverType.AimButton)
                return;

            if (_hitChecker.Check(out Enemy enemy, out Vector3 hitPoint))
                _weapon.Shoot(enemy, hitPoint);
        }
    }
}