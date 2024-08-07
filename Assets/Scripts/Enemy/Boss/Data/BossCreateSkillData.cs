using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossCreateSkill", menuName = "Scriptble Object/BossCreateSkillData")]
public class BossCreateSkillData : BossSkillData
{
    // ����ü ����
    public int baseProjectileCount;
    // �߻� ��Ÿ��
    public float baseProjectileCoolTime;

    // HealthObject ����
    public bool isHealthObject;
    // ����ü ü��
    public float maxHealth;
}

