using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimCanvas : MonoBehaviour
{
    public float yOffset = 1;
    public Image aimImage;
    public static AimCanvas instance;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Vector3 scale = aimImage.transform.localScale;
        scale.x = GunLine.instance.aimCirlceScale;
        scale.y = scale.x;
        aimImage.transform.localScale = scale;

        if (PlayerStatusHandler.instance.status != PlayerStatusHandler.Status.aiming)
        {
            aimImage.fillAmount = 0;
        }
        else
        {
            Vector3 angles = PlayerGun.instance.gunTransform.eulerAngles;
            float val = angles.z / 360;

            float usedValue = 0;

            if (Character.player.isRightOriented() == false)
            {
                usedValue = val;

                aimImage.fillOrigin = 1;
                aimImage.fillClockwise = false;

            }
            else
            {
                usedValue = -(val - 1);

                aimImage.fillOrigin = 3;
                aimImage.fillClockwise = true;

            }

            if (usedValue > 0 & usedValue < 0.5f)
            {
                aimImage.fillAmount = usedValue;
            }

            Vector3 pos = (PlayerGun.instance.gunTransform.position);
            pos.y += yOffset;
            aimImage.transform.position = pos;

        }
    }
}
