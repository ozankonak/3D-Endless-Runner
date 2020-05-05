using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private PlayerCollision myCollision;

    private void Awake()
    {
        myCollision = FindObjectOfType<PlayerCollision>();
    }

    private void OnGUI()
    {
        StartGameInputs();
        GamePlayInputs();
    }


    private void StartGameInputs()
    {
        if (GameManager.instance.status == GameStatus.Start)
        {
            //Start The Game
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.TriggerEvent(EventManager.instance.GameStartButton);
            }
        }
    }

    private void GamePlayInputs()
    {
        if (GameManager.instance.status == GameStatus.GamePlay)
        {
            //Rotate Player To Left
            if (SwipeManager.IsSwipingLeft() && myCollision.canTurn)
            {
                EventManager.TriggerEvent(EventManager.instance.RotateLeftButton);
            }
            //Rotate Player To Right
            else if (SwipeManager.IsSwipingRight() && myCollision.canTurn)
            {
                EventManager.TriggerEvent(EventManager.instance.RotateRightButton);
            }
            //Jump Player
            else if (SwipeManager.IsSwipingUp())
            {
                EventManager.TriggerEvent(EventManager.instance.JumpButton);
            }
            else if (SwipeManager.IsSwipingDown())
            {
                EventManager.TriggerEvent(EventManager.instance.CrouchButton);
            }
            else if (Input.GetMouseButton(0))
            {
                EventManager.TriggerEvent(EventManager.instance.MoveButton);
            }

        }
    }

}
