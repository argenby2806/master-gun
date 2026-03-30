using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    public static CharacterManager instance;

    public GameObject [] CharactersGraphics;

    public GameObject walkparticlePrefab;

    private void Awake()
    {
        instance = this;
    }

    public  GameObject GetRandomChar()
    {
        return CharactersGraphics[Random.Range(0, CharactersGraphics.Length)];
    }

}
