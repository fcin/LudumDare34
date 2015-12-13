using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldSettings : MonoBehaviour {


    public static bool IsGameRunning = false;

	public AudioSource bgMusicPlayer;
    private Text scoreText;

	public GameObject player;
	public GameObject startButton;
	public GameObject restartButton;
	public GameObject coinsText;
	public GameObject volumeSlider;
	public GameObject volumeText;

    private bool reloadedScene = false;

    public void SetGameState(bool isStarted)
    {
        IsGameRunning = isStarted;
    }

    void Start()
    {
        scoreText = GameObject.Find("ScoreUI").GetComponent<Text>();
        
		coinsText.SetActive (false);

		bgMusicPlayer.volume = 0.2f;
    }

    void Update()
    {
        scoreText.text = player.gameObject.transform.position.y.ToString("f1") + " m";
    }

    public void ToggleStartButton(bool shouldHide)
    {
        if (shouldHide)
        {
            startButton.SetActive(false);
			restartButton.SetActive (false);
			coinsText.SetActive (false);
			volumeSlider.SetActive (false);
			volumeText.SetActive (false);
        }
        else
        {
            startButton.SetActive(true);
			restartButton.SetActive (true);
			coinsText.GetComponent<Text> ().text = "Ate " + PlayerController.score + " coins";
			coinsText.SetActive (true);
			volumeSlider.SetActive (true);
			volumeText.SetActive (true);
        }
    }

    public void RestartLevel()
    {
        if (!reloadedScene)
        {
            reloadedScene = true;
            SceneManager.UnloadScene(0);
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(0);
        }
    }

	public void ChangeMusicValue(float value)
	{
		bgMusicPlayer.volume = value;
	}
}
