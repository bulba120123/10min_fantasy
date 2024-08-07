using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "BossSkill", menuName = "Scriptble Object/BossSkillData")]
public class BossSkillData : ScriptableObject
{
    // 보스 이름
    public BossType bossType;
    // 스킬 스킬 이름
    public BossSkillType bossSkillType;

    // 스킬 데미지
    public float baseDamage;

}

