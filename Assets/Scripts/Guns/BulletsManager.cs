using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour {
    // script to check the behaviour of the bullets
    public static BulletsManager instance;
    public List<Bullet> bullets = new List<Bullet>();
    public GameObject hitBulletPrefab;
    public GameObject hitCharacterPrefab;
    public Color currentBloodColor;
    
    bool alreadyAddedHeadshot = false;
    bool avoidGameOver;
    bool didHeadShot;

    private void Awake()
    {
        instance = this;
    }

    public void OnBulletHitHead(Bullet bullet)
    {
        bullets.Remove(bullet);
        didHeadShot = true;
        avoidGameOver = true;
        GameEngine.instance.OnEnemyHit(true);
        currentBloodColor = Color.white;
        SpawnBulletParticle(hitCharacterPrefab, bullet.transform.position);
        ScreenFlasher.instance.Flash();

        OnBulletsCompleted();
        HeadshotManager.instance.DoHeadshotEffect();
        SoundsManager.instance.PlayCoinSound();

        for (int i = 0; i < HeadshotManager.instance.headshotsCombo; i++)
        {
            Instantiate(GameEngine.instance.coin, bullet.transform.position, Quaternion.identity);
        }

    }

    public void OnBulletHitBody(Bullet bullet)
    {

        bullets.Remove(bullet);

        avoidGameOver = true;
        GameEngine.instance.OnEnemyHit();
        currentBloodColor = Character.enemy.graphicsProperty.bloodColor;
        SpawnBulletParticle(hitCharacterPrefab, bullet.transform.position);

        OnBulletsCompleted();

    }

    public void OnBulletHitPlayer()
    {
        currentBloodColor = Character.player.graphicsProperty.bloodColor;
        SpawnBulletParticle(hitCharacterPrefab, Character.player.Graphics.transform.position);

    }


    public void OnBulletHitGround(Bullet bullet, bool spawnParticle=true)
    {
        bullets.Remove(bullet);

        if (spawnParticle) SpawnBulletParticle(hitBulletPrefab, bullet.transform.position);

        OnBulletsCompleted();
    }



    void OnBulletsCompleted()
    {

        if (Character.enemy.isDead())
        { // if the enemy is dead you can skip the other bullet checks
            CheckHeadshot();

            GameEngine.instance.OnPlayerCompletedShots();
            return;
        }

        if (bullets.Count == 0)
        {
            if (avoidGameOver == false)
            {
                GameEngine.instance.OnPlayerMiss();
            }
            else
            {

                CheckHeadshot();

                GameEngine.instance.OnPlayerCompletedShots();
            }
        }

     
    }

    void CheckHeadshot()
    {
        if (didHeadShot)
        {
            if (alreadyAddedHeadshot == false)
            {
                HeadshotManager.instance.IncreaseHeadshotCount();
                alreadyAddedHeadshot = true;
            }
        }
        else
        {
            HeadshotManager.instance.ResetHeadshotCount();
        }
    }


    public void SpawnBulletParticle(GameObject particle, Vector3 pos)
    {
        GameObject newHitParticle = (GameObject)Instantiate(particle, pos, Quaternion.identity);
        Destroy(newHitParticle, 10);
    }

  
   
    public void ResetCheck()
    {
        avoidGameOver = false;
        didHeadShot = false;
        alreadyAddedHeadshot = false;
        bullets.Clear();
    }


  

}
