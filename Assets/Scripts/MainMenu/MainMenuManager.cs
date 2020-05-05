using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("Energy")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private Text energyText;
    public int energyPerGame = 25;

    [Header("Loading")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Text loadingTitleText;

    [Header("Coins And Score")]
    [SerializeField] private Text coinText;
    [SerializeField] private Text highScoreText;

    [Header("Ads")]
    [SerializeField] private GameObject watchAdsGetEnergyPanel;

    [Header("Options")]
    [SerializeField] private GameObject optionsPanel;

    public bool yesButtonClicked { get; set; } = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitialValues();
        //Optimization Settings For PC
        //TODO: Be careful when targetFrameRate 300 Mobile Platforms consume more battery it should be 60 in the end.
        Application.targetFrameRate = 300;
    }

    #region UI Settings

    #region Texts
    public void ChangeEnergyText (string text)
    {
        energyText.text = text; 
    }

    public void ChangeEnergySlider(float value)
    {
        energySlider.value = value;
    }

    public void ChangeCoinText(string text)
    {
        coinText.text = text;
    }

    public void ChangeHighScoreText(string text)
    {
        highScoreText.text = text;
    }

    #endregion

    #region SetActives

    private void SetActiveLoadingPanel(bool value)
    {
        loadingPanel.SetActive(value);
        
        if (loadingPanel.activeInHierarchy)
        {
            StartCoroutine(LoadingAnimation());
        }
        else
        {
            StopCoroutine(LoadingAnimation());
        }
    }

    public void SetActiveWatchAdsEarnEnergyPanel(bool value)
    {
        watchAdsGetEnergyPanel.SetActive(value);
    }

    private void SetActiveOptionsPanel(bool value)
    {
        optionsPanel.SetActive(value);
    }

    #endregion

    #endregion

    #region Button Settings

    public void OnPlayButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();

        if (EnergyManager.instance.currentEnergy >= energyPerGame)
        {
            SetActiveLoadingPanel(true);
            EnergyManager.instance.SpendEnergy(energyPerGame);
            Invoke("StartTheGame", Random.Range(4, 6));
        }
        else
        {
            SetActiveWatchAdsEarnEnergyPanel(true);
        }
    }

    public void OnYesButtonClicked()
    {
        AdMobManager.instance.ShowRewardBasedAd();
        yesButtonClicked = true;
    }

    public void OnNoButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        SetActiveWatchAdsEarnEnergyPanel(false);
    }

    public void OnQuitButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        Application.Quit();
    }

    public void OnOpenOptionsClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        SetActiveOptionsPanel(true);
    }

    public void OnCloseOptionsClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        SetActiveOptionsPanel(false);
    }

    #endregion

    #region UI Animations

    IEnumerator LoadingAnimation()
    {
        if (loadingTitleText.gameObject.activeInHierarchy)
        {
            loadingTitleText.text = "LOADING.";
            yield return new WaitForSeconds(0.5f);
            loadingTitleText.text = "LOADING..";
            yield return new WaitForSeconds(0.5f);
            loadingTitleText.text = "LOADING...";
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(LoadingAnimation());
        }
        else
        {
            StopCoroutine(LoadingAnimation());
        }
    }

    #endregion

    #region Private Functions

    private void InitialValues()
    {
        ChangeEnergyText(EnergyManager.instance.currentEnergy.ToString());
        ChangeEnergySlider((float)EnergyManager.instance.currentEnergy / 100f);
        ChangeCoinText(DataManager.instance.coins.ToString());
        ChangeHighScoreText(((int)(DataManager.instance.score)).ToString());

        SetActiveLoadingPanel(false);
        SetActiveWatchAdsEarnEnergyPanel(false);
        SetActiveOptionsPanel(false);
    }

    private void StartTheGame()
    {
        SceneManager.LoadScene("Game");
    }

    #endregion

}
