using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class EnemyRunAndShootingAlgorithm : EnemyAlgorithm
    {
        [SerializeField] private EnemyRunAlgorithm _runAlgorithm;
        [SerializeField] private EnemyShootingAlgorithm _shootingAlgorithm;

        public override async UniTask Run()
        {
            await _runAlgorithm.Run();
            Mover.Rotate(_shootingAlgorithm.PlayerPosition, _runAlgorithm.CancellationTokenSource);
            _shootingAlgorithm.Run();
        }

        public override void Stop()
        {
            _runAlgorithm.Stop();
            _shootingAlgorithm.Stop();
        }
    }
}
