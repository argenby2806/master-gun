using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
    public static BackgroundMusic instance;
     AudioSource bgMusicAudiosource;
    float currentVolume;
    void Awake() {
        bgMusicAudiosource = GetComponent<AudioSource>();
        currentVolume = bgMusicAudiosource.volume;
        bgMusicAudiosource.loop = true;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            }
	// Use this for initialization
public void PlayMusic() { if(bgMusicAudiosource.isPlaying==false)bgMusicAudiosource.Play(); }

    public void FadeMusic(bool inOut = false)
    {
        StartCoroutine(fadeMusic(inOut));
    }
    IEnumerator fadeMusic(bool inout)
    {
        float lerper = 0;
        float lerperTime = 0.5f;
        while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;
            yield return new WaitForEndOfFrame();
            if (inout)
            {
                bgMusicAudiosource.volume = Mathf.Lerp(0, currentVolume, lerper);
            }
            else {
                bgMusicAudiosource.volume = Mathf.Lerp(currentVolume, 0, lerper);

            }
        }
    }

}
