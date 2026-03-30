using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (transform.position.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }

    }
	

}
