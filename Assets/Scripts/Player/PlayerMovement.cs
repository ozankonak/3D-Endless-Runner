using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Move Settings")]
    private float moveDistance = 4f;
    private float currentSpeed = 0;

    [Header("Turn Settings")]
    private float turnSpeed = 1000f;
    private bool canRotateLeft = true;
    private bool canRotateRight = true;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 200f;
    public bool canJump { get; set;} = true;
    public bool canCrouch { get; set; } = true;

    private Vector3 startPosition;

    private Rigidbody rigid;

    //Crouch Settings
    private CapsuleCollider playerCollider;
    private float startColliderHeight;
    private Vector3 startColliderCenter;
    private float crouchColliderHeight;
    private Vector3 crouchColliderCenter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //Crouch Settings
        playerCollider = GetComponent<CapsuleCollider>();
        startColliderHeight = playerCollider.height;
        startColliderCenter = playerCollider.center;
        crouchColliderHeight = playerCollider.height / 2;
        crouchColliderCenter = playerCollider.center / 2;
    }

    private void Start()
    {
        startPosition = transform.position;

        EventManager.StartListening(EventManager.instance.RotateLeftButton, RotatePlayerToLeft);
        EventManager.StartListening(EventManager.instance.RotateRightButton, RotatePlayerToRight);
        EventManager.StartListening(EventManager.instance.JumpButton, Jump);
        EventManager.StartListening(EventManager.instance.CrouchButton, Crouch);
        EventManager.StartListening(EventManager.instance.MoveButton, MoveThis);
    }


    #region Events


    #endregion

    #region Movement Settings

    private void MoveThis()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 10;

        Vector3 castPoint = Camera.main.ScreenToViewportPoint(mouse);

        //Clicking Left of Player
        if (castPoint.x < 0.5f)
        {
            transform.Translate(-moveDistance * Time.deltaTime * (0.5f - castPoint.x),0f,0f);
        }
        else if (castPoint.x > 0.5f)
        {
            transform.Translate(moveDistance * Time.deltaTime * (castPoint.x - 0.5f), 0f, 0f);
        }

    }

    #endregion

    #region Rotation Settings

    private void RotatePlayerToLeft()
    {
        if (canRotateLeft)
        {
            AudioManager.instance.PlayRotateSound();
            canRotateLeft = false;
            canRotateRight = false;
            StartCoroutine(RotateLeft());
        }
    }

    private void RotatePlayerToRight()
    {
        if (canRotateRight)
        {
            AudioManager.instance.PlayRotateSound();
            canRotateRight = false;
            canRotateLeft = false;
            StartCoroutine(RotateRight());
        }
    }


    IEnumerator RotateLeft()
    {
        Quaternion newRot = transform.rotation * Quaternion.Euler(0, -90f, 0);
        
         while (transform.rotation != newRot)
         {
             transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * turnSpeed);
             yield return new WaitForFixedUpdate();
         }
        canRotateLeft = true;
        canRotateRight = true;

        EventManager.TriggerEvent("Rotate");

        this.transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
    }

    IEnumerator RotateRight()
    {
        Quaternion newRot = transform.rotation * Quaternion.Euler(0, 90f, 0);

        while (transform.rotation != newRot)
        { 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * turnSpeed);
            yield return new WaitForFixedUpdate();
        }
        canRotateRight = true;
        canRotateLeft = true;

        EventManager.TriggerEvent("Rotate");

        this.transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
    }

    #endregion

    #region Jump

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            PlayerAnimations.instance.PlayJumpAnimation();
            AudioManager.instance.PlayJumpSound();
            rigid.AddForce(Vector3.up * jumpForce);
        }
    }

    #endregion

    #region Crouch

    private void Crouch()
    {
        if (canCrouch)
        {
            canCrouch = false;
            AudioManager.instance.PlayCrouchSound();
            PlayerAnimations.instance.PlayCrouchAnimation();
            StartCoroutine(DoCrouch());
        }
    }

    IEnumerator DoCrouch()
    {
        playerCollider.height = crouchColliderHeight;
        playerCollider.center = crouchColliderCenter;
        yield return new WaitForSecondsRealtime(1f);
        playerCollider.height = startColliderHeight;
        playerCollider.center = startColliderCenter;
        canCrouch = true;
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }
}
