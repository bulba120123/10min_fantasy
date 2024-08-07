using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Bind(Sprite sprite, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        image.sprite = sprite;
        button.onClick.AddListener(action);
    }
}
