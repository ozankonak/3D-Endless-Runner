using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Singleton

    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private DateTime currentDate;

    private float timer = 0;

    private void Start()
    {
        TimePassed();
    }

    private void Update()
    {
        UpdateEnergy();
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.datetime = DateTime.Now.ToBinary().ToString();
        DataManager.instance.Save();
    }


    private void TimePassed()
    {
        currentDate = DateTime.Now;

        long temp = Convert.ToInt64(DataManager.instance.datetime);

        DateTime oldDate = DateTime.FromBinary(temp);

        TimeSpan difference = currentDate.Subtract(oldDate);

        int x = (int)(difference.TotalMinutes);

        for (int i = 0; i <= x; i++)
        {
            EnergyManager.instance.AddEnergy(1);
        }
    }

    private void UpdateEnergy()
    {
        timer += Time.deltaTime;

        if (timer >= 60)
        {
            timer = 0;
            EnergyManager.instance.AddEnergy(1);
        }
    }



























}
