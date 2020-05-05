using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const int maxHealth = 3;
    public static int currentHealth { get; set; } = maxHealth;

    private void Start()
    {
        EventManager.StartListening(EventManager.instance.playerDeadEvent, TakeDamage);
        UIManager.instance.SetActiveHealths(currentHealth);
    }

    private void TakeDamage()
    {
        PlayerAnimations.instance.PlayDeadAnimation();

        currentHealth--;
        UIManager.instance.SetActiveHealths(currentHealth);

        if (currentHealth < 1)
        {
            currentHealth = 0;
            EventManager.TriggerEvent(EventManager.instance.gameOverEvent);
        }
        else
        {
            EventManager.TriggerEvent(EventManager.instance.playerResurrectEvent);
        }
    }

}
