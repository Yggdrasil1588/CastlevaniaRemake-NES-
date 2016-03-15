using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class PlayerWeapons : MonoBehaviour 
{


    void Update()
    {
        MainWeapon();
        SecondaryWeapon();
    }

    void MainWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("main weapon active");
        }
    }
    
    void SecondaryWeapon()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            print("secondary weapon active");
        }
    }
}
