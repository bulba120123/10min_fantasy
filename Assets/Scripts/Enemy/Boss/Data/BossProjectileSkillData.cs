using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossProjectileSkill", menuName = "Scriptble Object/BossProjectileSkillData")]
public class BossProjectileSkillData : BossSkillData
{
    // ����ü ����
    public int baseProjectileCount;
    // ����ü �ӵ�
    public float baseProjectileSpeed;
    // ����ü ���� ����
    public bool isProjectilePenetrating;
}

