using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class EnemyMove : MonoBehaviour
{

    public Transform wallCheck;
    Rigidbody enemyRigidbody;
    RaycastHit rayDown;

    int obstacleLayerMask = (1 << 9);

    public string enemyTag;

    public bool atEdge, hittingWall, isGrounded, climbingUpStairs;
    bool moveRight;

    public float moveSpeed;
    public float rayChkDistFwd;
    public float rayChkDistDwn;
    float downRayDist;
    float grav = 0;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        EnemyRaycast();
        EdgeCheck();
        EnemyMovement();
    }

    void EnemyMovement()
    {
        // Set for downslopes or stairs, adds a variable to Y vector to push it down when travelling down slopes or stairs. 
        // If the grav variable is exactly the same as the ray check down distance variable the travel is smooth.  
        if (!isGrounded && downRayDist > 0.6f) // 0.7f seems to be the sweet spot to get it hovering just above collision 
            grav = rayChkDistDwn;
        else if (isGrounded)
            grav = 0;

        // speed increased when climbing stairs to appear slightly slower then normal but not as slow as it was when fighting 
        // rigidbody physics.   
        if (climbingUpStairs && isGrounded)
            moveSpeed = 5;
        else
            moveSpeed = 3;

        // direction based on raycheck
        if (moveRight)
        {
            transform.rotation = Quaternion.identity;
            enemyRigidbody.velocity = new Vector3(moveSpeed, -grav, 0);
        }
        else if (!moveRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            enemyRigidbody.velocity = new Vector3(-moveSpeed, -grav, 0);
        }
    }

    void EdgeCheck()
    {
        if (atEdge)
        {
            moveRight = !moveRight;
        }
        if (hittingWall)
        {
            moveRight = !moveRight;
        }
    }

    // uses a transform point to check for obstacles and platform edges
    void EnemyRaycast()
    {
        Ray dwnRay = new Ray(wallCheck.transform.position, -wallCheck.transform.up); // set as a ray so I could grab down dist from hit
        atEdge = !(Physics.Raycast(dwnRay, rayChkDistDwn));
        // grabs the down ray distance to use for adding a downward velocity on downslopes
        if (Physics.Raycast(dwnRay, out rayDown))
            downRayDist = rayDown.distance;

        hittingWall = (Physics.Raycast(wallCheck.transform.position, wallCheck.transform.right, rayChkDistFwd, obstacleLayerMask));
    }

    #region Collisions and Triggers
    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Ledge")
            isGrounded = false;
        if (collision.collider.tag == "Stairs")
        {
            climbingUpStairs = false;
            isGrounded = false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Ledge")
            isGrounded = true;
        if (collision.collider.tag == "Stairs")
        {
            climbingUpStairs = true;
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {
            moveRight = !moveRight;
        }
    } 
    #endregion
}
