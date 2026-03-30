using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsManager : MonoBehaviour
{

    public static StairsManager instance;
    public static StairsSpawner spawner;

    public Stairs[] stairsPrefabs;

    public int OrderLayer = 0; // the ordering layer of the sprites always decrease by 1 for each spawn

    float luminosityChangePercentage = 17f;// how much the fade decreases/increases each tick

    public Color currentColor;
    public Color baseColor;

    public Color[] levelColors;
    public List<Stairs> spawnedStairs;

    private void Awake()
    {
        instance = this;
        spawner = FindObjectOfType<StairsSpawner>();
        GetRandomColor();
        baseColor = currentColor;
    }

    public Stairs getRandomStairs()
    {
        return stairsPrefabs[Random.Range(0, stairsPrefabs.Length)];
    }

    public void DecreaseOrderingLayer()
    {
        OrderLayer--;
    }

    public void GetRandomColor()
    {
        currentColor = levelColors[Random.Range(0, levelColors.Length)];
    }

    public void DecreaseCurrentColorByTick()
    {
        currentColor = ColorUtility.ChangeColorLuminosity(currentColor, luminosityChangePercentage);
    }

    public void IncreaseStairsColorsByTick()
    {
        foreach (Stairs stair in spawnedStairs)
        {
            stair.targetColor = ColorUtility.ChangeColorLuminosity(stair.spriteRend.color, luminosityChangePercentage, true);
        }
        currentColor = ColorUtility.ChangeColorLuminosity(currentColor, luminosityChangePercentage, true);
    }




}
