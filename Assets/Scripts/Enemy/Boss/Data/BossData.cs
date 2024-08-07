using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "Boss", menuName = "Scriptble Object/BossData")]
public class BossData : ScriptableObject
{
    [Tooltip("���� ��������")]
    public float spawnStage;
    [Tooltip("���� �ð�")]
    public float spawnTime;
    // ���� �ִ� ü��
    public int health;
    // ���� ���ǵ�
    public float speed;
    // ���� ������ (����ü �Ǵ� ���� �������� �ƴ� �浹 ������)
    public float damage;
    // ������ �˹� ����
    public float knockBackResistance;
    // ȹ�� ����ġ
    public int exp;
    // ȹ�� ���
    public int goldCount;
    // ���� Ÿ��
    public BossType bossType;
}
