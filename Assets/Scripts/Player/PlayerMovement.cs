using UnityEngine;

//Author: J.Anderson

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    Rigidbody playerRb;
    PlayerCollisions playerCollisions;
    PlayerRaycast playerRaycast;

    float isMovingHorizontal;
    [HideInInspector]
    public float movement;
    public float moveSpeed = 8;
    public float downForce;
    public float downDistance;

    public bool facingLeft;
    public bool playerCanMove; // To stop player input when needed

    Vector3 moveVec3;
    #endregion

    void Awake()
    {
        playerCollisions = GetComponent<PlayerCollisions>();
        playerRaycast = FindObjectOfType<PlayerRaycast>();
    }

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerCanMove = true;
    }

    void FixedUpdate()
    {
        SetVariables();
        Player_Movement();
        PlayerFlip();
    }

    // Method to set booleans.
    void SetVariables()
    {
        isMovingHorizontal = Input.GetAxis("Horizontal");

        // Check to see direction player is facing
        if (isMovingHorizontal <= -.000000001)
            facingLeft = true;
        else if (isMovingHorizontal >= .000000001)
            facingLeft = false;
    }

    void Player_Movement()
    {
        movement = (-isMovingHorizontal) * moveSpeed;
        float gravity = 0;

        // Sets the gravity if player isn't grounded, stops conflict of there being too much gravity while cimbing 
        // up slopes and too little while climbing down.
        if (playerCollisions.isGrounded)
            gravity = 0;
        else if (!playerCollisions.isGrounded && playerRaycast.downDistance < 3)
            gravity = -downForce;
        else if (!playerCollisions.isGrounded && playerRaycast.downDistance > 2.4)
            gravity = -downForce * 5;


        moveVec3 = new Vector3(movement, gravity, 0);

        // Using Rigidbody.velocity rather than transform.translate to get past the conflict I had with rigidbody fighting
        // the transform while going up and down slopes. This also gives move control over downward force when falling
        // and jumping.
        if (playerCollisions.playerCanMove)
            playerRb.velocity = moveVec3;
    }

    // Scaleflips player based on the facingLeft bool which is determined by the current or last
    // directional keypress.
    void PlayerFlip()
    {
        float normalScale = gameObject.transform.localScale.y; // Always references off of Y scale to keep its 1:1:1 ratio 
                                                               // could also use the X scale because it's not affected 
                                                               // by the Z scale flip.
        if (playerCanMove)
        {
            if (facingLeft)
            {
                gameObject.transform.localScale = new Vector3(normalScale, normalScale, normalScale);
                playerRaycast.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0); // Flips ray holer
            }
            else if (!facingLeft)
            {
                gameObject.transform.localScale = new Vector3(normalScale, normalScale, -normalScale);
                playerRaycast.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0); ; // Flips ray holer
            }
        }

    }

}
