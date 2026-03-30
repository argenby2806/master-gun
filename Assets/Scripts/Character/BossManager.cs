using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossManager : MonoBehaviour
{

    public static BossManager instance;

    int enemyCount;
    int BossSpawnAfterXEnemy = 3;
    public int StartingBossLifeAtLevel1 = 5;
    public int maxBossLife = 100;
    public int maxEnemyCount = 15;
    public GameObject bossSignCanvas;
    public GameObject[] bossGraphics;
    public Slider slider;
    public GameObject charLevelGameObject;
    private void Awake()
    {
        bossSignCanvas.SetActive(false);
        instance = this;
    }

    public void IncreaseEnemyCount()
    {
        enemyCount++;

    }
    private void Start()
    {
        slider.fillRect.gameObject.SetActive(false);
    }

    public int GetBossLife()
    {
        int bossLife = StartingBossLifeAtLevel1 + (ExpLevelManager.instance.level - 1 * 3);
        if (bossLife > maxBossLife) bossLife = maxBossLife;
        return bossLife;
    }

    public bool haveToSpawnBoss()
    {
        int enemyNeed = BossSpawnAfterXEnemy + ExpLevelManager.instance.level;
        if (enemyNeed > maxEnemyCount) enemyNeed = maxEnemyCount;
        int usedEnemyCount = enemyCount;
        if (usedEnemyCount == 1) usedEnemyCount = 0;
        slider.value = (float)(usedEnemyCount) / (enemyNeed - 1);

        if (slider.value <= 0)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
        else
        {
            slider.fillRect.gameObject.SetActive(true);
        }

        if (enemyCount >= enemyNeed)
        {
            enemyCount = 0;
            bossSignCanvas.SetActive(true);
            charLevelGameObject.SetActive(false);
            return true;
        }
        return false;
    }



    public GameObject GetRandomChar()
    {
        return bossGraphics[Random.Range(0, bossGraphics.Length)];
    }




}
