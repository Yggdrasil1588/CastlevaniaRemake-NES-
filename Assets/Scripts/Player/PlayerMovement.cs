using UnityEngine;

//Author: J.Anderson

public class PlayerMovement : MonoBehaviour
{
    // Variables displayed in the inspector
    [System.Serializable]
    public class MoveSettings
    {
        #region Fields
        [SerializeField]
        float forwardForce = 250;
        [SerializeField]
        string moveAxis = "Horizontal";
        [SerializeField]
        string laneAxis = "Vertical";
        #endregion
        #region Properties
        public string getMoveAxis
        {
            get
            {
                return moveAxis;
            }
        }
        public string getLaneAxis
        {
            get
            {
                return laneAxis;
            }
        }
        public float getForwardForce
        {
            get
            {
                return forwardForce;
            }
        }
        #endregion

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
    public Vector3 velocity;

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
        if (Input.GetAxis(moveSettings.getLaneAxis) < 0)
        {
            if (lanes.Lane1() != null) // checks if transform is available
            {
                transTemp = lanes.Lane1();
                laneChange.ChangeLanes(gameObject, transTemp);
            }
            else
                Debug.LogError("Lane1 transform not referenced in inspector"); // throws an error if not available
        }

        if (Input.GetAxis(moveSettings.getLaneAxis) > 0)
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

    void SetFlipState()
    {
        isMovingHorizontal = Input.GetAxis(moveSettings.getMoveAxis);

        // Check to see direction player is facing
        if (isMovingHorizontal <= -.000000001)
            facingLeftSet = true;
        else if (isMovingHorizontal >= .000000001)
            facingLeftSet = false;
    }

    void Player_Movement()
    {
        print(playerRb.velocity);
        playerRb.velocity = (velocity); // the movement is base off adding force to zero velocity, this allows the jump to be added
                                        // to the velocity and caps the speed based on the force; 
        if (playerCanMove)
        {
            // deadzone is set up for rigidbody movement so that a zero point can be set when there is no player input
            // this seemed to fix the character not being able to stop while climbing up stairs
            if (Mathf.Abs(isMovingHorizontal) > inputDelay) // if the input variable is greater then the deadzone then player can move
            {
                // both of these if statements output a +forward movement, this is needed because the when the character rotates
                // the transform is facing forward but the input is -forward
                // both ignore the mass of the object. 
                if (isMovingHorizontal > 0) // if input is + then it stays +
                    playerRb.AddForce(transform.forward * (isMovingHorizontal * moveSettings.getForwardForce),ForceMode.Acceleration);
                if (isMovingHorizontal < 0)// if input is - then it's set as +
                    playerRb.AddForce(transform.forward * (-isMovingHorizontal * moveSettings.getForwardForce), ForceMode.Acceleration);
            }
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

    void PlayerFlip()
    {
        // rotates player based on the facingLeft bool which is determined by the current or last
        // directional keypress.
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