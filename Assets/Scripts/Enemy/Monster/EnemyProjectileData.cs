using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "EnemyProjectile", menuName = "Scriptble Object/EnemyProjectileData")]
public class EnemyProjectileData : ScriptableObject
{
    [Header("# Main Info")]
    public EnemyType enemyType;
    public EnemyProjectileType enemyProjectileType;
    public string projectileName;

    [Header("# Level Info")]
    public float baseDamage;
    public int baseCount;
    public float speed;
    public float coolTime;

    [Header("# Weapon")]
    public GameObject projectile;

}
