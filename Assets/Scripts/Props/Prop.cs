using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {


    SpriteRenderer spriteRend;
    Stairs stair;
    private void Update()
    {
        spriteRend.color = stair.spriteRend.color;
    }
    public void Assign(Stairs sta, SpriteRenderer sprite)
    {
        
        spriteRend = sprite;
        stair = sta;
        spriteRend.sortingOrder = stair.spriteRend.sortingOrder;

    }
}
