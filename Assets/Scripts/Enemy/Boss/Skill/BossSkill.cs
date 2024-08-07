using EnumTypes;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Carmone;

public class BossSkill : MonoBehaviour
{
    public BaseBoss boss;
    public Player player;

    public BossSkillType bossSkillType;
    public float baseDamage;

    public bool isProjectile;
    public int baseProjectileCount;
    public float baseProjectileSpeed;
    public float baseProjectileCoolTime;
    public bool isProjectilePenetrating;

    public bool isHealthObject;
    public float maxHealth;

    public float timer;
    public float delay;

    private void Awake()
    {
        player = GameManager.instance.player;
        delay = 1f;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        timer = 0;
    }
    virtual public void Init(BaseBoss boss, BossSkillData data)
    {
        this.boss = boss;

        bossSkillType = data.bossSkillType;
        baseDamage = data.baseDamage;
    }
    virtual public void Init(BaseBoss boss, BossProjectileSkillData data)
    {
        this.boss = boss;

        bossSkillType = data.bossSkillType;
        baseDamage = data.baseDamage;

        baseProjectileCount = data.baseProjectileCount;
        baseProjectileSpeed = data.baseProjectileSpeed;
        isProjectilePenetrating = data.isProjectilePenetrating;
    }
    virtual public void Init(BaseBoss boss, BossCreateSkillData data)
    {
        this.boss = boss;

        bossSkillType = data.bossSkillType;
        baseDamage = data.baseDamage;

        baseProjectileCount = data.baseProjectileCount;
        baseProjectileCoolTime = data.baseProjectileCoolTime;

        isHealthObject = data.isHealthObject;
        maxHealth = data.maxHealth;
    }


    virtual public void SkillAction() { }


}