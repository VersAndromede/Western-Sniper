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