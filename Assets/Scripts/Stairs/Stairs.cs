using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {
 public   SpriteRenderer spriteRend;
    bool isActive = false;
    public Transform topPoint;
    public SpriteRenderer[] debugSprites;
    public Transform[] steps;
    public Color targetColor;
    public PolygonCollider2D collider;
    private void Update()
    {
        if (isActive)
        {
            if (targetColor != spriteRend.color )
            {
                spriteRend.color = Color.Lerp(spriteRend.color, targetColor, 1 * Time.deltaTime);
            }
        }
    }
    public void SetUp()
    {
        DecreaseOrderingLayer();
        DecreaseFade();
        HideDebugSprites();
        StairsManager.instance.spawnedStairs.Add(this);
        targetColor = spriteRend.color;
        isActive = true;
    }

    void DecreaseOrderingLayer()
    {
        spriteRend.sortingOrder = StairsManager.instance.OrderLayer;
        StairsManager.instance.DecreaseOrderingLayer();
    }

    public void EnableCollider()
    {
        collider.enabled = true;
    }

    void DecreaseFade()
    {
       spriteRend.color = StairsManager.instance.currentColor;
        StairsManager.instance.DecreaseCurrentColorByTick();
    }

    void HideDebugSprites()
    {
        foreach (SpriteRenderer sprite in debugSprites)
        {
            sprite.enabled = false;
        }
    }
}
