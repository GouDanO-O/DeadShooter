using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float playerMoveSpeed = 0f;

    public bool isGrounded;

    public float playerWalkSpeed = 5.0f;

    public float playerRunSpeed = 8.0f;

    public float playerTurnSpeed = 1.0f;

    public float jumpHeight = 0.3f;

    public CharacterController playerCharacterController;

    public Transform groundCheck;

    public LayerMask groundMask;

    private Animator playerAnimator;

    private AudioSource playerAudio;

    private float groundDistance = 0.4f;

    private float gravity = -9.8f;

    private Vector3 velocity;

    public AudioClip[] playerMovementAudioClip;

    private bool jumpingBool;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementController();
        PlayerMovementAudioController();
    }
    private void PlayerMovementController()
    {
        playerMoveSpeed = 0;        
        playerAnimator.SetFloat("Speed", 0f);
        playerAnimator.SetBool("IsWalking", false);
        playerAnimator.SetBool("IsRunning", false);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        bool isCollision = Physics.GetIgnoreLayerCollision(7, 9);
        Debug.Log(isCollision);
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded&&velocity.y<0)
        {
            velocity.y = -2f;
        }
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight *-2f* gravity);
        }
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMove = transform.right * hor + transform.forward * ver;
        if(hor!=0||ver!=0)
        {
            playerMoveSpeed = playerWalkSpeed;
            playerAnimator.SetFloat("Speed", 3.0f);
            playerAnimator.SetBool("IsWalking", true);
        }
        playerCharacterController.Move(playerMove * Time.deltaTime * playerMoveSpeed);

        velocity.y += gravity * Time.deltaTime;
        playerCharacterController.Move(velocity * Time.deltaTime);

        if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.LeftShift))
        {
            playerMoveSpeed = playerRunSpeed;
            playerCharacterController.Move(playerMove * Time.deltaTime * playerMoveSpeed);
            playerAnimator.SetFloat("Speed", 5.0f);
            playerAnimator.SetBool("IsRunning", true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            playerMoveSpeed = 0;
            playerAnimator.SetFloat("Speed",0f);
            playerAnimator.SetBool("IsRunning", false);
            playerAnimator.SetBool("IsWalking", false);
        }
    }
    private void PlayerMovementAudioController()
    {
        if (playerMoveSpeed == playerWalkSpeed)
        {
            playerAudio.Pause();
            playerAudio.clip = playerMovementAudioClip[0];
            playerAudio.Play();
        }
        else if (playerMoveSpeed == playerRunSpeed)
        {
            playerAudio.Pause();
            playerAudio.clip = playerMovementAudioClip[1];
            playerAudio.Play();
        }
        if (playerMoveSpeed == 0)
        {
            playerAudio.Stop();
        }
        if (isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            playerAudio.Stop();
            AudioSource.PlayClipAtPoint(playerMovementAudioClip[3], transform.position, 1.0f);
        }
    }
}
