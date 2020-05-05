using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;

    private int randomNumber = -1;
    private static int SpawnCount;

    private void Start()
    {
        EventManager.StartListening(EventManager.instance.playerDeadEvent, ResetSpawnCount);
        EventManager.StartListening(EventManager.instance.gameOverEvent, ResetSpawnCount);
        EventManager.StartListening(EventManager.instance.platformSpawnEvent, () => SpawnCount++);
    }


    private void OnEnable()
    {
        randomNumber = Random.Range(0, obstacles.Length);

        foreach (GameObject go in obstacles)
        {
            go.SetActive(false);
        }

        if (SpawnCount > 3)
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (randomNumber == i)
                {
                    obstacles[i].SetActive(true);
                }
            }
        }
    }

    private void ResetSpawnCount()
    {
        SpawnCount = 0;
    }
}
