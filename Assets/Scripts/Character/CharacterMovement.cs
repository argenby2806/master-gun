using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Character currentChar;
    public int currentStair = 1;
    public bool isClimbing;
    float jumpHeight = 0.7f;
    float jumpTime = 0.1f;

    private void Start()
    {
        currentChar = GetComponent<Character>();
    }

    public void ClimbStairs()
    {
        // you cannot climb if you are already climbing
        if (isClimbing) return;

        // you cannot climbing if you are not standing ( as player )
        if (currentChar.isPlayer && PlayerStatusHandler.instance.status != PlayerStatusHandler.Status.standing) return;

        StartCoroutine(ClimbRoutine());



    }


    IEnumerator ClimbRoutine()
    {

        isClimbing = true;
        if (currentChar.isDead())
        {
            yield return null;
        }
        else
        {

            if (currentChar.isPlayer)
            {
                GameEngine.instance.OnPlayerClimbing();
            }

            Stairs stairToClimb = StairsManager.instance.spawnedStairs[currentStair];

            Transform[] steps = stairToClimb.steps;


            int currentStep = 0;
            while (currentStep < steps.Length)
            {
                float lerper = 0;
                float lerperTime = jumpTime;
                float jump = jumpHeight;

                Vector3 current = transform.position;
                Vector3 targetTop = steps[currentStep].position + (Vector3.up * jump);

                if (currentChar.isDead()) break;

                    if (currentStep == 0) // do some adjustment on the first step ( it doesn't jump. just go straight at the point )
                {
                    jump = 0;
                    targetTop.x = steps[currentStep].position.x - (steps[currentStep].position.x - current.x);
                    targetTop.y = current.y;
                }

                if (currentChar.isPlayer == false && currentStep == steps.Length - 1) // the enemy climbs but stay at the ledge
                {
                    targetTop.x = stairToClimb.topPoint.position.x;
                }



                while (lerper <= 1)   // Jump 
                {
                    if (currentChar.isDead()) break;

                    lerper += Time.deltaTime / lerperTime;
                    transform.position = Vector3.Lerp(current, targetTop, lerper);
                    yield return new WaitForEndOfFrame();
                }


                if (currentStep == steps.Length - 1)  // Flip the character when they are in mid air at the last "step"
                {
                    FlipCharacter();

                }


                Vector3 targetLand = steps[currentStep].position;

                if (currentChar.isPlayer == false && currentStep == steps.Length - 1)
                {
                    targetLand = stairToClimb.topPoint.position; // the enemy climbs but stay at the ledge 
                }

                lerper = 0;

                while (lerper <= 1) // Land
                {
                    if (currentChar.isDead()) break;

                    lerper += Time.deltaTime / lerperTime;
                    transform.position = Vector3.Lerp(targetTop, targetLand, lerper);
                    yield return new WaitForEndOfFrame();
                }

                GameObject part = CharacterManager.instance.walkparticlePrefab;
                GameObject newWalkPart = (GameObject)Instantiate(part, targetLand, part.transform.rotation);
                Destroy(newWalkPart, 3);

                currentStep++;
            }

            currentStair++;
            if (currentChar.isPlayer)
            {
                GameEngine.instance.OnPlayerFinishedClimbing();
            }


            isClimbing = false;


        }

    }


    void FlipCharacter() // flip the scale
    {
        Vector3 currentscale = transform.localScale;
        currentscale.x = currentscale.x * -1;
        transform.localScale = currentscale;
    }

}
