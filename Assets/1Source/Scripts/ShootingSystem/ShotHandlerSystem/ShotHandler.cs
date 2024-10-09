using ExplodingBarrelSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameStateSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using UnityEngine;
using VContainer;

namespace Scripts.ShootingSystem.ShotHandlerSystem
{
    public class ShotHandler : MonoBehaviour
    {
        private readonly HitChecker _hitChecker = new ();

        [SerializeField] private PointerObserver _pointerObserver;
        [SerializeField] private LayerMask _layerMask;

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

        private void OnDragEnded(GameState gameState)
        {
            if (gameState.Type == GameStateType.Observation || gameState.IsGameOver)
                return;

            if (_hitChecker.IsHitOnEnemy(_layerMask, out WeaponShotPoint shotPoint))
            {
                _weapon.ShootEnemy(shotPoint);
            }
            else if (_hitChecker.IsHitOnExplodingBarrel(_layerMask, out ExplodingBarrel explodingBarrel))
            {
                explodingBarrel.BlowUp();
                _weapon.Shoot(shotPoint.Position);
            }
            else
            {
                _weapon.Shoot(shotPoint.Position);
            }
        }
    }
}