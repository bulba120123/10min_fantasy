using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using Carmone.Item;

public class Gear : MonoBehaviour
{
    public ItemType type;
    public GearType gearType;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.gearType;
        transform.parent = GameManager.instance.player.transform;
        transform.position = Vector3.zero;

        // Property Set
        type = data.itemType;
        gearType = data.gearType;
        ApplyStat(data.levelGearStat[0]);
    }

    public void Levelup(GearStat gearStat)
    {
        ApplyStat(gearStat);
    }

    void ApplyStat(GearStat gearStat)
    {
        this.rate = gearStat.value;
        switch (gearType)
        {
            case GearType.TIME_MANIPULATION:
                GameManager.instance.player.playerStatus.gearCoolTimeMultiplyDecrease = gearStat.value;
                break;
            case GearType.EARTH_STAR:
                GameManager.instance.player.playerStatus.gearSpeedMultiplyIncrease = gearStat.value;
                break;
            case GearType.DREAM_EGG:
                GameManager.instance.player.playerStatus.gearProjectileCountIncrease = (int)gearStat.value;
                break;
            case GearType.FIRE_COAT_OF_ARMS:
                GameManager.instance.player.playerStatus.gearSizeMultiplyIncrease = gearStat.value;
                break;
            case GearType.MASTER_SWORD:
                GameManager.instance.player.playerStatus.gearDamageMultiplyIncrease = gearStat.value;
                break;
            case GearType.WOODEN_MASK:
                GameManager.instance.player.playerStatus.gearKnockbackIncrease = (int)gearStat.value;
                break;
        }
        GameManager.instance.player.playerStatus.Apply();
    }
}
