using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlasher : MonoBehaviour {
    //make animation flash 
    public Animation flashAnim;

    public static ScreenFlasher instance;

    private void Awake()
    {
        instance = this;
    }
    //flash and vibrate camera 
    public void Flash()
    {
        flashAnim.Play();
        CameraFollow.instance.VibrateCamera();
    }


}
