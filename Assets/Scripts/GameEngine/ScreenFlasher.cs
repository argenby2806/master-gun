using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlasher : MonoBehaviour {

    public Animation flashAnim;

    public static ScreenFlasher instance;

    private void Awake()
    {
        instance = this;
    }

    public void Flash()
    {
        flashAnim.Play();
        CameraFollow.instance.VibrateCamera();
    }


}
