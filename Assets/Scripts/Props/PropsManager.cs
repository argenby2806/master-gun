using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour
{


    public Sprite[] PropsList;
    public static PropsManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
    }

    Sprite randomProp()
    {
        return PropsList[Random.Range(0, PropsList.Length)];
    }


    public void SpawnPropOnStair(Stairs stair)
    {
        Vector3 basePos = stair.topPoint.position;

        GameObject newProp = new GameObject();
        SpriteRenderer sprite = newProp.AddComponent<SpriteRenderer>();
        sprite.sprite = randomProp();

        newProp.transform.position = basePos + Vector3.up * (sprite.sprite.bounds.size.y / 2);

        if (newProp.transform.position.x > 0)
        {
            newProp.transform.position += Vector3.right * sprite.sprite.bounds.size.x / 2;
        }
        else
        {
            newProp.transform.position += -Vector3.right * sprite.sprite.bounds.size.x / 2;
        }

        newProp.gameObject.AddComponent<Prop>().Assign(stair, sprite);

    }





}
