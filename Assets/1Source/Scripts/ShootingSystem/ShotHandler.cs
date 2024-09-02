using Scripts.CameraSystem;
using Scripts.EnemySystem.Body;
using UnityEngine;
using VContainer;

namespace Scripts.ShootingSystem
{
    public class ShotHandler : MonoBehaviour
    {
        private readonly HitChecker _hitChecker = new HitChecker();

        [SerializeField] private PointerObserver[] _pointerObservers;
        
        private PlayerWeapon _weapon;

        private void OnDestroy()
        {
            foreach (PointerObserver pointerObserver in _pointerObservers)
                    pointerObserver.DragEnded -= OnDragEnded;
        }

        [Inject]
        private void Construct(PlayerWeapon weapon)
        {
            _weapon = weapon;

            foreach (PointerObserver pointerObserver in _pointerObservers)
                    pointerObserver.DragEnded += OnDragEnded;
        }

        private void OnDragEnded(PointerObserverType pointerObserverType)
        {
            if (pointerObserverType != PointerObserverType.AimButton)
                return;

            if (_hitChecker.Check(out Enemy enemy))
                _weapon.Shoot(enemy);
        }
    }
}