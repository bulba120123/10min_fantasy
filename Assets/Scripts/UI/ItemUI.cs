using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;
using Carmone.Item;


namespace Carmone.Item
{
    public class ItemUI : MonoBehaviour
    {
        Image icon;
        Text textLevel;
        Text textName;
        Text textDesc;
        public ItemBtnType itemBtnType;
        Button itemBtn;

        public void InitializeUI(ItemData data, int level)
        {
            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.itemIcon;

            Text[] texts = GetComponentsInChildren<Text>();
            textLevel = texts[0];
            textName = texts[1];
            textDesc = texts[2];
            textName.text = data.itemName;
            itemBtn = GetComponent<Button>();
            itemBtn.onClick.AddListener(() => GetComponent<ItemController>().OnClick(itemBtnType));

            UpdateUI(data, level);
            AvailableBtn();
        }

        public void CheckAvailable(ItemData data, int level)
        {
            AvailableBtn();

            if (data.itemType == ItemType.Heal)
                return;
            if (data.itemType == ItemType.Gear)
            {
                if (data.price > GameManager.instance.gold)
                {
                    DisavailableBtn();
                }
                if (level >= data.levelGearStat.Length)
                {
                    DisavailableBtn();
                }
            }
            else if (data.itemType == ItemType.Melee || data.itemType == ItemType.Range)
            {
                if (level >= data.levelStat.Length)
                {
                    DisavailableBtn();
                }
            }
        }

        public void CheckInteractable(ItemData data, int level)
        {
            if (level >= data.levelStat.Length)
            {
                GetComponent<Button>().interactable = false;
            }
        }

        public void UpdateUI(ItemData data, int level)
        {
            textLevel.text = "Lv." + (level + 1);
            switch (data.itemType)
            {
                case ItemType.Melee:
                case ItemType.Range:
                    var levelStat = data.levelStat[level];
                    var damage = levelStat.damage;
                    var count = levelStat.projectileCount;
                    textDesc.text = string.Format(data.itemDesc, damage * 100, count);
                    break;
                case ItemType.Gear:
                    float rate;
                    if (level < data.levelGearStat.Length)
                    {
                        rate = data.levelGearStat[level].value;
                        textDesc.text = string.Format(data.itemDesc, rate);
                    }
                    else
                    {
                        textDesc.text = "�ִ� ��ȭ";
                    }
                    break;
                case ItemType.Heal:
                    textDesc.text = string.Format(data.itemDesc);
                    break;
            }
        }

        public void AvailableBtn()
        {
            Image image = this.GetComponent<Image>();
            Image[] childImages = this.GetComponentsInChildren<Image>();
            image.color = Color.white;
            foreach (var childImage in childImages)
            {
                childImage.color = Color.white;
            }
            GetComponent<ItemController>().available = true;
        }

        public void DisavailableBtn()
        {
            Image image = this.GetComponent<Image>();
            Image[] childImages = this.GetComponentsInChildren<Image>();
            image.color = Color.gray;
            foreach (var childImage in childImages)
            {
                childImage.color = Color.gray;
            }
            GetComponent<ItemController>().available = false;
        }
    }
}