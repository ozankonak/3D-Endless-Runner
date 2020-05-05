using UnityEngine;
using UnityEngine.SceneManagement;
public class EnergyManager : MonoBehaviour
{
    #region Singleton

    public static EnergyManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    public const int maxEnergy = 100;

    private int energyPerAds = 25;
    public int currentEnergy { get; set; }

    private void Start()
    {
        InitializeEnergy();
    }

    private void InitializeEnergy()
    {
        currentEnergy = DataManager.instance.energy;
        MainMenuManager.instance.ChangeEnergyText(currentEnergy.ToString());
        MainMenuManager.instance.ChangeEnergySlider((float)currentEnergy / (float)maxEnergy);
    }

    public void AddEnergy(int value)
    {
        currentEnergy += value;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        DataManager.instance.energy = currentEnergy;
        DataManager.instance.Save();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainMenuManager.instance.ChangeEnergyText(currentEnergy.ToString());
            MainMenuManager.instance.ChangeEnergySlider((float)currentEnergy / (float)maxEnergy);
        }
    }

    public void SpendEnergy(int value)
    {
        currentEnergy -= value;

        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }

        DataManager.instance.energy = currentEnergy;
        DataManager.instance.Save();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainMenuManager.instance.ChangeEnergyText(currentEnergy.ToString());
            MainMenuManager.instance.ChangeEnergySlider((float)currentEnergy / (float)maxEnergy);
        }
    }

    public void GiveEnergyWithAds()
    {
        currentEnergy += energyPerAds;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        DataManager.instance.energy = currentEnergy;
        DataManager.instance.Save();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
             MainMenuManager.instance.ChangeEnergyText(currentEnergy.ToString());
             MainMenuManager.instance.ChangeEnergySlider((float)currentEnergy / (float)maxEnergy);
        }
    }


}
