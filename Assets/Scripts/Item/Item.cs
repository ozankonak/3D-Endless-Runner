using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Coin, SpeedUp, OtherSkills}

public class Item : MonoBehaviour
{
    public ItemType type = ItemType.Coin;

    [SerializeField] private MeshRenderer[] itemMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (type == ItemType.Coin)
            {
                AudioManager.instance.PlayCoinSound();
                ScoreManager.currentCoin += 1;
                UIManager.instance.ChangeCoingText(ScoreManager.currentCoin.ToString());

                if (itemMesh != null)
                {
                    foreach (MeshRenderer item in itemMesh)
                    {
                        item.enabled = false;
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        if (itemMesh != null)
        {
            foreach (MeshRenderer item in itemMesh)
            {
                item.enabled = true;
            }
        }
    }

















}
