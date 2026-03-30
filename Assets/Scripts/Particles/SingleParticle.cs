using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleParticle : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite.color = BulletsManager.instance.currentBloodColor;
    }

    public void AddForce()
    {
        rigid.AddForce(Random.onUnitSphere.normalized * 6, ForceMode2D.Impulse);
        rigid.AddForce(Vector3.up, ForceMode2D.Impulse);

    }



}
