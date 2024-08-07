using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class PoolManager : SerializedMonoBehaviour
{
    [Obsolete] public Dictionary<PoolType, GameObject> objectPools = new Dictionary<PoolType, GameObject>();
    public Dictionary<PoolType, GameObject> prefabs = new Dictionary<PoolType, GameObject>();

    Dictionary<PoolType, List<GameObject>> pools;
    Dictionary<string, List<GameObject>> poolsString;

    public Dictionary<string, GameObject> bossPools = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> bossProjectilePools = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> enemyProjectilePools = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> weaponPools = new Dictionary<string, GameObject>();

    private void Awake()
    {
        pools = new Dictionary<PoolType, List<GameObject>>();
        poolsString = new Dictionary<string, List<GameObject>>();

        foreach (PoolType poolType in Enum.GetValues(typeof(PoolType)))
        {
            pools.Add(poolType, new List<GameObject>());
        }
        InitPoolsString(Array.ConvertAll((EnumTypes.BossType[])Enum.GetValues(typeof(BossType)), new Converter<BossType, string>((a) => a.ToString())));
        InitPoolsString(Array.ConvertAll((EnumTypes.BossSkillType[])Enum.GetValues(typeof(BossSkillType)), new Converter<BossSkillType, string>((a) => a.ToString())));
        InitPoolsString(Array.ConvertAll((EnumTypes.EnemyProjectileType[])Enum.GetValues(typeof(EnemyProjectileType)), new Converter<EnemyProjectileType, string>((a) => a.ToString())));
        InitPoolsString(Array.ConvertAll((EnumTypes.WeaponType[])Enum.GetValues(typeof(WeaponType)), new Converter<WeaponType, string>((a) => a.ToString())));
    }

    private void InitPoolsString(string[] stringList)
    {
        foreach (string poolType in stringList)
        {
            poolsString.Add(poolType, new List<GameObject>());
        }
    }
    
    public GameObject Get(PoolType poolType)
    {
        GameObject select = null;

        foreach (GameObject item in pools[poolType])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        try
        {
            if (!select)
            {
                select = Instantiate(objectPools[poolType], transform);
                pools[poolType].Add(select);
            }
        }
        catch (Exception)
        {
            Debug.LogError(string.Format("예외 발생: key={0}", poolType));
        }
        return select;
    }

    public GameObject Get(string poolType)
    {
        GameObject select = null;

        if (poolsString.ContainsKey(poolType))
        {
            foreach (GameObject item in poolsString[poolType])
            {
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }

        if (!select)
        {
            GameObject isGameObject =
                bossPools.ContainsKey(poolType) ? bossPools[poolType] :
                bossProjectilePools.ContainsKey(poolType) ? bossProjectilePools[poolType] :
                enemyProjectilePools.ContainsKey(poolType) ? enemyProjectilePools[poolType] :
                weaponPools.ContainsKey(poolType) ? weaponPools[poolType] :
                null;
            if (!isGameObject) Debug.LogError($"등록되지 않은 프리펩 {poolType}");
            select = Instantiate(isGameObject, transform);
            poolsString[poolType].Add(select);
        }
        return select;
    }
    public GameObject GetBossProjectile(string bossProjectileType)
    {
        GameObject select = null;

        foreach (GameObject item in poolsString[bossProjectileType])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(bossProjectilePools[bossProjectileType], transform);
            poolsString[bossProjectileType].Add(select);
        }
        return select;
    }


    public void Release(GameObject releaseTarget)
    {
        releaseTarget.SetActive(false);
    }
}
