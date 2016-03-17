using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Author: J.Anderson

public class AmmoCheck : MonoBehaviour 
{
    AmmoManager ammoManager;
    public Text ammoText;

    void Start()
    {
        ammoManager = FindObjectOfType<PlayerWeapons>().secondaryAmmo;
    }


    void Update()
    {
        ammoText.text ="Ammo: " + ammoManager.CheckAmmo().ToString();
    }

}
