using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class SecAmmoPickup : MonoBehaviour
{
    AmmoManager secondaryWeapon;
 [SerializeField]
    private int ammo;
   
    public int AmmoToAdd
    {
        get { return ammo; }
        set
        {
            if (ammo > 5)
            {
                ammo = 5;
            }
            else if (ammo < 0)
            {
                ammo = 0;
            }
            else
                ammo = value; 
        }
    }


    void Awake()
    {
        secondaryWeapon = FindObjectOfType<PlayerWeapons>().secondaryAmmo;
    }

    public void OnTriggerEnter(Collider ammoPickup)
    {
        if (ammoPickup.gameObject.tag == "Player")
        {
            secondaryWeapon.AddAmmo(ammo);
            Destroy(gameObject);
        }
        if (ammoPickup.gameObject.tag == "Ground")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
}
