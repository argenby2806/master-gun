using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float damage;
    Vector3 direction;
    Vector3 orientation;
    float bulletSpeed = 10;
    public GameObject GunshotParticle;
    Transform gunTransform;
    public void SetUp(float dmg, Vector3 dir, float YSpread,Transform origin)
    {
        damage = dmg;
   
        gunTransform = origin;

        direction = dir;
        orientation = origin.eulerAngles;
        transform.position = origin.position;

        BulletsManager.instance.bullets.Add(this);

direction.y += (Random.Range(-YSpread, YSpread));

        GameObject newGunShotParticle = (GameObject)Instantiate(GunshotParticle, transform.position, Quaternion.Euler(orientation));
        Destroy(newGunShotParticle, 2);
    }


    private void Update()
    {

        transform.position += direction * Time.deltaTime*bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (removed) return;

        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (i != 0) break;

            if (collision.gameObject.tag == "EnemyBody")
            {
                BulletsManager.instance.OnBulletHitBody(this);
                Remove();
                return;
            }

            if (collision.gameObject.tag == "EnemyHead")
            {
                BulletsManager.instance.OnBulletHitHead(this);
                Remove();
                return;
            }

            if (collision.gameObject.tag == "Untagged")
            {
                BulletsManager.instance.OnBulletHitGround(this);
                Remove();
                return;
            }

            if (collision.gameObject.tag == "Bounds")
            {
                BulletsManager.instance.OnBulletHitGround(this, false);
                Remove();
                return;
            }


           
        }

    }
    bool removed;
    private void Remove()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
        removed = true;
    }

}
