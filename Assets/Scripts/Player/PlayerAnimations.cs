using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations instance;

    private Animator playerAnim;

    private string runHash = "isRunning";
    private string jumpHash = "Jump";
    private string deadHash = "Dead";
    private string crouchHash = "Crouch";

    private void Awake()
    {
        instance = this;
        playerAnim = GetComponent<Animator>();
    }

    public void PlayRunAnimation(bool value)
    {
        playerAnim.SetBool(runHash, value);
    }

    public void PlayJumpAnimation()
    {
        playerAnim.SetTrigger(jumpHash);
    }

    public void PlayDeadAnimation()
    {
        playerAnim.SetTrigger(deadHash);
    }

    public void PlayCrouchAnimation()
    {
        playerAnim.SetTrigger(crouchHash);
    }

}
