using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour
{

	public static MainMenuGUI instance;

	public Text highscoreText;
	public Text gamesPlayedText;
    public Text coinText;
	string originalHighScoreText;
	string originalGamesPlayedText;

	void Awake ()
	{
		instance = this;
		originalHighScoreText = highscoreText.text;
		originalGamesPlayedText = gamesPlayedText.text;
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void LateUpdate ()
	{
		highscoreText.text = originalHighScoreText + ScoreHandler.instance.highScore;
		gamesPlayedText.text = originalGamesPlayedText + ObliusGameManager.instance.numberOfGames;
        coinText.text = ScoreHandler.instance.specialPoints.ToString();

    }

	public void OnShopButtonClick ()
	{
		SoundsManager.instance.PlayMenuButtonSound ();

		gameObject.SetActive (false);
		GUIManager.instance.ShowShopGUI ();
	}

	public void OnPlayButtonClick ()
	{

		SoundsManager.instance.PlayMenuButtonSound ();

		ObliusGameManager.instance.StartGame ();
		gameObject.SetActive (false);

	}

	public void OnRateButtonClick ()
	{
		SoundsManager.instance.PlayMenuButtonSound ();

		RateManager.instance.rateGame ();
	}



}
