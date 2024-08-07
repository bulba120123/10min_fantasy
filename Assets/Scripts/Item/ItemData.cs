using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnumTypes;


namespace Carmone.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("# Main Info")]
        public ItemType itemType;
        public GearType gearType;
        public string itemName;
        [TextArea]
        public string itemDesc;
        public Sprite itemIcon;
        public int price;

        [Header("# Gear")]
        public GearStat[] levelGearStat;

        [Header("# Weapon")]
        public float baseDamage;
        [Obsolete("WeaponStat으로 대체")] public int baseCount;
        [Obsolete("WeaponStat으로 대체")] public float[] damages;
        [Obsolete("WeaponStat으로 대체")] public int[] counts;
        [Obsolete("weaponType으로 대체")] public GameObject projectile;
        public WeaponType weaponType;
        public Sprite hand;
        public WeaponEnhancement[] enhancedWeapons;
        public WeaponStat initStat;
        public WeaponStat[] levelStat;


    }

    [System.Serializable]
    public class GearEnhancement
    {
        public GearType gearType;
        public StatEffect[] enhancementEffects;
        // 기타 필요한 능력치 추가
    }

    [System.Serializable]
    public class WeaponEnhancement
    {
        public PoolType bulletType;
        public string enchantName;
        public Sprite enchantImage;
        public string enchantDesc;
        public StatEffect[] enhancementEffects;
        // 기타 필요한 능력치 추가
    }

    [System.Serializable]
    public class StatEffect
    {
        public StatEffectType effect;
        public float value;
    }

    public enum StatEffectType
    {
        DamageIncreasePercent,       // 데미지 증가 퍼센트
        SizeIncreasePercent,         // 크기 증가 퍼센트
        ProjectileSpeedMultiplier,   // 투사체 속도 배율
        PenetrationIncrease,         // 관통력 증가
        NumberOfProjectiles,         // 투사체 갯수 증가
        HealthRecoveryIncreasePercent, // 회복력 퍼센트 증가
        ProjectileCountMultiplier,
        CoolTimePercent,
        KnockbackIncrease,
        moveSpeedPercent,
        // 추가로 필요한 효과를 여기에 정의
    }

    [System.Serializable]
    public class WeaponStat
    {
        [Tooltip("공격력")]
        public float damage;
        [Tooltip("크기")]
        public float scale = 1;
        [Tooltip("공격속도")]
        public float attackSpeed = 1f;
        [Tooltip("투사체 속도")]
        public float projectileSpeed = 1f;
        [Tooltip("투사체 관통력")]
        public int penetration = 1;
        [Tooltip("투사체 개수")]
        public int projectileCount;
    }

    [System.Serializable]
    public class GearStat
    {
        public float value = 1f;
    }
}