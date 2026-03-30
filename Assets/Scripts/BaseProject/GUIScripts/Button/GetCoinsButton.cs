using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oblius.Assets.AllInOneAdnetworks;
using UnityEngine.Events;
using UnityEngine.UI;

public class GetCoinsButton : MonoBehaviour
{
	public int minCoinsToReward;
	public int maxCoinsToReward;

	public int coinToReward {
		get {
			return Random.Range (minCoinsToReward, maxCoinsToReward);
		}
	}

	Button _button;

	Button button {
		get {
			if (_button == null) {
				_button = GetComponent<Button> ();
			}
			return _button;
		}
	}

	public void OnClick ()
	{
        SoundsManager.instance.PlayMenuButtonSound();
        
		AdNetworksManager.instance.ShowRewardedVideo ((bool value) => {
			if (value) {
				OnVideoWatched ();
			} else {
				OnVideoSkipped ();
			}
		}
        
		);
		button.interactable = false;
	}

	public void OnVideoWatched ()
	{
		ScoreHandler.instance.specialPoints += coinToReward;
	}

	public void OnVideoSkipped ()
	{
		Util.ShowPopUp (Application.productName, "Please watch the entire video to get the reward!");
	}

}
