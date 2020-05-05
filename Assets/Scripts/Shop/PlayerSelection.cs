using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public int playerID;

    public int coinCost;

    [SerializeField] private GameObject boughtGameobject;
    [SerializeField] private GameObject notBoughtGameobject;

    [SerializeField] private Text costText;
 
    private void Start()
    {
        costText.text = coinCost.ToString();

        if (DataManager.instance.players[playerID])
        {
            boughtGameobject.SetActive(true);
            notBoughtGameobject.SetActive(false);
        }
        else
        {
            boughtGameobject.SetActive(false);
            notBoughtGameobject.SetActive(true);
        }
    }

    #region Buy Settings

    public void BuyButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();

        if (DataManager.instance.coins > coinCost)
        {
            DataManager.instance.coins -= coinCost;
            DataManager.instance.players[playerID] = true;
            DataManager.instance.Save();

            MainMenuManager.instance.ChangeCoinText(DataManager.instance.coins.ToString());
            ShopManager.instance.ChangeShopCoinsText(DataManager.instance.coins.ToString());

            boughtGameobject.SetActive(true);
            notBoughtGameobject.SetActive(false);
        }
        else
        {
            ShopManager.instance.SetActiveNotEnoughtPanel(true);
        }
    }

    public void UseButtonClicked()
    {
        AudioManager.instance.PlayButtonClickSound();

        DataManager.instance.selectedPlayer = playerID;
        DataManager.instance.Save();

        ShopManager.instance.SetActiveShopPanel(false);
        ShopManager.instance.SetActiveShowCaseObjects(playerID);
    }


    #endregion


}
