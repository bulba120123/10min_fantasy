using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour
{
    [SerializeField] private Image noticeImage;
    [SerializeField] private Text noticeText;
    [SerializeField] private float waitTime;
    private WaitForSecondsRealtime wait;

    private void Awake()
    {
        wait = new WaitForSecondsRealtime(waitTime);
    }

    public void SetNotice(Sprite sprite, string text)
    {
        noticeImage.sprite = sprite;
        noticeText.text = text;
        gameObject.SetActive(true);
        StartCoroutine(NoticeRoutine());
    }

    IEnumerator NoticeRoutine()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;
        gameObject.SetActive(false);
    }
}
