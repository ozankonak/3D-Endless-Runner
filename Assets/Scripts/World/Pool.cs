using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class Pool : MonoBehaviour
{
    public static Pool instance;

    [Header("Normal Platforms")]
    public List<PoolItem> platformNormal;
    [Header("Split Platforms")]
    public List<PoolItem> platformSplit;
    [Header("Thin Platforms")]
    public List<PoolItem> platformThin;
    [Header("Stairs Up Platforms")]
    public List<PoolItem> platformStairsUp;
    [Header("Stairs Down Platforms")]
    public List<PoolItem> platformStairsDown;
    [Header("T Platforms")]
    public List<PoolItem> platformTSection;

    [HideInInspector]
    public List<GameObject> pooledItems;

    private void Awake()
    {
        instance = this;

        pooledItems = new List<GameObject>();

        InitialPool();
    }

    public GameObject GetRandom()
    {
        RandomUtils.Shuffle(pooledItems);

        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy)
            {
                return pooledItems[i];
            }
        }

        foreach (PoolItem item in platformNormal)
        {
            if (item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        foreach (PoolItem item in platformSplit)
        {
            if (item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        foreach (PoolItem item in platformThin)
        {
            if (item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        foreach (PoolItem item in platformStairsUp)
        {
            if (item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        foreach (PoolItem item in platformStairsDown)
        {
            if (item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        foreach (PoolItem item in platformTSection)
        {
            if (item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }



        return null;
    }


    private void InitialPool()
    {
        AddPoolItems(platformNormal);
        AddPoolItems(platformSplit);
        AddPoolItems(platformThin);
        AddPoolItems(platformTSection);
        AddPoolItems(platformStairsUp);
        AddPoolItems(platformStairsDown);
    }

    private void AddPoolItems(List<PoolItem> currentPlatforms)
    {
        foreach (PoolItem item in currentPlatforms)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }


}

public static class RandomUtils
{
    public static System.Random r = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
