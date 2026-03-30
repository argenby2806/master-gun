using UnityEngine;
using System.Collections;
using Oblius.Assets.AllInOneAdnetworks;

public class ObliusGameManager : MonoBehaviour
{

	public static ObliusGameManager instance;

	public enum GameState
	{
		menu,
		game,
		gameover,
		shop
	}

	public GameState gameState;
	public bool oneMoreChanceUsed = false;


	public int numberOfGames {
		get {
			return PlayerPrefs.GetInt ("NUMBER_OF_GAMES");
		}
		set {
			PlayerPrefs.SetInt ("NUMBER_OF_GAMES", value);
		}
	}


	void Awake ()
	{
		instance = this;
	}
	// Use this for initialization
	void Start ()
	{
		Application.targetFrameRate = 60;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public IEnumerator GameOverCoroutine (float delay)
	{
		gameState = GameState.gameover;
		yield return new WaitForSeconds (delay);
		SoundsManager.instance.PlayGameOverSound ();
		AdNetworksManager.instance.HideBanner ();

		//Leaderboard.instance.reportScore (ScoreHandler.instance.score);
		GUIManager.instance.ShowGameOverGUI ();
		InGameGUI.instance.gameObject.SetActive (false);
		AdNetworksManager.instance.ShowInterstitial (() => Debug.Log ("Interstitial Closed"));
	}


	public void GameOver (float delay)
	{
		StartCoroutine (GameOverCoroutine (delay));
	}

	public void StartGame ()
	{
		ResetGame ();
		numberOfGames++;
		GUIManager.instance.ShowInGameGUI ();
		//GUIManager.instance.tutorialGUI.ShowIfNeverAppeared();
		AdNetworksManager.instance.ShowBanner ();
		gameState = GameState.game;
        GameEventsCollection.instance.StartGame();
	}

	public void ResetGame (bool resetScore = true, bool resetOneMoreChance = true)
	{
		if (resetOneMoreChance) {
			oneMoreChanceUsed = false;
		}

		if (resetScore) {
			ScoreHandler.instance.reset ();
		}
	}


}
