using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using Oblius.Assets.AllInOneAdnetworks;

public class GameOverGUI : MonoBehaviour {

    public Text scoreText;
    public Text highScoreText;
    public Text diamondText;
    public Text coinText;
    public Button GetCoinButton;


	public UnityEvent onGetCoinVideoEntirelyWatched;
	public UnityEvent onGetCoinVideoSkipped;

	// Update is called once per frame
	void LateUpdate() {
        scoreText.text = "" + ScoreHandler.instance.score;
        highScoreText.text = "" + ScoreHandler.instance.highScore;
        diamondText.text = "" + ScoreHandler.instance.specialPoints;
	}



    public void OnBallShopClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        Deactivate();
        GUIManager.instance.ShowShopGUI();
    }

    public void OnRemoveAdsButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();
		//Purchaser.instance.BuyNonConsumable(Purchaser.instance.purchaseItems[0].generalProductID);
    }

    public void OnRestorePurchaseButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();
       // Purchaser.instance.RestorePurchases();
    }

    public void OnLeaderboardButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

       // Leaderboard.instance.showLeaderboard();
    }

    public void OnShareButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        ShareManager.instance.share();
    }

    public void OnPlayButtonClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }





}
