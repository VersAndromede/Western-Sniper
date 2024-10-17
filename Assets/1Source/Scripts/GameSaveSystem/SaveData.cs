using System;
using System.Collections.Generic;
using Scripts.LevelSystem;
using UnityEngine;
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
        [Preserve]
        public ShopItemsData ShopItemsData = new();
        [Preserve]
        public int FacePriceIndex;
    }

    [Serializable]
    public class ShopItemsData
    {
        [field: SerializeField] public List<ShopItem> ShopItems { get; private set; } = new List<ShopItem>();

        [field: SerializeField] public int SelectedFace { get; private set; }

        [field: SerializeField] public int SelectedWeapon { get; private set; }

        [field: SerializeField] public int SelectedHat { get; private set; }

        public event Action Changed;

        public void UnlockItem(int index)
        {
            ShopItems[index].Unlock();
            Changed?.Invoke();
        }

        public void SetFace(int index)
        {
            SelectedFace = index;
            Changed?.Invoke();
        }

        public void SetWeapon(int index)
        {
            SelectedWeapon = index;
            Changed?.Invoke();
        }

        public void SetHat(int index)
        {
            SelectedHat = index;
            Changed?.Invoke();
        }
    }

    [Serializable]
    public class ShopItem
    {
        [field: SerializeField] public bool Locked { get; private set; } = true;

        public void Unlock()
        {
            Locked = false;
        }
    }
}