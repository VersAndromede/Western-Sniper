using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using UnityEngine;
using VContainer;

namespace Scripts.ShootingSystem.ShotHandlerSystem
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

            if (_hitChecker.IsHitOnEnemy(out WeaponShotPoint shotPoint))
                _weapon.ShootEnemy(shotPoint);
            else
                _weapon.Shoot(shotPoint.Position);
        }
    }
}