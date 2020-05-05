using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public static GameObject dummyTraveller;
    public static GameObject lastPlatform;

    public static string lastPlatformTag;

    private void Awake()
    {
        dummyTraveller = new GameObject("Dummy");
    }

    private void Start()
    {
        CreateWorld(2);

        EventManager.StartListening("Rotate", RotateWorldToPlayerForward);
        
    }


    public static void RunDummy()
    {
        GameObject platform = Pool.instance.GetRandom();

        lastPlatformTag = platform.gameObject.tag;

        EventManager.TriggerEvent(EventManager.instance.platformSpawnEvent);

        if (platform == null) return;

        if (lastPlatform != null)
        {
            if (lastPlatform.tag == "platformTSection")
            {
                dummyTraveller.transform.position = lastPlatform.transform.position + PlayerCollision.player.transform.forward * 40;
            }
            else
            {
                dummyTraveller.transform.position = lastPlatform.transform.position + PlayerCollision.player.transform.forward * 10;
            }

            if (lastPlatform.tag == "stairsUp")
            {
                dummyTraveller.transform.Translate(0, 5, 0);
            }
        }

        lastPlatform = platform;
        platform.SetActive(true);
        platform.transform.position = dummyTraveller.transform.position;
        platform.transform.rotation = dummyTraveller.transform.rotation;

        if (platform.tag == "stairsDown")
        {
            dummyTraveller.transform.Translate(0, -5, 0);
            platform.transform.Rotate(0, 180, 0);
            platform.transform.position = dummyTraveller.transform.position;
        }

    }

    private void RotateWorldToPlayerForward()
    {
       dummyTraveller.transform.forward = -PlayerCollision.player.transform.forward;
       FindObjectOfType<PlayerCollision>().canTurn = false;
       CreateWorld(2);
    }


    private void CreateWorld(int value)
    {
        for (int i = 0; i < value; i++)
        {
            RunDummy();

            if (lastPlatformTag == "platformTSection")
                break;
        }
    }
}
