using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int currentCoin { get; set; } = 0;
    public static float currentScore { get; set; } = 0;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.instance.ChangeScoreText(((int)(currentScore + currentCoin)).ToString());
    }

    private void Update()
    {
        if (GameManager.instance.status == GameStatus.GamePlay)
        {
            currentScore += 2f * Time.deltaTime;
            UIManager.instance.ChangeScoreText(((int)(currentScore + currentCoin)).ToString());
        }
    }
}
