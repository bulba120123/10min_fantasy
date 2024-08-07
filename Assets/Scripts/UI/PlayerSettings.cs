using UnityEngine;

public static class PlayerSettings
{
    public static bool BGMOn
    {
        get
        {
            return PlayerPrefs.GetInt("BGMOn", 1) == 1;
        }

        set
        {
            PlayerPrefs.SetInt("BGMOn", value ? 1 : 0);
        }
    }

    public static bool SFXOn
    {
        get
        {
            return PlayerPrefs.GetInt("SFXOn", 1) == 1;
        }

        set
        {
            PlayerPrefs.SetInt("SFXOn", value ? 1 : 0);
        }
    }
}
