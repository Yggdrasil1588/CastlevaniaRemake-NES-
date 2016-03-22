using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class PlayerCollisions : MonoBehaviour
{
    PlayerJump playerJump;

    public bool isGrounded;
    public bool isFalling;
    public bool playerCanJump;
    public bool playerCanMove;
    public bool onStairs;

    void Awake()
    {
        playerJump = GetComponent<PlayerJump>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stairs")
        {
            print("Stairs");
            onStairs = true;
            isGrounded = false;
        }
        playerCanMove = true;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Ledge"
            || collision.collider.tag == "Stairs")
        {
            isFalling = false;
            isGrounded = true;
            playerCanJump = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            isGrounded = false;

        if (collision.collider.tag == "Stairs")
        {
            onStairs = false;
            isGrounded = false;
        }

        if (collision.collider.tag == "Ledge")
        {
            isFalling = true;
            isGrounded = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {

    }
    public void OnTriggerStay(Collider other)
    {

    }

    public void OnTriggerExit(Collider other)
    {

    }
}
