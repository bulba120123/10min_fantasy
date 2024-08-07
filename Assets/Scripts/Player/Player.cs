using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static readonly Vector2 DEFAULT_DIRECTION = Vector2.right; 
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public Vector2 CurrentDirection { get; private set; } = new Vector2(0, 0);
    public PlayerStatus playerStatus;
    public float collisionStayDelay;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    private WaitForSeconds collisonStayWait = new WaitForSeconds(0.5f);

    private Dictionary<int, Collision2D> currentCollisions = new Dictionary<int, Collision2D>();

    private RuntimeAnimatorController currentAnimCon;

    void Awake()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
        playerStatus = GetComponent<PlayerStatus>();

        collisionStayDelay = collisionStayDelay == 0 ? 0.5f : collisionStayDelay;
        collisonStayWait = new WaitForSeconds(collisionStayDelay);
    }

    private void OnEnable()
    {
        playerStatus.Init();
        if (currentAnimCon == null)
        {
            anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
        }
        else
        {
            anim.runtimeAnimatorController = currentAnimCon;
        }

        StartCoroutine(CoCollisionStay2D());
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec = inputVec.normalized * playerStatus.playerSpeed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
    }

    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        currentAnimCon = controller;
        if (anim != null)
        {
            anim.runtimeAnimatorController = currentAnimCon;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }

        // 방향 설정
        if (inputVec != Vector2.zero)
        {
            CurrentDirection = inputVec.normalized;
            // Debug.Log(CurrentDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isLive)
            return;
        if (!gameObject.CompareTag("Player"))
            return;

        switch (collision.transform.tag)
        {
            case "EnemyBullet":
            case "BossBullet":
            case "Boss":
                GameManager.instance.health -= collision.transform.GetComponent<DamageObject>().Damage;
                // Debug.Log("OnTriggerEnter2D");
                // Debug.Log(GameManager.instance.health);
                // Debug.Log(collision.transform.GetComponent<DamageObject>().Damage);
                if (GameManager.instance.health > 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
                }
                else
                {
                    Dying();
                }
                break;
            case "Town":
                GameManager.instance.townManager.DisableTown(collision.gameObject);
                GameManager.instance.uiTown.Show();
                break;
            case "Gold":
                collision.transform.GetComponent<Gold>().GetGold();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;
        if (!gameObject.CompareTag("Player"))
            return;

        switch (collision.transform.tag)
        {
            case "Enemy":
            case "Boss":
                GameManager.instance.health -= collision.transform.GetComponent<DamageObject>().Damage;
                // Debug.Log("OnCollisionEnter2D");
                // Debug.Log(GameManager.instance.health);
                // Debug.Log(collision.transform.GetComponent<DamageObject>().Damage);
                if (GameManager.instance.health > 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
                }
                else
                {
                    Dying();
                }
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        int instanceId = collision.transform.GetInstanceID();
        if (!currentCollisions.ContainsKey(instanceId))
        {
            currentCollisions.Add(instanceId, collision);
        }
    }

    private IEnumerator CoCollisionStay2D()
    {
        while (true)
        {
            yield return collisonStayWait;

            if (!GameManager.instance.isLive)
                continue;
            if (!gameObject.CompareTag("Player"))
                continue;
            List<Collision2D> collisionValuesList = new List<Collision2D>(currentCollisions.Values);

            foreach (Collision2D collision in collisionValuesList)
            {
                // Debug.Log(collision.gameObject.name);
                if (collision != null)
                {
                    CollEffect(collision);
                }
            }
            currentCollisions.Clear();

        }
    }

    public void CollEffect(Collision2D collision)
    {

        switch (collision.transform.tag)
        {
            case "Enemy":
            case "Boss":
                GameManager.instance.health -= collision.transform.GetComponent<DamageObject>().Damage;
                // Debug.Log("CoCollisionStay2D");
                // Debug.Log(GameManager.instance.health);
                // Debug.Log(collision.transform.GetComponent<DamageObject>().Damage);
                if (GameManager.instance.health > 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
                }
                else
                {
                    Dying();
                }
                break;
        }
    }

    private void Dying()
    {

        for (int index = 2; index < transform.childCount; index++)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }

        anim.SetTrigger("Dead");
        GameManager.instance.GameOver();
    }

}
