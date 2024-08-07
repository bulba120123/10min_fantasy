using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using System.Linq;
using Carmone.Item;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    ItemController[] items;


    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemController>(true);
        if(items.Length == 0)
            items = GetComponentsInChildren<ItemController>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(WeaponType weaponType)
    {
        var index = System.Array.FindIndex(items, items => items.WeaponType == weaponType);
        items[index].OnClick(ItemBtnType.LevelUp);
    }
    
    void Next()
    {
        foreach(ItemController item in items)
        {
            item.gameObject.SetActive(false);
        }
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;

        }

        for (int index = 0; index < ran.Length; index++)
        {
            ItemController ranItem = items[ran[index]];

            if (ranItem.level == ranItem.data.levelStat.Length)
            {
                // TODO 레벨 올릴 수 있는 상품들로 전환
                items[Random.Range(4, 5)].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
