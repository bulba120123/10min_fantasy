using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnchantSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text enchantLevel;
    [SerializeField] private Text desciption;
    private Button button;

    public void Awake()
    {
        this.button = GetComponent<Button>();
    }

    public void Bind(Sprite image, string weaponName, int enchantLevel, string desciption, UnityAction onClick)
    {
        this.button.onClick.RemoveAllListeners();

        this.image.sprite = image;
        this.weaponName.text = weaponName;
        this.enchantLevel.text = string.Format("lv.{0}", enchantLevel);
        this.desciption.text = desciption;
        this.button.onClick.AddListener(onClick);
    }
}
