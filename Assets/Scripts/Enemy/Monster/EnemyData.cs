using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptble Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("등장 스테이지")]
    public float spawnStage;
    [Tooltip("등장 시간")]
    public float spawnTime;
    [Tooltip("재등장 텀")]
    public float spawnDelay;
    // 몬스터 최대 체력
    public int health;
    // 몬스터 스피드
    public float speed;
    // 몬스터 가속도 (현재 듀라한 전용)
    public float acceleration;
    // 몬스터 데미지
    public float damage;
    // 몬스터의 넉백 저항
    public float knockBackResistance;
    // 맵에 존재하는 몬스터 최대 몬스터 수 제한
    public int maxCount;
    // 유령 효과 (몬스터가 플레이어를 제외한 레이어의 오브젝트 무시)
    public bool isGhost;
    // 획득 경험치
    public int exp;
    // 획득 골드
    public int goldCount;
    // 몬스터 타입
    public EnemyType enemyType;
}
