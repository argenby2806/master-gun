using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitsManager : MonoBehaviour
{

    public static CharacterHitsManager instance;
    public GameObject damageTextEffect;
    private void Awake()
    {
        instance = this;
    }
    public void KillCharacter(Character character)
    {


        Rigidbody rigid = character.Graphics.AddComponent<Rigidbody>();
        Vector3 force = Vector3.up * 5;
        force.x = Random.Range(-3, 3);
        rigid.AddForce(force, ForceMode.Impulse);
        rigid.AddTorque(Vector3.forward * 20, ForceMode.Impulse);
        GameObject gun = character.GunGraphics;

        Rigidbody2D rigidGun = gun.AddComponent<Rigidbody2D>();

        force = Vector3.up * 1;
        force.x = Random.Range(-2, 2);
        rigidGun.AddForce(force, ForceMode2D.Impulse);
        rigidGun.AddTorque(Random.Range(-3, 3), ForceMode2D.Impulse);
        Destroy(Character.enemy.gameObject, 5);

    }
    

    public void DamageEnemy(int damage)
    {
        if (Character.enemy.isDead()) return;

        Character.enemy.life -= damage;

        if (Character.enemy.isBoss)
        {
            Instantiate(damageTextEffect, Character.enemy.transform.position, Quaternion.identity);
            DamageText.instance.SetText(damage.ToString());
        }

        if (Character.enemy.life < 0)
        {

            KillCharacter(Character.enemy);
            Character.enemy.RemoveTagsFromPieces();
        }
        else
        {


        }

       
    }
}