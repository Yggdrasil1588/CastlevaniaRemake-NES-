using UnityEngine;

//Author: J.Anderson



public class PlayerJump : MonoBehaviour
{
    [System.Serializable]
    public class JumpSettings
    {
        [SerializeField]
        float jumpVelocity = 25;
        [SerializeField]
        float distToGrounded = 0.8f;
        [SerializeField]
        string jumpAxis = "Jump";
        [SerializeField]
        LayerMask ground;

        public string getJumpAxis
        {
            get
            {
                return jumpAxis;
            }
        }
        public float getJumpVelocity
        {
            get
            {
                return jumpVelocity;
            }
        }
        public int getGroundLayerMask
        {
            get
            {
                return (1 << ground);
            }
        } // returns ground mask as a bitwise operation (use ~ infront of property when setting in  
                                         // script to reverse to everything but chosen layer/s)
        public float getDistToGrounded
        {
            get
            {
                return distToGrounded;
            }
        }
    }
    [System.Serializable]
    public class PhysicsSettings
    {
        [SerializeField]
        float downAcceleration = 0.75f;
        public float getDownAcceleration
        {
            get
            {
                return downAcceleration;
            }
        }
    }

    public JumpSettings jumpSettings = new JumpSettings();
    public PhysicsSettings physicsSettings = new PhysicsSettings();

    PlayerMovement playerMovement;
    PlayerRaycast playerRaycast;
    PlayerCollisions playerCollisions;


    void Start()
    {
        // all of these check for the components and throw up specific errors if not attached rather than 
        // unity default null errors.
        if (GetComponent<PlayerMovement>())
            playerMovement = GetComponent<PlayerMovement>();
        else
            Debug.LogError("Player movement script not attached to object");
        if (GetComponentInChildren<PlayerRaycast>())
            playerRaycast = GetComponentInChildren<PlayerRaycast>();
        else
            Debug.LogError("Player raycast object and script not in player hierachy");
        if (GetComponent<PlayerCollisions>())
            playerCollisions = GetComponent<PlayerCollisions>();
        else
            Debug.LogError("Player collisions script not attached to object");
    }

    void FixedUpdate()
    {
        Jump();
    }

    bool Grounded()
    {
        // raycast to check if grounded based on distance to ground property
        return Physics.Raycast(playerRaycast.downRay, jumpSettings.getDistToGrounded, ~jumpSettings.getGroundLayerMask);
    }

    bool JumpInput()
    {
        return Input.GetButtonDown(jumpSettings.getJumpAxis);
    }

    void Jump()
    {
        // jump set through rigidbody
        if (JumpInput() && Grounded() && playerCollisions.playerCanJump)
        {
            playerCollisions.playerCanJump = false;
            playerMovement.velocity.y = jumpSettings.getJumpVelocity;
        }
        else if (!JumpInput() && Grounded())
        {
            playerMovement.velocity.y = 0;
        }
        else
        {
            playerMovement.velocity.y -= physicsSettings.getDownAcceleration;
        }
    }
}
