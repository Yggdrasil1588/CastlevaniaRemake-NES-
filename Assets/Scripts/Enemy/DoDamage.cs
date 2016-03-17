using UnityEngine;

using System.Collections;


//Author: J.Anderson

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
            playerHealthManager.ReduceHealth(1);
            StartCoroutine(gameObject.GetComponent<EnemyHealthManager>().Respawn());
        }
    }
}
