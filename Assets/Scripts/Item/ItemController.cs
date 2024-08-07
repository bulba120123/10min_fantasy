using UnityEngine;
using EnumTypes;
using Carmone.Weapons;


namespace Carmone.Item
{
    public class ItemController : MonoBehaviour
    {
        public ItemData data;
        public WeaponType WeaponType { get => data.weaponType; }
        public int level;
        public Weapon weapon;
        public Gear gear;
        public bool available;

        private ItemUI itemUI;

        private void Awake()
        {
            itemUI = GetComponent<ItemUI>();
            itemUI.InitializeUI(data, level);
        }

        public void OnClick(ItemBtnType itemBtnType)
        {
            if (!available)
                return;
            switch (itemBtnType)
            {
                case ItemBtnType.LevelUp:
                    HandleItemType(data.itemType);
                    itemUI.UpdateUI(data, level);
                    GameManager.instance.uiLevelUp.Hide();
                    itemUI.CheckInteractable(data, level);
                    break;
                case ItemBtnType.Town:
                    if (GameManager.instance.gold < data.price)
                    {
                        return;
                    }
                    HandleItemType(data.itemType);
                    itemUI.UpdateUI(data, level);
                    GameManager.instance.gold -= data.price;
                    itemUI.CheckAvailable(data, level);
                    break;
            }
        }

        public void GetItem()
        {
        }

        private void HandleItemType(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Melee:
                case ItemType.Range:
                    HandleWeapon();
                    break;
                case ItemType.Gear:
                    HandleGear();
                    break;
                case ItemType.Heal:
                    GameManager.instance.health = GameManager.instance.maxHealth;
                    break;
            }
        }

        private void HandleWeapon()
        {
            if (level == 0)
            {
                CreateWeapon();
            }
            else
            {
                UpgradeWeapon();
            }
            level++;
        }

        private void HandleGear()
        {
            if (level == 0)
            {
                CreateGear();
            }
            else
            {
                UpgradeGear();
            }
            level++;
        }

        private void CreateWeapon()
        {
            GameObject newWeapon = new GameObject();
            weapon = newWeapon.AddComponent<Weapon>();
            weapon.Init(data);
        }

        private void UpgradeWeapon()
        {
            var levelStat = data.levelStat[level - 1];
            weapon.LevelUp(levelStat);
        }

        private void CreateGear()
        {
            GameObject newGear = new GameObject();
            gear = newGear.AddComponent<Gear>();
            gear.Init(data);
        }

        private void UpgradeGear()
        {
            gear.Levelup(data.levelGearStat[level]);
        }

        public void EnhanceWeapon(int type)
        {
            var enhancement = data.enhancedWeapons[type];
            var prefabType = enhancement.bulletType;
            var effects = enhancement.enhancementEffects;
            weapon.EnchantWeapon(prefabType, effects);
        }
    }
}