using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class EnemyProjectile : MonoBehaviour
{
    //프리펩 친구들은 변수 초기화를 하는게 좋다
    public int per;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(CoCheckDestroy());
    }


    public void Init(float damage, float speed, int per, Vector3 dir)
    {
        GetComponent<DamageObject>().Init(damage);
        this.per = per;

        if (per >= 0)
        {
            rigid.velocity = dir * speed;
        }

    }

    //관통
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || per == -100)
            return;
        per--;
        if (per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
        {
            return;
        }
        gameObject.SetActive(false);
    }

    IEnumerator CoCheckDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 playerPos = GameManager.instance.playerPos;
            Vector3 myPos = transform.position;
            Vector3 dist = playerPos - myPos;
            // Debug.Log(dist.magnitude);
            if (dist.magnitude > 40)
            {
                gameObject.SetActive(false);
                yield break;
            }
        }

    }

}
