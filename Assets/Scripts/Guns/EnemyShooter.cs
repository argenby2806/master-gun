using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    public Transform EnemyGun;
    public Transform EnemyGunTip;
    public GameObject EnemyBullet;
    public GameObject GunShotParticle;
    public Animation ShooterAnim;


    public void ShootPlayer()
    {
        StartCoroutine(ShotThePlayer());

    }

    IEnumerator ShotThePlayer()
    {
        float lerper = 0;
        float lerperTime = 0.6f;


        // rotate the gun towards the player 

        Vector3 currentRight = EnemyGun.right;
        Vector3 targetRight = GetCharacterDirection();
        if (Character.enemy.isDead()) { yield return null; }
        else
        {
            while (lerper <= 1)
            {
                lerper += Time.deltaTime / lerperTime;
                EnemyGun.right = Vector3.Lerp(currentRight, targetRight, lerper);
                yield return new WaitForEndOfFrame();
            }
            ShooterAnim.Play();
            GameObject newGunShotParticle = (GameObject)Instantiate(GunShotParticle, EnemyGunTip.position, Quaternion.Euler(EnemyGun.eulerAngles));
            Destroy(newGunShotParticle, 2);

            // Spawn bullet and move directly to the player
            gameObject.GetComponent<AudioSource>().Play();

            GameObject newEnemyBullet = (GameObject)Instantiate(EnemyBullet, EnemyGunTip.position, Quaternion.Euler(EnemyGun.eulerAngles));
            Vector3 currentPos = newEnemyBullet.transform.position;
            lerperTime = 0.25f;
            lerper = 0;
            while (lerper <= 1)
            {
                lerper += Time.deltaTime / lerperTime;
                newEnemyBullet.transform.position = Vector3.Lerp(currentPos, Character.player.Graphics.transform.position, lerper);
                yield return new WaitForEndOfFrame();
            }
            Destroy(newEnemyBullet);

            // spawn the effect and kill the player
            if (Character.enemy.isDead()) { yield return null; }
            else
            {
                CharacterHitsManager.instance.KillCharacter(Character.player);
                BulletsManager.instance.OnBulletHitPlayer();
                GameEventsCollection.instance.GameFinished();
            }
        }

    }

    Vector3 GetCharacterDirection()
    {
        Vector3 target = Vector3.zero;
        if (Character.enemy.isRightOriented())
        {
            target = -(Character.player.transform.position - EnemyGun.position);

        }
        else
        {
            target = (Character.player.transform.position - EnemyGun.position);

        }
        return target;
    }



}

