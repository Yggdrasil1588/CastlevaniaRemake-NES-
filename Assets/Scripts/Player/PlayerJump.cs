using UnityEngine;

//Author: J.Anderson



public class PlayerJump : MonoBehaviour
{
    PlayerCollisions playerCollisions;
    PlayerMovement playerMovement;
    Rigidbody playerRigidbody;

    [Header("Set Jump Variables")]
    public float jumpHeight;

    public bool isJumping;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollisions = GetComponent<PlayerCollisions>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Jump();
    }

    void Jump()
    {
        Vector3 playerJump = new Vector3(playerMovement.movement, jumpHeight, 0);

        if (Input.GetButtonDown("Jump") && playerCollisions.playerCanJump == true)
        {
            if (playerCollisions.isGrounded)
            {
                playerCollisions.playerCanMove = false;
                playerRigidbody.velocity = playerJump;
                playerCollisions.playerCanJump = false;
            }
        }
    }



}
