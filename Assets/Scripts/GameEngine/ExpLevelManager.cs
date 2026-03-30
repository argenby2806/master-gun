using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpLevelManager : MonoBehaviour {

    public static ExpLevelManager instance;
    public Text levelText;

    private void Awake()

    {
        instance = this;
    }

    public  int level
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }

    private void Update()
    {
        levelText.text = "LEVEL " + level.ToString();
    }
}
