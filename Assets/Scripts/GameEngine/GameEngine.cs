using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject coin;
    public static GameEngine instance;
    bool levelCompleted;
    public Vector2Int randomCoinOnWin = new Vector2Int(20, 40);

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Debug.Log(" S : Spawn Stairs");
        Debug.Log(" I : Increase Color of Stairs");
        Debug.Log(" B : Begin game");
        SetUpScene();
    }


    void SetUpScene()
    {
        StairsManager.spawner.SpawnNewStair(30);

        StairsManager.instance.spawnedStairs[0].EnableCollider();
        StairsManager.instance.spawnedStairs[1].EnableCollider();

        playerObject.SetActive(true);

    }

    public void OnBeginGame()
    {
        EnemySpawner.instance.SpawnEnemy();
        EnemyReady();
    }


    public void OnPlayerClimbing() // called by CharacterMovement.cs
    {
        StairsManager.instance.IncreaseStairsColorsByTick();
        PlayerGun.instance.ResetGun();
        PlayerStatusHandler.instance.ChangeStatus(PlayerStatusHandler.Status.climbing);
    }

    public void OnPlayerFinishedClimbing()
    {
        if (levelCompleted) return;


        if (Character.enemy.isDead())
        {
            EnemySpawner.instance.SpawnEnemy();
            if (Character.enemy.isBoss == false)
            {
                EnemyReady();
            } // this is called by bossSign in case of the boss
        }
        else
        {
            Debug.Log("boss not dead, continue");
            EnemyReady(); // calling this here to avoid the fact that it can activated before the player arrives ( the enemy can be faster )

        }




    }

    public void EnemyReady() // called by CharacterSpawn.cs
    {
        PlayerStatusHandler.instance.ChangeStatus(PlayerStatusHandler.Status.aiming);
        int currentStairs = Character.player.movement.currentStair;
        StairsManager.instance.spawnedStairs[currentStairs].EnableCollider();
        StairsManager.instance.spawnedStairs[currentStairs].collider.gameObject.layer = LayerMask.NameToLayer("OldStairs");
        BulletsManager.instance.ResetCheck();


    }

    public void OnCharacterShot() // called by PlayerGun.cs
    {
        PlayerStatusHandler.instance.ChangeStatus(PlayerStatusHandler.Status.shooting);
    }    

    public void OnPlayerMiss()
    {
        PlayerStatusHandler.instance.ChangeStatus(PlayerStatusHandler.Status.waitingDeath);
        Character.enemy.ShootPlayer();
    }

    public void OnEnemyHit(bool headshot = false)
    {
        int score = ExpLevelManager.instance.level;
        int damage = PlayerGun.instance.shooter.bulletDamage;
        if (headshot)
        {
            damage = damage * 2;
            score = score * 2;
        }

        CharacterHitsManager.instance.DamageEnemy(damage);

        GameEventsCollection.instance.IncreaseScore(score);
    }


    public void OnPlayerCompletedShots()
    {

        if(Character.enemy.isBoss && Character.enemy.isDead() && levelCompleted==false)
        {
         

            OnLevelCompleted(); // boss killed. Level Complete
            return;
        }


        PlayerStatusHandler.instance.ChangeStatus(PlayerStatusHandler.Status.standing);
        Character.player.movement.ClimbStairs();

        if (Character.enemy.isDead() == false && Character.enemy.isBoss) // /the boss will climb because is not dead
        {
            Character.enemy.movement.ClimbStairs();

        } 
    }

    void OnLevelCompleted()
    {
        levelCompleted = true;

        SoundsManager.instance.PlayCoinSound();
        int numberOfCoins = Random.Range(randomCoinOnWin.x, randomCoinOnWin.y);
        for (int i = 0; i < numberOfCoins; i++)
        {
            Instantiate(coin, Character.enemy.transform.position, Quaternion.identity);
        }

        GameEventsCollection.instance.GameFinished(false);
        ExpLevelManager.instance.level++;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StairsManager.spawner.SpawnNewStair();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            StairsManager.instance.IncreaseStairsColorsByTick();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnBeginGame();
        }

    }
}
