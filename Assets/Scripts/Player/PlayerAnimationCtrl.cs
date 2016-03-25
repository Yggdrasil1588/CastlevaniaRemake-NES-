using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class PlayerAnimationCtrl : MonoBehaviour 
{
    PlayerCollisions pColl;
    Animator playerAnimator;
    Rigidbody playerRb;

    float playerMove;
    bool playerJump;
    bool playerThrow;
    bool playerGrounded;

    void Start()
    {
        pColl = gameObject.GetComponent<PlayerCollisions>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        AnimVars();
        AnimationCtrl();
        playerGrounded = pColl.isGrounded;
    }

    void AnimVars()
    {
        playerMove = playerRb.velocity.x;
        if (playerMove < 0)
            playerMove = -playerMove;

        if (Input.GetButton("Jump"))
            playerJump = true;
        else
            playerJump = false;

        if (Input.GetButton("Fire2"))
            playerThrow = true;
        else
            playerThrow = false;
    }

    void AnimationCtrl()
    {
        playerAnimator.SetFloat("Move", playerMove);
        playerAnimator.SetBool("Jump", playerJump);
        playerAnimator.SetBool("Throw", playerThrow);
        playerAnimator.SetBool("Grounded", playerGrounded);
    }
}
