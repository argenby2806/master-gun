using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLine : MonoBehaviour
{
   public float startingX;
    public float maxXOffset = 1;
    public Vector3 Offset;
    Vector3 startingPosition;
    float maxZDegreeParent = 45;
    public static GunLine instance;
    public float aimCirlceScale = 1;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    void Update()
    {

        Vector3 scale = transform.localScale;
        float anglez = transform.parent.localEulerAngles.z;
        scale.x = Mathf.Lerp(startingX, startingX + maxXOffset, anglez / maxZDegreeParent);
        transform.localScale = scale;

        if(startingPosition == Vector3.zero)
        {
            startingPosition = transform.localPosition;
        }
        transform.localPosition = startingPosition + Offset;

    }
}
