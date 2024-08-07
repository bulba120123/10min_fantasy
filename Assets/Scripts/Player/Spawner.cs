using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using Carmone;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public BossData[] bossDatas;
    public EnemyData[] enemyDatas;
    private WaitForSeconds gameTimeTerm = new WaitForSeconds(0.1f);

    // �� ����
    public Dictionary<EnemyType, int> enemyCountDic;

    private void Awake()
    {
        spawnPoints=GetComponentsInChildren<Transform>();
        enemyCountDic = new Dictionary<EnemyType, int>() { { EnemyType.UNDEAD_SOLDIER, 1 }, { EnemyType.UNDEAD_ARCHER, 1 }, { EnemyType.UNDEAD_WARRIOR, 1 }, { EnemyType.UNDEAD_MAGE, 1 }, { EnemyType.GHOUL, 1 }, { EnemyType.DULLAHAN, 1 } };
        gameTimeTerm = new WaitForSeconds(0.1f);
    }

    //void Update()
    //{
    //    if (!GameManager.instance.isLive)
    //        return;
    //    timer += Time.deltaTime;
    //    level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnDatas.Length - 1);

    //    if (timer > (spawnDatas[level].spawnTime))
    //    {
    //        timer = 0;
    //        if(!enemyCountDic.ContainsKey(spawnDatas[level].enemyType) || enemyCountDic[spawnDatas[level].enemyType] <= spawnDatas[level].maxCount)
    //        {
    //            Spawn();
    //        }
    //    }

    //}

    public void SpawnEachEnemies()
    {
        if (!GameManager.instance.isLive)
            return;
        foreach (var enemyData in enemyDatas)
        {
            if (MathUtil.MyApproximation(GameManager.instance.spawnTime, enemyData.spawnTime, 0.001f) && GameManager.instance.stage== enemyData.spawnStage)
            {
                StartCoroutine(SpawnEnemyRoutine(enemyData));
            }
            else
            {
            }
        }
        foreach(var bossData in bossDatas)
        {
            if (MathUtil.MyApproximation(GameManager.instance.spawnTime, bossData.spawnTime, 0.001f) && GameManager.instance.stage== bossData.spawnStage)
            {
                StartCoroutine(SpawnBossRoutine(bossData));
            }
        }
    }

    public IEnumerator SpawnEnemyRoutine(EnemyData enemyData)
    {
        float timer = 0;
        do
        {
            if(timer == 0 || (MathUtil.MyApproximation(timer, enemyData.spawnDelay, 0.001f) && GameManager.instance.stage == enemyData.spawnStage && (!enemyCountDic.ContainsKey(enemyData.enemyType) || enemyCountDic[enemyData.enemyType] <= enemyData.maxCount)))
            {
                timer = 0;
                SpawnEnemy(enemyData);

            }
            yield return gameTimeTerm;
            timer += 0.1f;
        }
        while (true);
    }

    public IEnumerator SpawnBossRoutine(BossData bossData)
    {
        yield return null;
        SpawnBoss(bossData);
    }


    void SpawnEnemy(EnemyData enemyData)
    {
        // Debug.Log(enemyData.enemyType.ToString() + " 적 생성!");
        GameObject enemy = GameManager.instance.pool.Get(PoolType.Enemy);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        enemy.GetComponent<Enemy>().Init(enemyData);
        IncreaseEnemyCount(enemyData.enemyType);
    }

    public void MoveEnemy(GameObject enemy)
    {
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }

    void SpawnBoss(BossData bossData)
    {
        GameObject boss = GameManager.instance.pool.Get(bossData.bossType.ToString());
        boss.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        boss.GetComponent<BaseBoss>().Init(bossData);
    }

    public void IncreaseEnemyCount(EnemyType enemyType)
    {
        if (enemyCountDic.ContainsKey(enemyType))
        {
            enemyCountDic[enemyType]++;
        }
        else
        {
            enemyCountDic.Add(enemyType, 1);
        }
        //Debug.Log(enemyType + " : " + enemyCountDic[enemyType]);
    }

    public void DecreaseEnemyCount(EnemyType enemyType)
    {
        if (enemyCountDic.ContainsKey(enemyType))
        {
            enemyCountDic[enemyType]--;
        }
        //Debug.Log(enemyType + " : " + enemyCountDic[enemyType]);
    }
}