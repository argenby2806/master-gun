using UnityEngine;
using System.Collections;
using Oblius.Assets.AllInOneAdnetworks;

public class GameEventsCollection : MonoBehaviour
{
    public static GameEventsCollection instance;
    public Animation screenFadeToBlackAnimation;

    // To call this script somewhere in any code, attach this script to a gameObject and
    // call it by using GameEventsCollection.instance.something();


    private void Awake()
    {
        instance = this;
    }

    public bool GameStarted;

    public void StartGame() // Call when you start the game ( imagine an ipotethical menu. When you click PLAY this event must be called )
    {
        GameStarted = true;
        GameEngine.instance.OnBeginGame();
    }

    public void GetCoin(int val) // Call when you collect a coin
    {
        ScoreHandler.instance.specialPoints += val;
     //   SoundsManager.instance.PlayStarSound();
    }

    public void IncreaseScore(int val) // Call when you score
    {
        ScoreHandler.instance.score += val;
      //  SoundsManager.instance.PlayLandSound();
    }

    private bool finished;

    public void GameFinished(bool isDeath=true) // Call On Game Over
    {
        if (finished) return;

        finished = true;


        if (isDeath)
        {
            ObliusGameManager.instance.GameOver(1f);
        }
        else
        {

            StartCoroutine(NewLevel());

        }
    }


    IEnumerator NewLevel()
    {
        screenFadeToBlackAnimation.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        screenFadeToBlackAnimation.Play();
        yield return new WaitForSeconds(screenFadeToBlackAnimation.clip.length);
       // Leaderboard.instance.reportScore(ScoreHandler.instance.score);
       AdNetworksManager.instance.ShowInterstitial(() => Debug.Log("Interstitial Closed"));
        yield return new WaitForSeconds(0.75f);
      
        Application.LoadLevel(Application.loadedLevel);
    }
}