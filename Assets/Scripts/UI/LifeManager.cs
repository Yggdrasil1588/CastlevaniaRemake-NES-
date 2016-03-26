using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class LifeManager : MonoBehaviour 
{
    PlayerHealthManager playerHealthManager;
    public Text lifeText;

    void Awake()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
    }

    void Update()
    {
       lifeText.text ="Life: " + playerHealthManager.CheckLife().ToString();
    }
	



}
