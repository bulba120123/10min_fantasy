using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptble Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("���� ��������")]
    public float spawnStage;
    [Tooltip("���� �ð�")]
    public float spawnTime;
    [Tooltip("����� ��")]
    public float spawnDelay;
    // ���� �ִ� ü��
    public int health;
    // ���� ���ǵ�
    public float speed;
    // ���� ���ӵ� (���� ����� ����)
    public float acceleration;
    // ���� ������
    public float damage;
    // ������ �˹� ����
    public float knockBackResistance;
    // �ʿ� �����ϴ� ���� �ִ� ���� �� ����
    public int maxCount;
    // ���� ȿ�� (���Ͱ� �÷��̾ ������ ���̾��� ������Ʈ ����)
    public bool isGhost;
    // ȹ�� ����ġ
    public int exp;
    // ȹ�� ���
    public int goldCount;
    // ���� Ÿ��
    public EnemyType enemyType;
}
