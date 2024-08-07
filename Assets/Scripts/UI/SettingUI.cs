using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private Text sfxText;
    [SerializeField]
    private Text bgmText;

    [SerializeField]
    private UnityEvent<bool> onBGMChanged;

    [SerializeField]
    private UnityEvent<bool> onSFXChanged;


    private bool sfxValue = false;
    private bool bgmValue = false;


    private void OnEnable()
    {
        // 이전에 저장한 설정값을 로드
        sfxValue = PlayerSettings.SFXOn;
        bgmValue = PlayerSettings.BGMOn;

        UpdateSFXValue(sfxValue);
        UpdateBGMValue(bgmValue);
    }

    public void ChangeBGMValue()
    {
        bgmValue = !bgmValue;
        UpdateBGMValue(bgmValue);
    }

    public void ChangeSFXValue()
    {
        sfxValue = !sfxValue;
        UpdateSFXValue(sfxValue);
    }

    private void UpdateSFXValue(bool value)
    {
        sfxText.text = "효과음\n" + (value ? "ON" : "OFF");
    }

    private void UpdateBGMValue(bool value)
    {
        bgmText.text = "배경음악\n" + (value ? "ON" : "OFF");
    }

    public void UpdateSetting()
    {
        // 체크박스 상태에 따라 사운드와 음악 켜고 끄기
        PlayerSettings.BGMOn = bgmValue;
        PlayerSettings.SFXOn = sfxValue;

        onBGMChanged?.Invoke(bgmValue);
        onSFXChanged?.Invoke(sfxValue);
    }
}
