using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus { Start, GamePlay,Dead, GameOver}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameStatus status = GameStatus.Start;

    [SerializeField] private GameObject[] players;

    private void Awake()
    {
        instance = this;

        ChoosePlayer();
    }

    private void ChoosePlayer()
    {
        foreach (GameObject go in players)
        {
            go.SetActive(false);
        }

        for (int i = 0; i < players.Length; i++)
        {
            if (DataManager.instance.selectedPlayer == i)
            {
                players[i].SetActive(true);
            }
        }
    }

    private void Start()
    {
        //Optimization Settings For PC
        //TODO: Be careful when targetFrameRate 300 Mobile Platforms consume more battery it should be 60 in the end.
        Application.targetFrameRate = 300;
        EventManager.StartListening(EventManager.instance.GameStartButton, StartTheGame);
        EventManager.StartListening(EventManager.instance.gameOverEvent, GameOver);
        EventManager.StartListening(EventManager.instance.playerResurrectEvent, DeadAndResurrect);
    }

    private void StartTheGame()
    {
        EventManager.StopListening(EventManager.instance.GameStartButton, StartTheGame);
        UIManager.instance.SetActiveClickToPlayPanel(false);
        PlayerAnimations.instance.PlayRunAnimation(true);
        status = GameStatus.GamePlay;
    }


    #region Game Over

    private void GameOver()
    {
        status = GameStatus.GameOver;

        UIManager.instance.SetActivePauseButton(false);
        UIManager.instance.SetActivePausePanel(false);

        Invoke("GameOverScreen", 3f);
    }

    private void GameOverScreen()
    {
        AudioManager.instance.PlayGameOverSound();
        UIManager.instance.SetActiveGameOverPanel(true);
    }

    #endregion

    private void DeadAndResurrect()
    {
        status = GameStatus.Dead;

        Invoke("Resurrect", 5f);
    }

    private void Resurrect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Button Configurations
    public void OnPauseButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        UIManager.instance.SetActivePausePanel(true);
        Time.timeScale = 0;
    }

    public void OnContinueButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        Time.timeScale = 1;
        UIManager.instance.SetActivePausePanel(false); 
    }

    public void OnPauseToMainMenuButtonClicked()
    {
        AdMobManager.instance.ShowInterstialAds();
        AudioManager.instance.PlayButtonClickSound();
        Time.timeScale = 1;

        DataManager.instance.coins += ScoreManager.currentCoin;

        if ((ScoreManager.currentScore + ScoreManager.currentCoin) > DataManager.instance.score)
        {
            DataManager.instance.score = (ScoreManager.currentScore + ScoreManager.currentCoin);
        }

        DataManager.instance.Save();
            
        ScoreManager.currentCoin = 0;
        ScoreManager.currentScore = 0;
        PlayerHealth.currentHealth = 3;

        SceneManager.LoadScene("MainMenu");

    }

    public void OnGameOverToMainMenuButtonClicked()
    {
        AdMobManager.instance.ShowInterstialAds();
        AudioManager.instance.PlayButtonClickSound();
        DataManager.instance.coins += ScoreManager.currentCoin;

        if ((ScoreManager.currentScore + ScoreManager.currentCoin) > DataManager.instance.score)
        {
            DataManager.instance.score = (ScoreManager.currentScore + ScoreManager.currentCoin);
        }
        DataManager.instance.Save();

        ScoreManager.currentCoin = 0;
        ScoreManager.currentScore = 0;
        PlayerHealth.currentHealth = 3;

        SceneManager.LoadScene("MainMenu");
    }



    #endregion

    public void OnApplicationQuit()
    {
        DataManager.instance.coins += ScoreManager.currentCoin;

        if ((ScoreManager.currentScore + ScoreManager.currentCoin) > DataManager.instance.score)
        {
            DataManager.instance.score = ScoreManager.currentScore + ScoreManager.currentCoin;
        }

        DataManager.instance.Save();
    }

}
