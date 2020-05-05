using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    #region Event Names
    
    //GAME EVENTS
    public string gameOverEvent { get; private set; } = "GameOver";


    //PLAYER EVENTS
    public string playerResurrectEvent { get; private set; } = "Resurrect";
    public string playerDeadEvent { get; private set; } = "Dead";

    //PLATFORM EVENTS
    public string platformSpawnEvent { get; private set; } = "Spawn";

    //INPUTS
    public string GameStartButton { get; private set; } = "StarButton";
    public string MoveButton { get; private set; } = "Move";
    public string RotateLeftButton { get; private set; } = "RotateLeft";
    public string RotateRightButton { get; private set; } = "RotateRight";
    public string JumpButton { get; private set; } = "Jump";
    public string CrouchButton { get; private set; } = "Crouch";

    //ADS
    public string WatchAdsGetCoin { get; private set; } = "WatchAdsGetCoin";
    public string WatchAdsGetEnergy { get; private set; } = "WatchAdsGetEnergy";

    #endregion
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one GameEvents script on a Gameobject in your scene");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;

        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }





}
