using System.Collections;
using System.Collections.Generic;
using Carmone.Item;
using Carmone.Weapons;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{
    public float defaultPlayerSpeed;
    public float playerSpeed;
    [SerializeField]
    private PlayerStatusData playerStatusData;
    [System.NonSerialized]
    public float defaultSpeedMultiply;
    [System.NonSerialized]
    public float gearSpeedMultiplyIncrease;

    [System.NonSerialized]
    public float defaultProjectileSpeedMultiply;
    [System.NonSerialized]
    public float gearProjectileSpeedMultiplyIncrease;

    [System.NonSerialized]
    public float defaultCoolTimeMultiply;
    [System.NonSerialized]
    public float gearCoolTimeMultiplyDecrease;

    [System.NonSerialized]
    public float defaultSizeMultiply;
    [System.NonSerialized]
    public float gearSizeMultiplyIncrease;

    [System.NonSerialized]
    public float defaultDamageMultiply;
    [System.NonSerialized]
    public float gearDamageMultiplyIncrease;

    [System.NonSerialized]
    public int defaultProjectileCountIncrease;
    [System.NonSerialized]
    public int gearProjectileCountIncrease;

    [System.NonSerialized]
    public int defaultKnockback;
    [System.NonSerialized]
    public int gearKnockbackIncrease;

    [System.NonSerialized]
    public int defaultPenetration;
    [System.NonSerialized]
    public int gearPenetrationIncrease;

    private CharacterData characterData;

    public void Init()
    {
        var characterData = GameManager.instance.characterData;
        this.characterData = characterData;
        ApplyStat(playerStatusData);
        Apply();
    }

    public void Apply()
    {
        playerSpeed = defaultPlayerSpeed * (defaultSpeedMultiply + gearSpeedMultiplyIncrease);
        var weapons = GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            weapon.ApplyStat();
            weapon.Setting();
        }
    }

    public void LevelUp(int level)
    {
        if (characterData == null)
            return;

        if (level % characterData.levelInterval == 0)
        {
            var passive = characterData.passive;
            switch (passive.effect)
            {
                case StatEffectType.DamageIncreasePercent:
                    gearDamageMultiplyIncrease += characterData.passive.value;
                    break;
                case StatEffectType.SizeIncreasePercent:
                    gearSizeMultiplyIncrease += characterData.passive.value;
                    break;
                case StatEffectType.ProjectileSpeedMultiplier:
                    gearProjectileSpeedMultiplyIncrease += characterData.passive.value;
                    break;
                case StatEffectType.PenetrationIncrease:
                    gearPenetrationIncrease += (int)characterData.passive.value;
                    break;
                case StatEffectType.NumberOfProjectiles:
                    gearProjectileCountIncrease += (int)characterData.passive.value;
                    break;
                case StatEffectType.HealthRecoveryIncreasePercent:
                    break;
                case StatEffectType.ProjectileCountMultiplier:
                    break;
            }
            var weapons = GetComponentsInChildren<Weapon>();
            foreach (Weapon weapon in weapons)
            {
                weapon.ApplyStat();
                weapon.Setting();
            }
        }
    }

    private void ApplyStat(PlayerStatusData data)
    {
        this.defaultPlayerSpeed = data.defaultPlayerSpeed;
        this.defaultSpeedMultiply = data.defaultSpeedMultiply;
        this.gearSpeedMultiplyIncrease = data.gearSpeedMultiplyIncrease;
        this.defaultProjectileSpeedMultiply = data.defaultProjectileSpeedMultiply;
        this.gearProjectileSpeedMultiplyIncrease = data.gearProjectileSpeedMultiplyIncrease;
        this.defaultCoolTimeMultiply = data.defaultCoolTimeMultiply;
        this.gearCoolTimeMultiplyDecrease = data.gearCoolTimeMultiplyDecrease;
        this.defaultSizeMultiply = data.defaultSizeMultiply;
        this.gearSizeMultiplyIncrease = data.gearSizeMultiplyIncrease;
        this.defaultDamageMultiply = data.defaultDamageMultiply;
        this.gearDamageMultiplyIncrease = data.gearDamageMultiplyIncrease;
        this.defaultProjectileCountIncrease = data.defaultProjectileCountIncrease;
        this.gearProjectileCountIncrease = data.gearProjectileCountIncrease;
        this.defaultKnockback = data.defaultKnockback;
        this.gearKnockbackIncrease = data.gearKnockbackIncrease;
        this.defaultPenetration = data.defaultPenetration;
        this.gearPenetrationIncrease = data.gearPenetrationIncrease;
    }
}
