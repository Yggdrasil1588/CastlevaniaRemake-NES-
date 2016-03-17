using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class SecAmmoPickup : MonoBehaviour
{
    AmmoManager secondaryWeapon;

    void Awake()
    {
        secondaryWeapon = FindObjectOfType<PlayerWeapons>().secondaryAmmo;
    }

    public void OnTriggerEnter(Collider ammoPickup)
    {
        if (ammoPickup.gameObject.tag == "Player")
        {
            secondaryWeapon.AddAmmo(1);
            Destroy(gameObject);
        }
    }
}
