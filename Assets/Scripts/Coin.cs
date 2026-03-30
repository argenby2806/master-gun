using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public Rigidbody2D rigid;
	// Use this for initialization
	void Start () {

        rigid.AddForce(Vector2.up*Random.Range(3f,6f), ForceMode2D.Impulse);
        rigid.AddForce(Vector2.right * Random.Range(-3f,3f), ForceMode2D.Impulse);
        Destroy(gameObject, 3);
        GameEventsCollection.instance.GetCoin(1);
	}
	

}
