using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnvironments : MonoBehaviour
{
    [SerializeField] private GameObject[] environments;

    private int randomNumber = -1;

    private void OnEnable()
    {
        randomNumber = Random.Range(0, environments.Length);


        foreach (GameObject go in environments)
        {
            go.SetActive(false);
        }

            for (int i = 0; i < environments.Length; i++)
            {
                if (randomNumber == i)
                {
                    environments[i].SetActive(true);
                }
            }
        
    }

}
