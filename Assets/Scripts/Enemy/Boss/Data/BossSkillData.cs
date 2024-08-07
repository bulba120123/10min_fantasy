using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "BossSkill", menuName = "Scriptble Object/BossSkillData")]
public class BossSkillData : ScriptableObject
{
    // ���� �̸�
    public BossType bossType;
    // ��ų ��ų �̸�
    public BossSkillType bossSkillType;

    // ��ų ������
    public float baseDamage;

}

