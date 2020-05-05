using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPoints;

    private int randomNumber;

    private void OnEnable()
    {
       randomNumber = Random.Range(0, spawnPoints.Length);

       transform.localPosition = spawnPoints[randomNumber];

    }




}
