using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Vector3 distanceOffset;
    float followSpeed = 1;
    public bool vibrate;
    public static CameraFollow instance;
    public Animation vibrationAnimation;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(FollowRoutine());
    }
    IEnumerator FollowRoutine()
    {
        while (Character.player == null)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();


        }
        Vector3 charpos = Character.player.transform.position;
        charpos.x = transform.position.x;
        distanceOffset = transform.position - charpos;

        while (true)
        {
             charpos = Character.player.transform.position;
            charpos.x = transform.position.x;

            if (vibrate == false)
            {
                transform.position = Vector3.Lerp(transform.position, charpos + distanceOffset, followSpeed * Time.deltaTime);
            }

            yield return new WaitForEndOfFrame();
        }
    }


    public void VibrateCamera()
    {

        StartCoroutine(VibrationRoutine());
    }
    IEnumerator VibrationRoutine()
    {
        vibrate = true;
        vibrationAnimation.Play();
        yield return new WaitForSeconds(vibrationAnimation.clip.length);
        vibrate = false;

    }

}
