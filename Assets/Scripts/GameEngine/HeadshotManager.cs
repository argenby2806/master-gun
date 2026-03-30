using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadshotManager : MonoBehaviour {

    public int headshotsCombo;
    public Text headshotText;
    public Animation headshotAnimation;
    public static HeadshotManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void IncreaseHeadshotCount()
    {
        headshotsCombo++;
    }
    public void ResetHeadshotCount()
    {
        headshotsCombo = 0;
    }

    public void DoHeadshotEffect()
    {
        string headshotstring = "HEADSHOT";
        if (headshotsCombo > 1)
        {
            headshotstring += " X" + headshotsCombo.ToString();
        }
        headshotText.text = headshotstring;
        headshotAnimation.Play();
        SoundsManager.instance.PlayHeadshotSound();
    }
}
