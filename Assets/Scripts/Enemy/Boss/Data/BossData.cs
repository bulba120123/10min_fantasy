using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "Boss", menuName = "Scriptble Object/BossData")]
public class BossData : ScriptableObject
{
    [Tooltip("등장 스테이지")]
    public float spawnStage;
    [Tooltip("등장 시간")]
    public float spawnTime;
    // 보스 최대 체력
    public int health;
    // 보스 스피드
    public float speed;
    // 보스 데미지 (투사체 또는 무기 데미지가 아닌 충돌 데미지)
    public float damage;
    // 보스의 넉백 저항
    public float knockBackResistance;
    // 획득 경험치
    public int exp;
    // 획득 골드
    public int goldCount;
    // 보스 타입
    public BossType bossType;
}
