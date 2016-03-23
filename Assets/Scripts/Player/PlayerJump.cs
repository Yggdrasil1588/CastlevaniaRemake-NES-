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
        string JUMP_AXIS = "Jump";
        [SerializeField]
        LayerMask ground;       

        public string JumpAxis()
        {
            return JUMP_AXIS;
        }

        public float JumpVelocity()
        {
            return jumpVelocity;
        }

        public LayerMask GroundLayerMask()
        {
            return ground;
        }

        public float DistToGrounded()
        {
            return distToGrounded;
        }
    }

    [System.Serializable]
    public class PhysicsSettings
    {
        [SerializeField]
        float downAcceleration = 0.75f;

        public float DownAcceleration()
        {
            return downAcceleration;
        }
    }
    public JumpSettings jumpSettings = new JumpSettings();
    public PhysicsSettings physicsSettings = new PhysicsSettings();

    PlayerMovement playerMovement;
    PlayerRaycast playerRaycast;
    PlayerCollisions playerCollisions;

    Vector3 velocity;

    public Vector3 Velocity()
    {
        return velocity;
    }

    void Start()
    {
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

    void Jump()
    {
        if (JumpInput() && Grounded()&&playerCollisions.playerCanJump)
        {
            playerCollisions.playerCanJump = false;
            playerMovement.velocity.y = jumpSettings.JumpVelocity();
        }
        else if (!JumpInput() && Grounded())
        {
            playerMovement.velocity.y = 0;
        }
        else
        {
            playerMovement.velocity.y -= physicsSettings.DownAcceleration();
        }
    }

    bool JumpInput()
    {
        return Input.GetButtonDown(jumpSettings.JumpAxis());
    }

    bool Grounded()
    {
        return Physics.Raycast(playerRaycast.downRay, jumpSettings.DistToGrounded(),jumpSettings.GroundLayerMask());
    }



}
