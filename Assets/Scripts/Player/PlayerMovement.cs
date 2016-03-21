using UnityEngine;

//Author: J.Anderson

public class PlayerMovement : MonoBehaviour
{
    // Variables displayed in the inspector
    [System.Serializable]
    public class MoveSettings
    {
        [SerializeField]
        float moveSpeed = 5;
        [SerializeField]
        float forwardForce = 20;
        [SerializeField]
        float smoothTime = 0.75f;

        // references private variables as public methods so that they can be read or written externally
        // depending on what's needed.  
        public float ZeroSmoothTime()
        {
            return smoothTime;
        }
        public float ForwardForce()
        {
            return forwardForce;
        }
        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }
        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    } // all variables for movement
    [System.Serializable]
    public class Lanes
    {
        [SerializeField]
        Transform lane1;
        [SerializeField]
        Transform lane2;

        public Transform Lane1()
        {
            return lane1;
        }
        public Transform Lane2()
        {
            return lane2;
        }

    } // transforms for lane change

    public MoveSettings moveSettings = new MoveSettings(); // sets class reference
    public Lanes lanes = new Lanes(); // sets class reference


    // Internal variables
    Transform transTemp = null; // temp transform to hold lane change transform information
    float inputDelay = 0.1f; // deadzone for character movement input
    float isMovingHorizontal;
    float velocityRef; // reference variable for Mathf.SmoothDamp
    bool facingLeftSet; // sets if the character is facing left
    bool playerCanMove // grabs bool from playerCollisions
    {
        get
        {
            return playerCollisions.playerCanMove;
        }
    }

    // Componenets
    LaneChange laneChange; // script containing the lane changing method
    Rigidbody playerRb;
    PlayerCollisions playerCollisions; // script containing all player collisions
    public PlayerRaycast playerRaycast; // script containing all player raycast data (reference from inspector)

    // Read only variables
    public bool facingLeftCheck
    {
        get
        {
            return facingLeftSet;
        }
    } // read only bool to check if facing left externally

    void Awake()
    {
        laneChange = GetComponent<LaneChange>();
        playerCollisions = GetComponent<PlayerCollisions>();
    }

    void Start()
    {
        if (GetComponent<Rigidbody>())
            playerRb = gameObject.GetComponent<Rigidbody>();
        else
            Debug.LogError("Character has no rigidbody");
    }

    void Update()
    {
        ChangeLanes();
        GravityToggle();
        SetFlipState();
        PlayerFlip();
    }

    void ChangeLanes()
    {
        // snaps the player z transform into one of two lanes depending on input
        if (Input.GetAxis("Vertical") < 0)
        {
            if (lanes.Lane1() != null) // checks if transform is available
            {
                transTemp = lanes.Lane1();
                laneChange.ChangeLanes(gameObject, transTemp);
            }
            else
                Debug.LogError("Lane1 transform not referenced in inspector"); // throws an error if not available
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            if (lanes.Lane2() != null)
            {
                transTemp = lanes.Lane2();
                laneChange.ChangeLanes(gameObject, transTemp);
            }
            else
                Debug.LogError("Lane2 transform not referenced in inspector");
        }
    }

    void FixedUpdate()
    {
        Player_Movement();
    }

    // Method to set booleans.
    void SetFlipState()
    {
        isMovingHorizontal = Input.GetAxis("Horizontal");

        // Check to see direction player is facing
        if (isMovingHorizontal <= -.000000001)
            facingLeftSet = true;
        else if (isMovingHorizontal >= .000000001)
            facingLeftSet = false;
    }

    void Player_Movement()
    {
        if (playerCanMove)
        {
            float currentXVelocity = playerRb.velocity.x; // temp float for holding the current x axis velocity of the character

           
            // deadzone is set up for rigidbody movement so that a zero point can be set when there is no player input
            // this seemed to fix the character not being able to stop while climbing up stairs
            if (Mathf.Abs(isMovingHorizontal) > inputDelay) // if the input variable is greater then the deadzone then player can move
            {
                SpeedCap(); // see method for info

                // both of these if statements output a +forward movement, this is needed because the when the character rotates
                // the transform is facing forward but the input is -forward 
                if (isMovingHorizontal > 0) // if input is + then it stays +
                    playerRb.AddForce(transform.forward * (isMovingHorizontal * moveSettings.ForwardForce()));
                if (isMovingHorizontal < 0)// if input is - then it's set as +
                    playerRb.AddForce(transform.forward * (-isMovingHorizontal * moveSettings.ForwardForce()));
            }
            // when no input is detected Mathf.SmoothDamp grabs the last value from currentXVelocity and interpolates between
            // that value and zero by the smoothTime value set in ZeroSmoothTime()
            else
            {
                float smoothToZero = Mathf.SmoothDamp(currentXVelocity, 0.0f, ref velocityRef, moveSettings.ZeroSmoothTime());
                playerRb.velocity = new Vector3(smoothToZero, playerRb.velocity.y, playerRb.velocity.z);
            }
            Debug.Log(playerRb.velocity);
        }
    }

    void SpeedCap()
    {
        // Stops the AddForce method from increasing the characters velocity over the set move speed
        // if current velocity is greater than move speed then current velocity equals move speed
        if (playerRb.velocity.x > moveSettings.GetMoveSpeed())
        {
            Vector3 speedCap = new Vector3(moveSettings.GetMoveSpeed(), playerRb.velocity.y, playerRb.velocity.z);
            playerRb.velocity = speedCap;
        }
        if (playerRb.velocity.x < -moveSettings.GetMoveSpeed())
        {
            Vector3 speedCap = new Vector3(-moveSettings.GetMoveSpeed(), playerRb.velocity.y, playerRb.velocity.z);
            playerRb.velocity = speedCap;
        }
    }

    void GravityToggle()
    {
        // turns off gravity while climbing starts so the player doesn't slide back down
        if (playerCollisions.onStairs)
        {
            playerRb.useGravity = false;
            playerCollisions.playerCanJump = false;
        }
        else
        {
            playerRb.useGravity = true;
            playerCollisions.playerCanJump = true;
        }
    }

    public float CheckMovement()
    {
        return playerRb.velocity.x;
    }// reference to current player velocity for jumping

    // rotates player based on the facingLeft bool which is determined by the current or last
    // directional keypress.
    void PlayerFlip()
    {
        if (playerCanMove)
        {
            if (facingLeftCheck)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0); // flips character
                playerRaycast.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0); // Flips ray holder
            }
            else if (!facingLeftCheck)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 270, 0); // flips character
                playerRaycast.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0); ; // Flips ray holder
            }
        }
    }
}