using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayerParticle : MonoBehaviour {

    public SingleParticle[] particles;
    public SpriteRenderer expandCircle;
    private void Awake()
    {
        particles = GetComponentsInChildren<SingleParticle>();
    }
    private void Start()
    {
        foreach (SingleParticle particle in particles)
        {
            particle.AddForce();
        }
        expandCircle.color = BulletsManager.instance.currentBloodColor;
    }


}
