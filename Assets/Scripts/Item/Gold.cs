using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().SetInteger("state", Random.Range(1, 3));
    }

    public void GetGold()
    {
        GameManager.instance.UpdateGold(1);
        GameManager.instance.pool.Release(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
