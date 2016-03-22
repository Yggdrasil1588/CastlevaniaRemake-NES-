using UnityEngine;

//Author: J.Anderson

public class PlayerRaycast : MonoBehaviour
{
    public RaycastHit playerRaycastOutHitForwards;
    public RaycastHit playerRaycastOutHitBackwards;
    public RaycastHit playerRaycastOutHitDown;

    public Ray downRay;

    public bool forwardRayIsHitting;
    public bool backwardRayIsHitting;
    public bool downRayIsHitting;

    public float forwardDistance;
    public float backwardDistance;
    public float downDistance;

    public string backRayTag;
    public string downRayTag;
    public string forwardRayTag;

    void Awake()
    {
        Update(); // In awake to get rid of null reference error on startup. 
    }

    void Update()
    {
        PlayerRaycastOut();
    }

    void PlayerRaycastOut()
    {

        Ray forwardsRay = new Ray(gameObject.transform.position, gameObject.transform.forward);
        if (Physics.Raycast(forwardsRay, out playerRaycastOutHitForwards))
        {
            forwardRayIsHitting = true;
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red);
            forwardDistance = playerRaycastOutHitForwards.distance;
        }
        else forwardRayIsHitting = false;

        Ray backwardsRay = new Ray(gameObject.transform.position, -gameObject.transform.forward);
        if (Physics.Raycast(backwardsRay, out playerRaycastOutHitBackwards))
        {
            backwardRayIsHitting = true;
            Debug.DrawRay(gameObject.transform.position, -gameObject.transform.forward, Color.red);
            backwardDistance = playerRaycastOutHitBackwards.distance;
        }
        else
            backwardRayIsHitting = false;

        downRay = new Ray(gameObject.transform.position, -gameObject.transform.up);
        if (Physics.Raycast(downRay, out playerRaycastOutHitDown))
        {
            Debug.DrawRay(gameObject.transform.position, -gameObject.transform.up, Color.red);
            downDistance = playerRaycastOutHitDown.distance;
        }
    }

    public string GetDownRayTag()
    {
        return playerRaycastOutHitDown.transform.gameObject.tag;
    }
}
