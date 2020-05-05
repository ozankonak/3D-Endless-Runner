using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float scrollSpeed = -0.12f;

    private void FixedUpdate()
    {
        if (GameManager.instance.status == GameStatus.GamePlay)
        {
            this.transform.position += PlayerCollision.player.transform.forward * scrollSpeed;
        }
    }

}
 