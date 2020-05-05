using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    [Header("Start")]
    [SerializeField] private GameObject clickToPlayPanel;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    [Header("Coin")]
    [SerializeField] private Text coinText;

    [Header("Meter")]
    [SerializeField] private Text scoreText;

    [Header("Healths")]
    [SerializeField] private GameObject[] healths;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitialValues();
    }

    #region Private Functions

    private void InitialValues()
    {
        SetActiveGameOverPanel(false);
        SetActivePausePanel(false);
        SetActiveClickToPlayPanel(true);
        ChangeCoingText(ScoreManager.currentCoin.ToString());
    }

    #endregion

    #region Set Actives
    public void SetActivePausePanel(bool value)
    {
        pausePanel.SetActive(value);
    }

    public void SetActiveGameOverPanel(bool value)
    {
        gameOverPanel.SetActive(value);
    }

    public void SetActivePauseButton(bool value)
    {
        pauseButton.SetActive(value);
    }

    public void SetActiveClickToPlayPanel(bool value)
    {
        clickToPlayPanel.SetActive(value);
    }

    public void SetActiveHealths(int num)
    {

        #region Control
        if (num > 3)
        {
            num = 3;
        }
       
        if (num <= 0)
        {
            num = 0;
        }

        #endregion

        if (num == 3)
        {
            healths[0].SetActive(true);
            healths[1].SetActive(true);
            healths[2].SetActive(true);
        }
        else if (num == 2)
        {
            healths[0].SetActive(true);
            healths[1].SetActive(true);
            healths[2].SetActive(false);
        }
        else if (num == 1)
        {
            healths[0].SetActive(true);
            healths[1].SetActive(false);
            healths[2].SetActive(false);
        }
        else if (num == 0)
        {
            healths[0].SetActive(false);
            healths[1].SetActive(false);
            healths[2].SetActive(false);
        }
    }

    #endregion

    #region ChangeText

    public void ChangeCoingText(string text)
    {
        coinText.text = text;
    }

    public void ChangeScoreText(string text)
    {
        scoreText.text = text;
    }

    #endregion
}
