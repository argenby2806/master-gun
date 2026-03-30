using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner instance;
    public GameObject CharPrefab;

    private void Awake()
    {
        instance = this;
    }


    void Start () {
        Debug.Log(" M : Spawn Enemy");

    }

    void Update () {
		if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnEnemy();
        }
	}
    public void SpawnEnemy()
    {

        BossManager.instance.IncreaseEnemyCount();

        if (BossManager.instance.haveToSpawnBoss() == false)
        {

            SpawnNormalEnemy();
            Character.enemy.BecomeNormal();

        }
        else
        {
            SpawnNormalEnemy();
            Character.enemy.BecomeBoss();
        }


    }

    GameObject SpawnNormalEnemy()
    {


        GameObject newEnemy = (GameObject)Instantiate(CharPrefab);

        int stairIndex = Character.player.movement.currentStair;

        Stairs stair = StairsManager.instance.spawnedStairs[stairIndex];
        newEnemy.transform.position = stair.topPoint.position;

        Vector3 scale = newEnemy.transform.localScale;
        if (!Character.player.isRightOriented()) scale.x = scale.x * -1;
        newEnemy.transform.localScale = scale;
        return newEnemy;
    }
}
