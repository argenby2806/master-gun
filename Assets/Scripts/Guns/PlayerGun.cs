using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public static PlayerGun instance;
    public SpriteRenderer gunTip;
    public SpriteRenderer gunLine;
    public Transform gunTransform;
    bool tilting = false;
    float lerper = 0;
    float lerperTime = 1.35f;
    float maxGunAngle = 45;
    public GunShooter shooter;
    public GameObject UsedGun;

    private void Awake()
    {
        instance = this;
        gunTip.enabled = false;
        gunLine.enabled = false;

        shooter = GetComponentInChildren<GunShooter>();

       
    }

    private void Update()
    {
        if (isAiming())
        {
            if (!tilting)
            {

                StartCoroutine(tiltGunAim());
            }

            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }
        }

        if (ShopHandler.instance.shopItemToUse.LinkedGun!=UsedGun && ShopHandler.instance.shopItemToUse.LinkedGun!=null)
        {
            UsedGun = ShopHandler.instance.shopItemToUse.LinkedGun;

            ChangeGun(UsedGun);
        }


 
    }



    void Shot()
    {
        GameEngine.instance.OnCharacterShot();
        shooter.Shot();
    }





    IEnumerator tiltGunAim()
    {
        SetTilting(true);

        while (isAiming())
        {
            lerper = 0;
            while (lerper <= 1)
            {
                if (!isAiming()) SetTilting(false);
                LerpGun(true);
                yield return new WaitForEndOfFrame();
            }

            lerper = 0;
            while (lerper <= 1)
            {
                if (!isAiming()) SetTilting(false);
                LerpGun(false);
                yield return new WaitForEndOfFrame();
            }
        }
        SetTilting(false);
    }

    bool isAiming()
    {
        return (PlayerStatusHandler.instance.status == PlayerStatusHandler.Status.aiming);
    }

    public void ResetGun()
    {
        StopAllCoroutines();
        lerper = 0;
        SetTilting(false);
        LerpGun(true);
    }


    void SetTilting(bool val)
    {
        gunLine.enabled = val;
        tilting = val;
    }


    void LerpGun(bool goMax)
    {

        lerper += Time.deltaTime / lerperTime;
        Vector3 currentEuler = gunTransform.eulerAngles;
        float angle = maxGunAngle;
        if (Character.player.isRightOriented()) angle = -angle;
        if (goMax)
        {
            currentEuler.z = Mathf.LerpAngle(0, angle, lerper);
        }
        else
        {
            currentEuler.z = Mathf.LerpAngle(angle, 0, lerper);

        }
        gunTransform.eulerAngles = currentEuler;

    }





    public void ChangeGun(GameObject gun)
    {
        Vector3 gunPosition = shooter.transform.position;
        Transform gunParent = shooter.transform.parent;
        Quaternion gunRotation = shooter.transform.rotation;
        Vector3 gunScale = shooter.transform.localScale;

        Destroy(shooter.gameObject);


        GameObject newGun = (GameObject)Instantiate(gun, gunPosition, gunRotation);
        newGun.transform.parent = gunParent;
        newGun.transform.localScale = gunScale;
        shooter = newGun.gameObject.GetComponent<GunShooter>();
        gunTransform = newGun.transform;
        gunTip =  shooter.tip;
        gunLine = shooter.line;
        gunLine.enabled = false;
        gunTip.enabled = false;
        Character.player.GunGraphics = shooter.graphics.gameObject;

    }



}
