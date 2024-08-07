using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossCreateSkill", menuName = "Scriptble Object/BossCreateSkillData")]
public class BossCreateSkillData : BossSkillData
{
    // 투사체 개수
    public int baseProjectileCount;
    // 발사 쿨타임
    public float baseProjectileCoolTime;

    // HealthObject 여부
    public bool isHealthObject;
    // 투사체 체력
    public float maxHealth;
}

