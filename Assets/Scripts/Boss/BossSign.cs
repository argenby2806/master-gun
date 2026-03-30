using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossSign : MonoBehaviour {

    public GameObject healthBar;
    public Animation startAnimation;
    public Slider slider;
    public Text bossName;
    float startValue;
	// Use this for initialization
    IEnumerator StartRoutine()
    {
        healthBar.SetActive(false);
        startAnimation.Play();

        yield return new WaitForEndOfFrame();

        bossName.text = Character.enemy.graphicsProperty.enemyName;

        yield return new WaitForSeconds(startAnimation.clip.length);

        startValue = Character.enemy.life;

        healthBar.SetActive(true);


        // Calling here enemy ready after the boss spawn
        GameEngine.instance.EnemyReady();

    }

    private void OnEnable()
    {
        StartCoroutine(StartRoutine());
    }

    private void Update()
    {
        float val = (float)Character.enemy.life / startValue;
        slider.value = val;
    }

}
