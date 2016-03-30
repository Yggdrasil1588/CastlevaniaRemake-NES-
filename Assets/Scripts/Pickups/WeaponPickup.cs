using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class WeaponPickup : MonoBehaviour
{
    WeaponSelection weaponSelection;
    PlayerWeapons playerWeapons;
    string weaponName;


    void Start()
    {
        weaponSelection = FindObjectOfType<WeaponSelection>();
        playerWeapons = FindObjectOfType<PlayerWeapons>();
        weaponName = gameObject.tag;
    }

    void OnTriggerEnter(Collider other)
    {
        // for the 3 phases of the whip
        if (other.tag == "Player" && gameObject.tag == "Whip")
        {
            // if true then switch to phase 2 increasing whip cast length
            if (playerWeapons.weaponVariables.getPhase1)
            {
                playerWeapons.weaponVariables.WeponPhaseSwap(true);
            }
            // if true then phase 2 stays active and 3 is also activated increasing the damage form 1 to 3
            else if (playerWeapons.weaponVariables.getPhase2)
            {
                playerWeapons.weaponVariables.Phase3(true);
            }
            Destroy(gameObject);
        }
        // for secondary weapon pickups
        if (other.tag == "Player" && gameObject.tag != "Whip")
        {
            weaponSelection.AddWepToInventory(gameObject.tag);
            weaponSelection.SetActivePickup(gameObject.tag);
            Destroy(gameObject);
        }
        if (other.tag == "Ground")
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
