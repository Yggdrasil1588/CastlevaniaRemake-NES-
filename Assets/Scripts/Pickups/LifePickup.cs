using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class LifePickup : MonoBehaviour
{
    PlayerHealthManager playerHealthManager;

    [SerializeField]
    int addLife;

    void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealthManager.AddLife(addLife);
            Destroy(gameObject);
        }
        if (other.tag == "Ground")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
