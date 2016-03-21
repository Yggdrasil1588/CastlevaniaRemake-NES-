using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class PlayerHealthManager : MonoBehaviour
{
    TimerManager timeManager;
    public Canvas deadDebug;
    Vector3 startPos;
    [SerializeField]
    int playerMaxHealth = 10;
    int playerHealth;
    int playerLife = 3;
    public Text healthDisplayDebug;

    void Start()
    {
        startPos = gameObject.transform.position;
        timeManager = FindObjectOfType<TimerManager>();
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

    public int CheckLife()
    {
        return playerLife;
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
        if (playerHealth <= 0 && playerLife >= 1)
        {
            playerLife = playerLife - 1;
            gameObject.transform.position = startPos;
            timeManager.timerClass.ResetTimer();
            playerHealth = playerMaxHealth;
        }
        if (playerHealth <= 0 && playerLife <= 0)
        {
            timeManager.timerClass.StopTimer();
            deadDebug.enabled = true;
            Time.timeScale = 0;
        }
    }
}
