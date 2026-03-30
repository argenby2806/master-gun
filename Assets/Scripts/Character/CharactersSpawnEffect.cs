using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawnEffect : MonoBehaviour
{
    Character currentChar;

    void Awake()
    {
        currentChar = GetComponent<Character>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEffect());
    }

    IEnumerator SpawnEffect()
    {
        int spawnEffect = Random.Range(0, 3);

        if (currentChar.isPlayer == false)
        {
            Vector3 targetPos = transform.position;
            bool leftRight = transform.position.x < 0;
            Vector3 spawnPos = transform.position;
            if (leftRight)
            {
                spawnPos.x = -5;
            }
            else
            {
                spawnPos.x = +5;
            }

            float lerper = 0;
            float lerperTime = 0.35f;

            switch (spawnEffect)
            {
                case 1:
                    lerperTime = lerperTime * 2; // slow spawn
                    break;

                case 2:
                    spawnPos.y += 1; // from above;
                    break;
            }


            while (lerper <= 1)   // Jump 
            {
                lerper += Time.deltaTime / lerperTime;
                transform.position = Vector3.Lerp(spawnPos, targetPos, lerper);
                yield return new WaitForEndOfFrame();
            }

          
        }
        yield return null;



    }
}
