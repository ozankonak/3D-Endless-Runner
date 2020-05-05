using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private GameData data;

    public bool isGameStartedFirstTime;
    public bool isMusicOn;

    [Header("TIME")]
    public string datetime;

    [Header("SCORES AND COIN")]
    public int energy;
    public int coins;
    public float score;
    
    [Header("ACHIEVEMENTS")]
    public bool[] achievements;
    public bool[] collectedItems;

    [Header("STORE")]
    public int selectedPlayer;
    public bool[] maps;
    public bool[] players;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitializeGameVariables();
    }

    private void InitializeGameVariables()
    {
        Load();

        if (data != null)
        {
            isGameStartedFirstTime = data.isGameStartedFirstTime;
        }else
        {
            isGameStartedFirstTime = true;
        }

        // FIRST VALUES IN THIS GAME
        //TODO: WILL CHANGE IN THE FUTURE
        if (isGameStartedFirstTime)
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
            energy = 100;
            score = 0;
            coins = 0;
            datetime = DateTime.Now.ToBinary().ToString();

            maps = new bool[3];
            players = new bool[4];
            achievements = new bool[3];
            collectedItems = new bool[4];


            maps[0] = true;
            for (int i = 1; i < maps.Length; i++)
            {
                maps[i] = false;
            }

            players[0] = true;
            for (int i = 1; i < players.Length; i++)
            {
                players[i] = false;
            }

            achievements[0] = true;
            for (int i = 1; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }

            collectedItems[0] = true;
            for (int i = 1; i < collectedItems.Length; i++)
            {
                collectedItems[i] = false;
            }

            data = new GameData();

            data.isGameStartedFirstTime = isGameStartedFirstTime;
            data.isMusicOn = isMusicOn;
            data.selectedPlayer = selectedPlayer;
            data.energy = energy;
            data.score = score;
            data.coins = coins;
            data.datetime = datetime;
            data.maps = maps;
            data.players = players;
            data.achievements = achievements;
            data.collectedItems = collectedItems;
            

            Save();
            Load();
        }
        else
        {
            isGameStartedFirstTime = data.isGameStartedFirstTime;
            isMusicOn = data.isMusicOn;
            selectedPlayer = data.selectedPlayer;
            energy = data.energy;
            datetime = data.datetime;
            maps = data.maps;
            players = data.players;
            achievements = data.achievements;
            collectedItems = data.collectedItems;
            coins = data.coins;
            score = data.score;
        }
    }

    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {
                data.isGameStartedFirstTime = isGameStartedFirstTime;
                data.isMusicOn = isMusicOn;
                data.selectedPlayer = selectedPlayer;
                data.energy = energy;
                data.datetime = datetime;
                data.maps = maps;
                data.players = players;
                data.achievements = achievements;
                data.collectedItems = collectedItems;
                data.coins = coins;
                data.score = score;

                bf.Serialize(file, data);
            }
        }
        catch (Exception e) { }


        finally
        {
            if (file != null)
            file.Close();
        }
    }
    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + "/Gamedata.dat",FileMode.Open);

            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception) {}
        
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }




    }
}


