using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    public WaitForSeconds waitDeleteTerm;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        waitDeleteTerm = new WaitForSeconds(1f);
    }

    private void OnEnable()
    {
        StartCoroutine(CoCheckDestroy());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffX = GameManager.instance.playerPos.x - myPos.x;
                float diffY = GameManager.instance.playerPos.y - myPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 120);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 120);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = GameManager.instance.playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    transform.Translate(ran + dist * 2);
                    transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f); // Z값을 0.5로 설정
                }
                break;
            case "Boss":
                if (coll.enabled)
                {
                    Vector3 dist = GameManager.instance.playerPosWithZConvert - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    transform.Translate(ran + dist * 2);
                    transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f); // Z값을 0.5로 설정
                }
                break;
        }

    }

    IEnumerator CoCheckDestroy()
    {
        if (!transform.CompareTag("Enemy"))
            yield break;
        while (true)
        {
            yield return waitDeleteTerm;
            Vector3 playerPos = GameManager.instance.playerPos;
            Vector3 myPos = transform.position;
            Vector3 dist = playerPos - myPos;
            // Debug.Log(dist.magnitude);
            if(dist.magnitude > 40  )
            {
                GetComponent<Enemy>().Reposition();
                yield break;
            }
        }
    }
}
