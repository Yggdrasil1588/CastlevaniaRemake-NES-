using UnityEngine;

//Author: J.Anderson

/// <summary>
/// needs to be tweaked for current project
/// <summary>

public class PlayerCamera : MonoBehaviour
{

    Quaternion cameraRotation;
    public GameObject forwardCameraTarget;
    [HideInInspector]
    public GameObject cameraTarget;
    Vector3 cameraPosOffset;

    float velocityX;

    [Header("Camera Position")]
    [Range(-15, 15)]
    public float cameraYPos = 3.5f;
    [Range(-15, 15)]
    public float cameraZPos = 10f;
    [Range(-15, 15)]
    public float cameraXPos = 6f;
    public float dampTimeX = 0.4f;

    public bool facingLeft;

    void Start()
    {
        cameraTarget = forwardCameraTarget;
        //ledgeClimb = GameObject.FindGameObjectWithTag("Player").GetComponent<LedgeClimb>();
    }

    void FixedUpdate()
    {
        IsFlipped();
        CameraPos();
    }

    public void CameraLookAt(GameObject target)
    {
        gameObject.transform.LookAt(target.transform.position);
    }

    void CameraPos()
    {
        // Setup camera offset in inspector
        cameraPosOffset.y = cameraTarget.transform.position.y + cameraYPos;
        cameraPosOffset.z = cameraTarget.transform.position.z + cameraZPos;
        cameraPosOffset.x = cameraTarget.transform.position.x + cameraXPos;

        // Set the smooth between camera target switch
        float smoothDampX = Mathf.SmoothDamp(gameObject.transform.position.x, cameraTarget.transform.position.x, ref velocityX, dampTimeX);

        // camera rotation
        cameraRotation.y = 1.0f; 
        cameraRotation.w = 0.0f;

        transform.rotation = cameraRotation;
        transform.position = new Vector3(smoothDampX, transform.position.y, transform.position.z);
    }

    void IsFlipped()
    {
        if (Input.GetAxis("Horizontal") <= -.001)
        {
            facingLeft = false;
        }
        if (Input.GetAxis("Horizontal") >= .001)
        {
            facingLeft = true;
        }
    }
}


