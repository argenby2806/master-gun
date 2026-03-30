using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class ScoreHandler : MonoBehaviour
{

	int _score;

	public int score {
		get {
			return _score;
		}
		set {			
			_score = value;
			lifeTimeScore += value;

			if (score > highScore) {

				if (!highscoreScoredInThisSession) {
					if (OnScoredHighscore != null) {
						OnScoredHighscore.Invoke (score);
					}
				}

				highscoreScoredInThisSession = true;
				highScore = score;
			}

		}
	}


	int _secondaryScore;

	public int secondaryScore {
		get {
			return _secondaryScore;
		}
		set {
			_secondaryScore = value;
		}
	}

	int _lifetimeScore;

	public int lifeTimeScore {
		get {
			return PlayerPrefs.GetInt ("LIFETIMESCORE", 0);
		}
		set {
			PlayerPrefs.SetInt ("LIFETIMESCORE", value);


		}
	}


	int _highScore;

	public int highScore {
		get {
			return PlayerPrefs.GetInt ("HIGHSCORE", 0);
		}
		set {
			PlayerPrefs.SetInt ("HIGHSCORE", value);
		}
	}

	int _specialPoints;

	public int specialPoints {
		get {
			return PlayerPrefs.GetInt ("SPECIALPOINTS", 0);
		}
		set {
			PlayerPrefs.SetInt ("SPECIALPOINTS", value);
		}
	}




	public bool highscoreScoredInThisSession;

	public UnityIntEvent OnScoredHighscore;

	public static ScoreHandler instance;

	void Awake ()
	{		
		instance = this;    	
	}

	public ScoreHandlerData Serialize ()
	{
		ScoreHandlerData scoreHandlerData = new ScoreHandlerData ();
		scoreHandlerData.score = score;
		scoreHandlerData.highScoreScoredInThisSession = highscoreScoredInThisSession;

		return scoreHandlerData;
	}

	public void Deserialize (ScoreHandlerData data)
	{
		score = data.score;
		highscoreScoredInThisSession = data.highScoreScoredInThisSession;
	}


	public void reset ()
	{
		score = 0;
		highscoreScoredInThisSession = false;
		secondaryScore = 0;
	}

	[System.Serializable]
	public class UnityIntEvent : UnityEvent<int>
	{
		
	}

}

[Serializable]
public class ScoreHandlerData
{
	public int score;
	public bool highScoreScoredInThisSession;
}