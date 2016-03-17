using UnityEngine;

//Author: J.Anderson



public class PlayerJump : MonoBehaviour
{
    PlayerCollisions playerCollisions;
    PlayerMovement playerMovement;
    PlayerRaycast playerRaycast;
    Rigidbody playerRigidbody;

    [Header("Set Jump Variables")]
    public float jumpHeight;
    public float force;
    public float gravity;

    public bool isJumping;


    void Awake()
    {
        playerRaycast = FindObjectOfType<PlayerRaycast>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollisions = GetComponent<PlayerCollisions>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Jump();
    }

    //void SetDownForce()
    //{
    //    //if (playerRaycast.downDistance > jumpHeight)
    //    //{
    //    //    print("jumped off ledge");
    //    //    playerRigidbody.velocity = new Vector3(playerMovement.movement / 2, -gravity / 2, 0);
    //    //}
    //    //if (isJumping)
    //    //{
    //    //    if (playerRaycast.downDistance > jumpHeight && !playerCollisions.isFalling)
    //    //    {
    //    //        print("player jump peak");
    //    //        playerRigidbody.velocity = new Vector3(playerMovement.movement, -gravity, 0);
    //    //    }

    //    //}
    //    //if (!isJumping && playerCollisions.isFalling)
    //    //{
    //    //    playerRigidbody.velocity = new Vector3(playerMovement.CheckMovement() / 2, -gravity, 0);
    //    //    print("player is falling");
    //    //}
    //}

    void Jump()
    {
        Vector3 playerJump = new Vector3(playerMovement.CheckMovement() / 2, jumpHeight, 0);

        if (Input.GetButtonDown("Jump") && playerCollisions.playerCanJump == true)
        {
            if (playerCollisions.isGrounded)
            {
                isJumping = true;
                playerCollisions.playerCanMove = false; // Needed to disable rigidbody.velocity in PlayerMovement to be able to 
                                                        // override it with this rigidbody.velocity.
                playerRigidbody.velocity = playerJump;
                playerRigidbody.AddForce(transform.up * force);
                playerCollisions.playerCanJump = false; // Stops player from jumping again until grounded.

            }
        }
    }



}
