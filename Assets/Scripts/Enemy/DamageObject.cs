using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class DamageObject : MonoBehaviour
{
    public float damage;
    public int per;

    public bool isTrigger = false;

    Rigidbody2D rigid;
    public DamageObjectType damageObjectType;

    public float Damage { get => damage; set => damage = value; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        UpdateObjectType(this.damageObjectType);
    }
    public void UpdateObjectType(DamageObjectType objectType)
    {
        this.damageObjectType = objectType;
        switch (this.damageObjectType)
        {
            case DamageObjectType.Destroyable:
                per = 1;
                break;
            case DamageObjectType.Persistent:
                per = -100;
                break;
        }
    }

    public void Init(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (per == -100)
                return;
            per--;
            Debug.Log("Hit Damage");
            if (per < 0)
            {
                GameManager.instance.pool.Release(gameObject);
                rigid.velocity = Vector2.zero;
            }
        }
    }
}
