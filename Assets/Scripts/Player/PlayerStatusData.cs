using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "ScriptableObjects/PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    public float maxHealth = 100f;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    public float defaultPlayerSpeed = 5f;
    public float defaultSpeedMultiply = 1f;
    public float gearSpeedMultiplyIncrease = 0f;
    public float defaultProjectileSpeedMultiply = 1f;
    public float gearProjectileSpeedMultiplyIncrease = 0f;
    public float defaultCoolTimeMultiply = 1f;
    public float gearCoolTimeMultiplyDecrease = 0f;
    public float defaultSizeMultiply = 1f;
    public float gearSizeMultiplyIncrease = 0f;
    public float defaultDamageMultiply = 1f;
    public float gearDamageMultiplyIncrease = 0;
    public int defaultProjectileCountIncrease = 0;
    public int gearProjectileCountIncrease = 0;
    public int defaultKnockback = 0;
    public int gearKnockbackIncrease = 0;
    public int defaultPenetration = 0;
    public int gearPenetrationIncrease = 0;
}