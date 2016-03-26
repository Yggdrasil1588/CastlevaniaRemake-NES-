using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class PlayerHealthManager : MonoBehaviour
{
    TimerManager timeManager;
    public Canvas deadDebug; // on death UI (not linked up through scenes yet)
    Vector3 startPos;
    [SerializeField]
    int maxHealth = 10;
    [SerializeField]
    int maxLife = 5;
    int playerHealth;
    int playerLife = 3;
    public Text healthDisplayDebug;

    void Start()
    {
        startPos = gameObject.transform.position;
        timeManager = FindObjectOfType<TimerManager>();
        playerHealth = maxHealth;
    }

    // All access to private fields in script 
    #region Properties
    // set so that health will never exceed max health
    public void AddHealth(int amount)
    {
        playerHealth = playerHealth + amount;
        if (playerHealth >= maxHealth)
        {
            playerHealth = maxHealth;
        }
    }

    public void RemoveHealth(int amount)
    {
        playerHealth = playerHealth - amount;
    }

    public int CheckHealth()
    {
        return playerHealth;
    }

    // set so that life will never exceed max life
    public void AddLife(int amount)
    {
        playerLife = playerLife + amount;
        if (playerLife >= maxLife)
        {
            playerLife = maxLife;
        }
    }

    public int CheckLife()
    {
        return playerLife;
    }
    #endregion

    void Update()
    {
        OnDeath();

        healthDisplayDebug.text = "Health: " + playerHealth.ToString();
    }


    void OnDeath()
    {
        if (playerHealth <= 0 && playerLife >= 1)
        {
            playerLife = playerLife - 1;
            gameObject.transform.position = startPos; // will be replaced with a checkpoint 
            timeManager.timerClass.ResetTimer();
            playerHealth = maxHealth;
        }
        if (playerHealth <= 0 && playerLife <= 0)
        {
            timeManager.timerClass.StopTimer();
            deadDebug.enabled = true;
            Time.timeScale = 0;
        }
    }
}
