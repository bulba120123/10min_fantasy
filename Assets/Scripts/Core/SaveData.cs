using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    public static bool OpenArcher {
        get {
            return PlayerPrefs.GetInt("openArcher", 0) == 1;
        }
        set {
            PlayerPrefs.SetInt("openArcher", value ? 1 : 0);
        }
    }

    public static bool OpenWarrior {
        get {
            return PlayerPrefs.GetInt("openWarrior", 0) == 1;
        }
        set {
            PlayerPrefs.SetInt("openWarrior", value ? 1 : 0);
        }
    }
    public static bool OpenMage {
        get {
            return PlayerPrefs.GetInt("openMage", 0) == 1;
        }
        set {
            PlayerPrefs.SetInt("openMage", value ? 1 : 0);
        }
    }

    public static bool OpenPriest {
        get {
            return PlayerPrefs.GetInt("openPriest", 0) == 1;
        }
        set {
            PlayerPrefs.SetInt("openPriest", value ? 1 : 0);
        }
    }

    public static bool OpenElemental {
        get {
            return PlayerPrefs.GetInt("openElemental", 0) == 1;
        }
        set {
            PlayerPrefs.SetInt("openElemental", value ? 1 : 0);
        }
    }

    public static int KillMonsterCount {
        get {
            return PlayerPrefs.GetInt("killMonsterCount", 0);
        }
        set {
            PlayerPrefs.SetInt("killMonsterCount", value);
        }
    }

    public static int CollectGoldCount {
        get {
            return PlayerPrefs.GetInt("collectGoldCount", 0);
        }
        set {
            PlayerPrefs.SetInt("collectGoldCount", value);
        }
    }

    public static int DefeatBossCount {
        get {
            return PlayerPrefs.GetInt("defeatBossCount", 0);
        }
        set {
            PlayerPrefs.SetInt("defeatBossCount", value);
        }
    }

    public static int LevelUpCount {
        get {
            return PlayerPrefs.GetInt("levelUpCount", 0);
        }
        set {
            PlayerPrefs.SetInt("levelUpCount", value);
        }
    }
}
