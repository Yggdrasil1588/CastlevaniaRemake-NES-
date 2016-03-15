using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class PlayerHealthManager : MonoBehaviour 
{

    public int playerHealth;


    void HealthCheck()
    {
        if (playerHealth <= 0)
        {
            // Start death coroutine
        }
    }
}
