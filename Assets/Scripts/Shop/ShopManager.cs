using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("UI")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject notEnoughtCoinPanel;
    [SerializeField] private Text shopCoinsText;
    [Header("SHOWCASE")]
    [SerializeField] private GameObject[] showCaseObjects;

    public bool getCoinButtonClicked { get; set; } = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetActiveShopPanel(false);
        SetActiveNotEnoughtPanel(false);
        ChangeShopCoinsText(DataManager.instance.coins.ToString());

        SetActiveShowCaseObjects(DataManager.instance.selectedPlayer);
 
    }

    #region Button Configurations

    public void OpenShopButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        SetActiveShopPanel(true);
    }

    public void CloseShopButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        SetActiveShopPanel(false);
    }


    public void NotEnoughtOkButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();
        SetActiveNotEnoughtPanel(false);
    }

    public void WatchAdsGetsCoinButtonClicked()
    {
        AdMobManager.instance.ShowRewardBasedAd();
        getCoinButtonClicked = true;
    }

    #endregion

    #region Set Active Part

    public void SetActiveShopPanel(bool value)
    {
        shopPanel.SetActive(value);
    }
    public void SetActiveNotEnoughtPanel(bool value)
    {
        notEnoughtCoinPanel.SetActive(value);
    }


    #endregion

    #region Texts

    public void ChangeShopCoinsText(string text)
    {
        shopCoinsText.text = text;
    }

    #endregion

    public void SetActiveShowCaseObjects(int num)
    {
        foreach (GameObject go in showCaseObjects)
        {
            go.SetActive(false);
        }

        showCaseObjects[num].SetActive(true);
    }


}
