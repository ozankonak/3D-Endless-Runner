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
        StartCoroutine(UpdateScore());
    }

    IEnumerator UpdateScore()
    {
        yield return new WaitForSeconds(0.5f);

        if (GameManager.instance.status == GameStatus.GamePlay)
        {
            currentScore++;
            UIManager.instance.ChangeScoreText(((int)(currentScore + currentCoin)).ToString());
        }

        StartCoroutine(UpdateScore());

    }
}
