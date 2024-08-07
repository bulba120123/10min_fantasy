using Carmone.Item;
using System.Collections;
using Carmone.Weapons;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using EnumTypes;
using System.Linq;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using Carmone;

public class GameManager : SerializedMonoBehaviour
{

    public static GameManager instance;
    [SerializeField] private AchiveManager achiveManager;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;
    public bool isGameStart = false;
    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    public int gold = 0;
    public Spawner spawner;
    private WaitForSeconds gameTimeTerm;
    private bool gamePause;
    public bool GamePause
    {
        get { return gamePause; }
        set
        {
            gamePause = value;
            if (gamePause)
            {
                Time.timeScale = 0;
                AudioManager.instance.PauseBgm(true);
            }
            else
            {
                Time.timeScale = 1;
                AudioManager.instance.PauseBgm(false);
            }
        }
    }
    public int stage = 0;
    public float spawnTime = 0;

    [Header("# Game Object")]
    public Player player;
    [SerializeField]
    private PlayerStatusData playerStatusData;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public TownUI uiTown;
    public Result uiResult;
    public GameObject enemyCleaner;
    public Follow uiHealth;
    public GameObject townObj;
    public Transform uiJoy;
    public Transform uiHUD;
    public EnchantUI uiEnchant;
    public CharacterData characterData;
    public List<GameObject> mapObjects = new List<GameObject>();
    public Vector3 playerPos;
    public Vector3 playerPosWithZConvert;
    [Header("# 업적")]
    private ObjectiveManager objectiveManager = new ObjectiveManager();
    [SerializeField]
    public TownManager townManager = new TownManager();

    public UnityEvent killMonsterEvent;

    public AdReword adReword;
    public UnityEvent killBossEvent;
    public UnityEvent<int> collectGoldEvent;
    public UnityEvent levelUpEvent;
    public UnityEvent defeatBossEvent;

    void Awake()
    {
        instance = this;
        isGameStart = false;
        Application.targetFrameRate = 60;
        if (playerStatusData != null)
        {
            maxHealth = playerStatusData.maxHealth;
            nextExp = playerStatusData.nextExp;
        }
    }
    [SerializeField][Tooltip("kill 몬스터 달성 수")] private int killMonsterCountGoal = 10;
    [SerializeField][Tooltip("gold 획득량 달성 수")] private int collectGoldCountGoal = 3000;
    [SerializeField][Tooltip("보스 처치 달성 수")] private int defeatBossCountGoal = 1;
    [SerializeField] private Dictionary<CharacterType, CharacterData> characterDatas;
    [SerializeField][Tooltip("레벨업 달성 수")] private int levelUpCountGoal = 1000;

    void Start()
    {
        SaveData.OpenWarrior = true;
        objectiveManager.AddUnlockCondition(new HuntMonstersObjective(killMonsterCountGoal), () =>
        {
            SaveData.OpenArcher = true;
            achiveManager.CheckAchive(characterDatas[CharacterType.Archer]);
        });
        objectiveManager.AddUnlockCondition(new CollectGoldObjective(collectGoldCountGoal), () =>
        {
            SaveData.OpenMage = true;
            achiveManager.CheckAchive(characterDatas[CharacterType.Mage]);
        });
        objectiveManager.AddUnlockCondition(new DefeatBossObjective(), () =>
        {
            SaveData.OpenPriest = true;
            achiveManager.CheckAchive(characterDatas[CharacterType.Priest]);
        });
        objectiveManager.AddUnlockCondition(new LevelUpObjective(levelUpCountGoal), () =>
        {
            SaveData.OpenElemental = true;
            
        });
        killMonsterEvent.AddListener(() =>
        {
            SaveData.KillMonsterCount += 1;
            objectiveManager.UpdateMonsterKillObjectives(1);
        });
        killBossEvent.AddListener(() =>
        {
            SaveData.DefeatBossCount += 1;
            objectiveManager.UpdateDefeatBossObjectives(1);
        });
        collectGoldEvent.AddListener((_gold) =>
        {
            SaveData.CollectGoldCount = _gold;
            objectiveManager.UpdateCollectGoldObjectives(_gold);
        });
        levelUpEvent.AddListener(() =>
        {
            SaveData.LevelUpCount += 1;
            objectiveManager.UpdateLevelUpObjectives(1);
        });
    }

    private void FixedUpdate()
    {
        playerPos = player.transform.position;
        playerPosWithZConvert = player.transform.position;
        playerPosWithZConvert.z = -0.3f;
    }


    public void GameStart(CharacterData characterData)
    {
        this.characterData = characterData;
        playerId = characterData.id;
        player.SetAnimatorController(characterData.animator);
        health = maxHealth;
        gameTimeTerm = new WaitForSeconds(0.1f);

        player.gameObject.SetActive(true);
        playerPos = player.transform.position;

        townManager.OpenAllTown();

        uiLevelUp.Select(characterData.startWeapon);
        uiHealth.gameObject.SetActive(true);
        uiHUD.gameObject.SetActive(true);

        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        StartCoroutine(GameStartRoutine());
        isGameStart = true;
    }

    IEnumerator GameStartRoutine()
    {
        while (true)
        {
            yield return gameTimeTerm;
            if (!isLive)
                yield break;

            spawner.SpawnEachEnemies();
            gameTime += 0.1f;
            spawnTime += 0.1f;
            // Debug.Log(gameTime);
            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
                GameVictory();
            }
        }
    }

    public void ToggleGamePause()
    {
        GamePause = !GamePause;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());

    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        //AdManager.instance.adReword.ShowAd();
        // AdReword.cs �� OnUnityAdsShowComplete �� �̵� -> �Ϸ��� ������ �̵��� ���� ���� ��� ���� ū ���� ������ ����. ���������� �ε��� ��������
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }


    public void GetExp(int addExp)
    {
        if (!isLive)
            return;

        exp += addExp;
        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            var unusedExp = exp - nextExp[Mathf.Min(level, nextExp.Length - 1)];
            level++;
            exp = unusedExp;
            uiLevelUp.Show();
            player.GetComponent<PlayerStatus>().LevelUp(level);
            levelUpEvent.Invoke();
            if (level % 10 == 0)
            {
                ShowEnchantUI();
            }
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
        uiHUD.localScale = Vector3.zero;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
        uiHUD.localScale = Vector3.one;
    }

    public void NextStage()
    {
        mapObjects[stage].SetActive(false);
        stage++;
        spawnTime = 0;
        Debug.Log("stage: " + stage);
        mapObjects[stage].SetActive(true);
    }

    private void ShowEnchantUI()
    {
        var weapons = player.gameObject.GetComponentsInChildren<Weapon>();
        var enchantUIDatas = weapons.Select(weapon =>
            new EnchantUIData(
                weapon,
                ItemDataProvider.Instance.GetItemData(weapon.weaponType)
            )
        ).ToList();
        uiEnchant.Show(enchantUIDatas);
    }

    public void UpdateGold(int gold)
    {
        this.gold += gold;
        if (gold > 0)
        {
            collectGoldEvent.Invoke(gold);
        }
    }
}
