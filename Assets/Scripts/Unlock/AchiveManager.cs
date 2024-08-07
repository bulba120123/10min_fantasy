using System;
using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    [SerializeField] private NoticeUI noticeUI;

    public enum Achive { UnlockAchor, UnlockMage, UnlockPrist, UnlockElemental }
    Achive[] achives;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }

    }
    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    public void CheckAchive(CharacterData characterData)
    {
        if (PlayerPrefs.GetInt(characterData.ToString()) == 0)
        {
            PlayerPrefs.SetInt(characterData.name, 1);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
            noticeUI.SetNotice(characterData.sprite, $"{characterData.name}이/가 해금되었습니다.");
        }
    }

    private bool CheckAchiveCondition(Achive achive)
    {
        return achive switch
        {
            Achive.UnlockAchor => SaveData.OpenArcher == true,
            Achive.UnlockMage => SaveData.OpenMage == true,
            Achive.UnlockPrist => SaveData.OpenPriest == true,
            Achive.UnlockElemental => SaveData.OpenElemental == true,
            _ => false,
        };
    }
}
