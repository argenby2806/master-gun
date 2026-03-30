using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLink {
    public string ID;
    public string Link;
}


public class PrivacyPolicyScript : MonoBehaviour {


    public GameLink[] Links;
         

    public void OpenGameID(string ID) {
        for (int i = 0; i < Links.Length; i++)
        {
            if (ID == Links[i].ID) {
                Application.OpenURL(Links[i].Link);
            }
        }
    }

}
