using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {


    public TextMesh textMesh;
    public static DamageText instance;
    private void Awake()
    {
        transform.position += Random.insideUnitSphere.normalized*0.2f;
        instance = this;
        Destroy(this.gameObject, 3);
    }
    public void SetText(string text)
    {
        textMesh.text = "-"+text;
    }

}
