using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Carmone.Item
{
    public class ItemDataProvider : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponType, ItemData> itemDataDict = new();

        public static ItemDataProvider Instance { get => instance; }
        private static ItemDataProvider instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public ItemData GetItemData(WeaponType weaponType)
        {
            return itemDataDict.GetValueOrDefault(weaponType, null);
        }
    }
}
