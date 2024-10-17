using System;
using UnityEngine;

namespace Scripts.GameConfigSystem
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float ObservationSpeed { get; private set; }

        [field: SerializeField] public float AimingSpeed { get; private set; }

        [field: SerializeField] public float TimeToExitAiming { get; private set; }

        [field: SerializeField] public PlayerWeaponConfig PlayerWeaponConfig { get; private set; }

        [field: SerializeField] public LocationConfig LocationConfig { get; private set; }

        [field: SerializeField] public uint LevelReward { get; private set; }

        [field: SerializeField] public float HeadshotBonusMultiplier { get; private set; }

        [field: SerializeField] public float ExplosionRadius { get; private set; }

        [field: SerializeField] public float MaxExplosionForce { get; private set; }

        [field: SerializeField] public float EnemyExplosionForceMultiplier { get; private set; }

        [field: SerializeField] public float LoadLevelDelay { get; private set; }

        [field: SerializeField] public int[] FacePrices { get; private set; }
    }

    [Serializable]
    public class LocationConfig
    {
        [field: SerializeField] public Sprite[] LocationIcons { get; private set; }

        [field: SerializeField] public uint LevelNumbers { get; private set; }
    }

    [Serializable]
    public class PlayerWeaponConfig
    {
        [field: SerializeField] public uint Damage { get; private set; }

        [field: SerializeField] public uint HeadshotMultiplier { get; private set; }

        [field: SerializeField] public uint AmmunitionCount { get; private set; }

        [field: SerializeField] public float BulletInsertionTime { get; private set; }

        [field: SerializeField] public float PreReloadTime { get; private set; }

        [field: SerializeField] public float ReloadTime { get; private set; }
    }
}