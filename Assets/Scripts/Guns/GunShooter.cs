using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Animation recoilAnim;

    public float numberOfShots = 2;
    public float delayBetweenShots = 0.1f;
    public int bulletDamage = 3;
    public float ySpread = 0.1f;
    public SpriteRenderer line, tip,graphics;
    public void Shot()
    {
        StartCoroutine(ShotRoutine());
    }

    IEnumerator ShotRoutine()
    {


        try
        {
            GetComponent<AudioSource>().Play();
        }
        catch { }

        for (int i = 0; i < numberOfShots; i++)
        {

            recoilAnim.Play();
            GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            Vector3 direction = PlayerGun.instance.gunLine.transform.right;
            if (Character.player.isRightOriented()) direction = direction * -1;
            bullet.SetUp(bulletDamage, direction, ySpread, PlayerGun.instance.gunTip.transform);


            yield return new WaitForSeconds(delayBetweenShots);
        }

      

    }
}
