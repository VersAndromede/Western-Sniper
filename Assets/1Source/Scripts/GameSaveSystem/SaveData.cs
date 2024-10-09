using Scripts.LevelSystem;
using System;
using UnityEngine.Scripting;

namespace Modules.SavingsSystem
{
    [Serializable]
    public class SaveData
    {
        [Preserve]
        public bool MusicEnabled = true;
        [Preserve]
        public uint Currency;
        [Preserve]
        public Level Level = new();
    }
}