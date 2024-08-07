using System.Collections.Generic;
using UnityEngine;
using Carmone.Weapons;
using Carmone.Item;


[System.Serializable]
public class EnchantUIData
{
    public Weapon weapon;
    public ItemData itemData;

    public EnchantUIData(Weapon weapon, ItemData itemData)
    {
        this.weapon = weapon;
        this.itemData = itemData;
    }
}


public class EnchantUI : MonoBehaviour
{
    [Header("Item Slots")]
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private int maxItemSlots = 10;

    [Header("Enchant Slots")]
    [SerializeField] private Transform enchantSlotContainer;
    [SerializeField] private GameObject enchantSlotPrefab;
    [SerializeField] private int maxEnchantSlots = 2;

    private List<EnchantUIData> enchantUIDatas = new();
    private GameObject[] itemSlots;
    private GameObject[] enchantSlots;
    private int selectIndex;

    private void Awake()
    {
        InitializeSlots(itemSlotContainer, ref itemSlots, itemSlotPrefab, maxItemSlots);
        InitializeSlots(enchantSlotContainer, ref enchantSlots, enchantSlotPrefab, maxEnchantSlots);
    }

    private void InitializeSlots(Transform container, ref GameObject[] slots, GameObject prefab, int maxSlots)
    {
        // container.RemoveChildrens();
        slots = new GameObject[maxSlots];
        for (int i = 0; i < maxSlots; i++)
        {
            slots[i] = Instantiate(prefab, container);
            slots[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < enchantUIDatas.Count)
            {
                var itemSlot = itemSlots[i].GetComponent<ItemSlot>();
                var data = enchantUIDatas[i];
                int index = i;
                itemSlot.Bind(data.itemData.itemIcon, () => UpdateEnchantSlots(index));
                itemSlots[i].SetActive(true);
            }
            else
            {
                itemSlots[i].SetActive(false);
            }
        }
        UpdateEnchantSlots(0);
    }

    public void Show(List<EnchantUIData> enchantUIDatas)
    {
        this.enchantUIDatas.Clear();
        this.enchantUIDatas.AddRange(enchantUIDatas);
        gameObject.SetActive(true);
    }

    private void UpdateEnchantSlots(int index)
    {
        var data = enchantUIDatas[index];
        for (int i = 0; i < enchantSlots.Length; i++)
        {
            if (i < data.itemData.enhancedWeapons.Length)
            {
                var enchantSlot = enchantSlots[i].GetComponent<EnchantSlot>();
                var enhancement = data.itemData.enhancedWeapons[i];
                enchantSlot.Bind(
                    enhancement.enchantImage,
                    enhancement.enchantName,
                    enchantLevel: data.weapon.EnchantmentLevel,
                    enhancement.enchantDesc,
                    () => Enchant(data.weapon, enhancement)
                );
                enchantSlots[i].SetActive(true);
            }
            else
            {
                enchantSlots[i].SetActive(false);
            }
        }
    }

    public void Enchant(Weapon weapon, WeaponEnhancement enhancement)
    {
        weapon.EnchantWeapon(enhancement.bulletType, enhancement.enhancementEffects);
        gameObject.SetActive(false);
    }
}