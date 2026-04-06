using UnityEngine;
using System.Collections;

public class SoundsManager : MonoBehaviour
{

    public AudioSource baseSource;


    public AudioClip[] gameSounds;
    public AudioSource HeadshotSound;
    public AudioSource CoinSound;
    public static SoundsManager instance;

    bool cannotExecuteSound;
    float timeToBePaused = 0.1f;


    public static bool audioEnabled = true;


    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SoundsManager.audioEnabled == false)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
//headshot sound
    public void PlayHeadshotSound()
    {
        if(HeadshotSound.isPlaying==false)
        HeadshotSound.Play();
    }
    //PlaycoinSound
    public void PlayCoinSound()
    {
        CoinSound.Play();
    }
    //PlayGameOverSound
    public void PlayGameOverSound()
    {
        Play(gameSounds[1]);
    }
    //PlayMenuButtonSound
    public void PlayMenuButtonSound()
    {
        Play(gameSounds[0]);
    }


    //PlayDestroySound
    public void Play(AudioClip clip)
    {
        StartCoroutine(PlayAndDestroySource(clip));
    }

    public AudioSource CreateTempSource(AudioSource audioSource)
    {
        AudioSource tempSource = GameObject.Instantiate(audioSource.gameObject).GetComponent<AudioSource>();
        return tempSource;
    }

    private IEnumerator PlayAndDestroySource(AudioClip clip)
    {
        AudioSource audioSource = GameObject.Instantiate(baseSource.gameObject).GetComponent<AudioSource>();

        audioSource.transform.SetParent(transform);
        audioSource.name = clip.name + "_AudioSource";
        audioSource.clip = clip;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Destroy(audioSource.gameObject);
    }

}
