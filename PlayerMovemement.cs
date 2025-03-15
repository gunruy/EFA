using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovemement : MonoBehaviour
{

    //movement stats
    [Header("Movement Stats")]
    public float walkingSpeed = 5f;
    public float silentWalkSpeed = 3.5f;
    public float crouchWalkSpeed = 2f;
    public float jumpHeight = 0.8f;
    public float climbingSpeed = 1f;
    public bool allowMidAirMovement = false;
    public float gravityValue = -24;


    //player condition
    private bool isClimbing;
    private bool isSilentWalkInput;
    private bool isJumpingInput;
    private bool groundedPlayer;


    private float playerHeight;
    private Vector3 playerVelocity;
    private Vector3 moveDirection;

    [Tooltip("Climable objelerin ayr�m�n� yapmak i�in se�ilen layer ismi.")]
    public LayerMask climable;


    //components
    private CharacterController controller;
    private new Renderer renderer; // 'new' anahtar kelimesi eklendi
    private Transform orientation;

    PhotonView view;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        orientation = transform.Find("Orientation").GetComponent<Transform>();

        renderer = transform.Find("Capsule").GetComponent<Renderer>();
        playerHeight = renderer.bounds.size.y;

        view = GetComponent<PhotonView>();

    }


    private void Update()
    {
        GetInput();
        climbCheck();

    }

    void FixedUpdate()
    {

        if (isClimbing)
        {
            Move(climbingSpeed);
        }
        else if (isSilentWalkInput)
        {
            Move(silentWalkSpeed);
        }
        else
        {
            Move(walkingSpeed);
        }
    }



    //�st�nde durulan objenin climable(t�rman�labilir) bir obje olup olmad���n� kontrol eder.
    private void climbCheck()
    {
        isClimbing = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, climable); //sor; neden playerheight� d�zg�n hesaplayam�yor?
    }


    #region Move Function

    private void GetInput()
    {
        if (view.IsMine)
        {
            if (groundedPlayer || allowMidAirMovement) //z�plad���nda veya havadayken hareket y�n�n� de�i�tirmeyi engellemek i�in k�s�tlama.
            {
                moveDirection = Input.GetAxis("Vertical") * orientation.forward + Input.GetAxis("Horizontal") * orientation.right;
                moveDirection = moveDirection.normalized;
            }
            isJumpingInput = Input.GetButton("Jump");
            isSilentWalkInput = Input.GetKey(KeyCode.LeftShift);
        }
    }

    private void Move(float moveSpeed)
    {
        if (view.IsMine)
        {
            groundedPlayer = controller.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (groundedPlayer)
            {
                moveDirection = Input.GetAxis("Vertical") * orientation.forward + Input.GetAxis("Horizontal") * orientation.right;
                moveDirection = moveDirection.normalized;
            }

            if (isJumpingInput && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.fixedDeltaTime;

            controller.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
            controller.Move(playerVelocity * Time.fixedDeltaTime);
        }
    }
    #endregion

}
