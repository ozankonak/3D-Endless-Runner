using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static GameObject player;
    public bool canTurn { get; set; } = false;

    [Header("VFX")]
    [SerializeField] private GameObject waterSplash;
    [SerializeField] private GameObject sandVFX;
 
    private bool isDead = false;
    private Rigidbody playerRigid;
    private void Awake()
    {
        player = this.gameObject;
        playerRigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isDead)
        {
            if (other.gameObject.tag == "Fire")
            {
                FireDeath();
            }
            else if (other.gameObject.tag == "Obstacle")
            {
                ObstacleDeath();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Deactivation")
        {
            StartCoroutine(DeactivatePlatform(other.gameObject));
        }
        else if (other.gameObject.tag == "Activation" && GenerateWorld.lastPlatform.tag != "platformTSection")
        {
            GenerateWorld.RunDummy();
        }
        else if (other.gameObject.tag == "TurnPoint")
        {
            canTurn = true;
        }
        else if (other.gameObject.tag == "Sand" && !isDead)
        {
            SandDeath();
        }
        else if (other.gameObject.tag == "Water" && !isDead)
        {
            WaterDead();
        }
    }

    IEnumerator DeactivatePlatform(GameObject go)
    {
        yield return new WaitForSecondsRealtime(5f);
        if (GameManager.instance.status == GameStatus.GamePlay)
        go.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TurnPoint")
        {
            canTurn = false;
        }
    }

    #region Rigidbody

    private void StopRigidbody(float time)
    {
        Invoke("StopRigidWithTime", time);
    }

    private void StopRigidWithTime()
    {
        playerRigid.isKinematic = true;
    }

    #endregion

    #region Dead Types

    private void FireDeath()
    {
        isDead = true;
        AudioManager.instance.PlayPunchSound();

        GameObject go = Instantiate(sandVFX, transform.position, Quaternion.identity);
        Destroy(go, 2f);

        EventManager.TriggerEvent(EventManager.instance.playerDeadEvent);
    }

    private void ObstacleDeath()
    {
        isDead = true;
        AudioManager.instance.PlayPunchSound();

        GameObject go = Instantiate(sandVFX, transform.position, Quaternion.identity);
        Destroy(go, 2f);

        EventManager.TriggerEvent(EventManager.instance.playerDeadEvent);
    }

    private void SandDeath()
    {
        isDead = true;
        AudioManager.instance.PlayPunchSound();

        GameObject go = Instantiate(sandVFX, transform.position, Quaternion.identity);
        Destroy(go, 2f);

        StopRigidbody(0.05f);
        EventManager.TriggerEvent(EventManager.instance.playerDeadEvent);
    }


    private void WaterDead()
    {
        isDead = true;
        AudioManager.instance.PlayWaterSplashSound();

        GameObject go = Instantiate(waterSplash, transform.position, Quaternion.identity);
        Destroy(go, 2f);

        StopRigidbody(0.5f);
        EventManager.TriggerEvent(EventManager.instance.playerDeadEvent);
    }


    #endregion

}
