using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSpawner : MonoBehaviour
{

    float XPositionLeft = 0.5f;
    float XPositionRight = -0.5f;
    float lastYposition = -3.1f;


    public Stairs lastSpawnedStair;


    bool directionTrigger;

    public void SpawnNewStair(int val = 0)
    {
        for (int i = 0; i < val; i++) {

            GameObject newStair = (GameObject)Instantiate(StairsManager.instance.getRandomStairs().gameObject);

            float dir = 0;
            float scaleMultiplier = 0;
            getDirection(out dir, out scaleMultiplier); // get direction and orientation (left/right)


            Vector3 pos = new Vector3(dir, lastYposition, 0); // set position
            newStair.transform.position = pos;

            Vector3 scale = newStair.transform.localScale; // set the scale ( for left/right orientation )
            scale.x = scale.x * scaleMultiplier;
            newStair.transform.localScale = scale;

            Stairs currentStair = newStair.GetComponent<Stairs>(); // extra set up
            currentStair.SetUp();

            lastSpawnedStair = currentStair; // override last stair and the last y position
            lastYposition = lastSpawnedStair.topPoint.position.y;



            int checkToSpawnProp = Random.Range(0, 5);
            if (checkToSpawnProp == 0)
            {
                PropsManager.instance.SpawnPropOnStair(newStair.GetComponent<Stairs>());
            }


        }
    }



    void getDirection(out float returnPos, out float returnScale)
    {
        directionTrigger = !directionTrigger;

        if (directionTrigger)
        {
            returnPos = XPositionLeft;
            returnScale = 1;
        }
        else
        {
            returnPos = XPositionRight;
            returnScale = -1;

        }

    }
}