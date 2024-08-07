using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CharacterUI : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image image;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button selectButton;
    [SerializeField] private UnityEvent<CharacterData> onCharacterSelect;

    [Header("테스트용 변수")]
    [Tooltip("조건이 없이 테스트를 하고 싶을 때 체크해주세요.")]
    [SerializeField] private bool isTestEnable;

    void Start()
    {
        image.sprite = characterData.sprite;
        nameText.text = characterData.name;
        descriptionText.text = characterData.description;
        selectButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            var isEnable = IsEnableCharacter();
            if (isEnable)
            {
                onCharacterSelect.Invoke(characterData);
            }
            else
            {
                Debug.Log("캐릭터가 잠겨있습니다.");
            }
        });
        ChangeUI(IsEnableCharacter());
    }

    private bool IsEnableCharacter()
    {
        if (isTestEnable)
        {
            return true;
        }

        switch (characterData.characterType)
        {
            case EnumTypes.CharacterType.Archer:
                return SaveData.OpenArcher;
            case EnumTypes.CharacterType.Warrior:
                return SaveData.OpenWarrior;
            case EnumTypes.CharacterType.Mage:
                return SaveData.OpenMage;
            case EnumTypes.CharacterType.Priest:
                return SaveData.OpenPriest;
            case EnumTypes.CharacterType.Elemental:
                return SaveData.OpenElemental;
            default:
                return false;
        }
    }

    private void ChangeUI(bool isEnable)
    {
        selectButton.interactable = isEnable;
        image.color = isEnable ? Color.white : Color.black;
        nameText.text = isEnable ? nameText.text : "???";
        descriptionText.text = isEnable ? descriptionText.text : "미해금";
    }
}