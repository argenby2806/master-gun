using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStairColor : MonoBehaviour {
    public ParticleSystem particle;
    public ParticleSystem.MainModule emit;
    private void Start()
    {
        emit = particle.main;
        emit.startColor = StairsManager.instance.baseColor;
    }
}
