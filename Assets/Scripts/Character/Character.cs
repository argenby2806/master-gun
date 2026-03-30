using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public bool RandomizeGraphics;

    public GameObject Graphics;
    public GameObject GunGraphics;
    public static Character player;
    public static Character enemy;
    public CharacterGraphics graphicsProperty;
    public bool isPlayer;
    public bool isBoss;
    public int life=0;
    [HideInInspector]
    public CharacterMovement movement;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();

  
        if (isPlayer) { player = this; } else {

            enemy = this;

            movement.currentStair = player.movement.currentStair + 1;
        }
    }

    private void Start()
    {
        Debug.Log(" R : Randomize Chars");
        Debug.Log(" K : CLIMB");

        if (isPlayer)
        { // set the starting position
            transform.position = StairsManager.instance.spawnedStairs[0].steps[StairsManager.instance.spawnedStairs[0].steps.Length - 1].position;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChooseCharacter(CharacterManager.instance.GetRandomChar());
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            movement.ClimbStairs();
        }

    }

    public bool isRightOriented()
    {
        return (transform.position.x > 0);
    }


    void ChooseCharacter(GameObject newObj)
    {
        Transform parent = Graphics.transform.parent;
        Vector3 localPos = Graphics.transform.localPosition;

        Destroy(Graphics);

        GameObject newGraphics = (GameObject)Instantiate(newObj);

        newGraphics.transform.parent = parent;
        newGraphics.transform.localPosition = localPos;


        Graphics = newGraphics;
        graphicsProperty = newGraphics.GetComponent<CharacterGraphics>();
    }

    public void RemoveTagsFromPieces()
    {
        graphicsProperty.body.tag = "Untagged";
        graphicsProperty.head.tag = "Untagged";

    }

    public void BecomeNormal()
    {
        if (RandomizeGraphics)
        {
            ChooseCharacter(CharacterManager.instance.GetRandomChar());
        }
    }

    public void BecomeBoss()
    {
        isBoss = true;
        life = BossManager.instance.GetBossLife();
        ChooseCharacter(BossManager.instance.GetRandomChar());

    }


    public bool isDead()
    {
        if (life < 0) { return true; }
        else
        {

            return false;
        }
    }

    public void ShootPlayer()
    {
        GetComponent<EnemyShooter>().ShootPlayer();
    }



}
