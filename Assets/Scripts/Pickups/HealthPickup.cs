using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class HealthPickup : MonoBehaviour
{
    PlayerHealthManager playerHealthManager;

    [SerializeField]
    int healthValue;

    void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        playerHealthManager.AddHealth(healthValue);
        Destroy(gameObject);
    }
}
