﻿using UnityEngine;

using System.Collections;

//Author: J.Anderson

public class PlayerWeapons : MonoBehaviour
{
    [HideInInspector]
    public AmmoManager secondaryAmmo = new AmmoManager();
    public Transform instantiatePoint;
    [SerializeField]
    int throwForce;

    [Header("WeaponSelected")]

    bool dagger;
    bool stopWatch;
    bool holyCross;


    void Update()
    {
        MainWeapon();
        SecondaryWeaponDagger();
    }

    void MainWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("main weapon active");
        }
    }

    public void CheckSelectedWeapon(string currentWep)
    {
        dagger = currentWep == "Dagger" ? true : false;
        stopWatch = currentWep == "StopWatch" ? true : false;
        holyCross = currentWep == "HolyCross" ? true : false;
    }

    void SecondaryWeaponDagger()
    {
        if (dagger)
        {
            Vector3 playerVelocity = gameObject.GetComponent<Rigidbody>().velocity; //Grabs the current velocity of the player
            if (Input.GetButtonDown("Fire2") && secondaryAmmo.CheckAmmo() > 0)
            {
                GameObject temp = Instantiate(Resources.Load("Dagger", typeof(GameObject)),
                                                instantiatePoint.position,
                                                    instantiatePoint.rotation) as GameObject;
                Rigidbody rb = temp.AddComponent<Rigidbody>();
                Physics.IgnoreCollision(temp.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                rb.useGravity = false;
                rb.velocity = playerVelocity; // Sets the velocity of the projectile to the current player velocity
                rb.AddForce(transform.forward * throwForce); // Adds forward force to the projectile 

                secondaryAmmo.RemoveAmmo(1);
            }
        }
    }
    void SecondaryWeaponStopWatch()
    {
        if (stopWatch)
        {
            if (Input.GetButtonDown("Fire2") && secondaryAmmo.CheckAmmo() > 0)
            {
                print("Stop-Watch Fire");
            }
        }
    }
    void SecondaryWeaponHolyCross()
    {
        if (holyCross)
        {
            if (Input.GetButtonDown("Fire2") && secondaryAmmo.CheckAmmo() > 0)
            {
                print("Holy Cross Fire");
            }
        }
    }
}
