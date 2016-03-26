using UnityEngine;

using System.Collections;


//Author: J.Anderson

/// <summary>
/// Script for enemies to damage player on contact
/// </summary>

public class DoDamage : MonoBehaviour
{
    PlayerHealthManager playerHealthManager;
    public int damage;

    void Awake()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
    }

    public void OnCollisionEnter(Collision enemyCollide)
    {
        
        if (enemyCollide.gameObject.tag == "Player")
        {
            playerHealthManager.RemoveHealth(1);
            StartCoroutine(gameObject.GetComponent<EnemyHealthManager>().Respawn());
        }
    }
}
