using System;
using UnityEngine;

namespace Scripts.LevelSystem
{

    [Serializable]
    public class Level
    {
        private const uint StartLevel = 1;

        [field: SerializeField] public uint Number { get; private set; } = StartLevel;

        [field: SerializeField] public uint Location { get; private set; } = StartLevel;

        public event Action Changed;

        public void SetNextLevel(uint maxLevel)
        {
            if (Number == maxLevel)
                return;
            
            Number++;
            Changed?.Invoke();
        }

        public void SetNextLocation(uint numberLevelsInLocation)
        {
            if (IsMightSetNextLocation(numberLevelsInLocation))
            {
                Location++;
                Changed?.Invoke();
            }
        }

        private bool IsMightSetNextLocation(uint numberLevelsInLocation)
        {
            return Number % numberLevelsInLocation == 1;
        }
    }
}