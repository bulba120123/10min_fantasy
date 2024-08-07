using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;
using Carmone.Item;

public class TownUI : MonoBehaviour
{
    public int townNum;
    RectTransform rect;
    public ItemController[] items;
    public RectTransform contentRect;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (items.Length == 0)
            items = GetComponentsInChildren<ItemController>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        contentRect.localPosition = new Vector2(0, 0);
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        // GameManager.instance.targetTownArrow.UnSetArrow();
        GameManager.instance.townManager.CloseTownAuto();
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].OnClick(ItemBtnType.Town);
    }
    public void Next()
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }
        
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
        }
    }
}
