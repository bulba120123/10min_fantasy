using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossProjectileSkill", menuName = "Scriptble Object/BossProjectileSkillData")]
public class BossProjectileSkillData : BossSkillData
{
    // 투사체 개수
    public int baseProjectileCount;
    // 투사체 속도
    public float baseProjectileSpeed;
    // 투사체 관통 여부
    public bool isProjectilePenetrating;
}

