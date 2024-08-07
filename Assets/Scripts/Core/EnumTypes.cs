using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumTypes
{
    public enum PoolType
    {
        None,
        Enemy,
        Spirit,
        Fireball,
        Sword,
        Arrow,
        SaintCirle,
        EnemyArrow,
        EnemyLaser,
        Gold,
        Town,
        Sword_1,
        Sword_2,
        Spirit_1,
        Spirit_2,
        Fireball_1,
        Fireball_2,
        Shield,
        Boss,
        Shield_1,
        TargetTownArrow
    }

    public enum EnemyType
    {
        UNDEAD_SOLDIER = 0,
        UNDEAD_ARCHER = 1,
        UNDEAD_WARRIOR = 2,
        UNDEAD_MAGE = 3,
        GHOUL = 4,
        DULLAHAN = 5
    }

    public enum BossType
    {
        UNDEAD_GENERAL,
        UNDEAD_DRAGON,
        UNDEAD_KING,
    }

    public enum ItemBtnType
    {
        LevelUp,
        Town
    }

    public enum WeaponType
    {
        NONE = 0,
        FIREBALL = PoolType.Fireball,
        ELEMENTAL = PoolType.Spirit,
        ARROW = PoolType.Arrow,
        SWORD = PoolType.Sword,
        AREA = PoolType.SaintCirle,
        SWORD_1 = PoolType.Sword_1,
        SWORD_2 = PoolType.Sword_2,
        ELEMENTAL_1 = PoolType.Spirit_1,
        ELEMENTAL_2 = PoolType.Spirit_2,
        FIREBALL_1 = PoolType.Fireball_1,
        FIREBALL_2 = PoolType.Fireball_2,
        SHIELD = PoolType.Shield,
        SHIELD_1 = PoolType.Shield_1,
    }
    public enum ItemType { Melee, Range, Gear, Heal }

    public enum HealthObjectType
    {
        ENEMY,
        BOSS,
        PROJECTILE
    }

    public enum DamageObjectType
    {
        Destroyable,
        Persistent
    }

    public enum GearType
    {
        NULL,
        DREAM_EGG,
        EARTH_STAR,
        TIME_MANIPULATION,
        FIRE_COAT_OF_ARMS,
        MASTER_SWORD,
        WOODEN_MASK
    }

    public enum EnemyProjectileType
    {
        ALLOW,
        LASER
    }

    public enum BossSkillType
    {
        GENERAL_ROTATE_SWORD,
        DRAGON_FIRE,
        DRAGON_BRESS,
        DRAGON_RUSH,
        KING_SWING_SWORD,
        KING_RUSH
    }

    public enum CharacterType
    {
        Archer,
        Warrior,
        Mage,
        Priest,
        Elemental
    }
}