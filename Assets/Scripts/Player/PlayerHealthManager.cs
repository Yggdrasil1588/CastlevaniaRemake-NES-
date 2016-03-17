using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class PlayerHealthManager : MonoBehaviour 
{
    public Canvas deadDebug;
    public int playerMaxHealth;
    int playerHealth;
    public Text healthDisplayDebug;

    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    public void AddHealth(int amount)
    {
        playerHealth = playerHealth + amount;
    }

    public void RemoveHealth(int amount)
    {
        playerHealth = playerHealth - amount;
    }

    public int CheckHealth()
    {
        return playerHealth;
    }

    void Update()
    {
        OnDeath();
        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;

        healthDisplayDebug.text = "Health: " + playerHealth.ToString();
    }

    public void ReduceHealth(int amount)
    {
        playerHealth = playerHealth - amount;
    }

    void OnDeath()
    {
        if (playerHealth <= 0)
        {
            deadDebug.enabled = true;
            Time.timeScale = 0;
            // Start death coroutine
        }
    }
}
