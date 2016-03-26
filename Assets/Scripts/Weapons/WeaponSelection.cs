using UnityEngine;
using System.Collections.Generic;
using System.Collections;


//Author: J.Anderson

public class WeaponSelection : MonoBehaviour
{
    PlayerWeapons playerWeapons;
    PlayerHealthManager pHM;
    [SerializeField]
    List<string> weaponsInInventoryList;
    string[] weaponsInInventory;
    string currentWep;
    int availableAmmoForWep;

    int arrayPos = 0;

    void Awake()
    {
        pHM = FindObjectOfType<PlayerHealthManager>();
        playerWeapons = FindObjectOfType<PlayerWeapons>();
    }

    void Start()
    {
        weaponsInInventory = new string[3];
        currentWep = "None";
    }

    void Update()
    {
        availableAmmoForWep = AmmoCostCheck(currentWep, playerWeapons.secondaryAmmo.CheckAmmo());
        OnDeath();
        SelectionMenu();
    }

    public void AddWepToInventory(string wepTag)
    {
        if (wepTag == "Dagger")
            if (weaponsInInventoryList.Find(d => d == "Dagger") == null)
            {
                weaponsInInventoryList.Add(wepTag);
                weaponsInInventoryList.CopyTo(weaponsInInventory);
            }


        if (wepTag == "StopWatch")
            if (weaponsInInventoryList.Find(d => d == "StopWatch") == null)
            {
                weaponsInInventoryList.Add(wepTag);
                weaponsInInventoryList.CopyTo(weaponsInInventory);
            }

        if (wepTag == "HolyCross")
            if (weaponsInInventoryList.Find(d => d == "HolyCross") == null)
            {
                weaponsInInventoryList.Add(wepTag);
                weaponsInInventoryList.CopyTo(weaponsInInventory);
            }
    }

    void SelectionMenu()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
            if (arrayPos >= weaponsInInventoryList.Count - 1)
            {
                arrayPos = 0;
                currentWep = weaponsInInventory[arrayPos];
            }
            else
            {
                arrayPos++;
                currentWep = weaponsInInventory[arrayPos];
            }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
            if (arrayPos <= 0 && weaponsInInventoryList.Count > 0)
            {
                arrayPos = weaponsInInventoryList.Count - 1;
                currentWep = weaponsInInventory[arrayPos];
            }
            else if (weaponsInInventoryList.Count > 0)
            {
                arrayPos--;
                currentWep = weaponsInInventory[arrayPos];
            }

        playerWeapons.CheckSelectedWeapon(currentWep);
    }

    public string GetCurrentWepName()
    {
        return currentWep;
    }

    public void SetActivePickup(string pickupTag)
    {
        currentWep = pickupTag;
    }

    int AmmoCostCheck(string wep, int availableAmmo)
    {
        int available = 0;
        if (wep == "Dagger")
            available = availableAmmo / 1;
        if (wep == "HolyCross")
            available = availableAmmo / 5;
        if (wep == "StopWatch")
            available = availableAmmo / 3;
        return available;
    }

    public int availableAmmoForCurrentWep()
    {
        return availableAmmoForWep;
    }

    void OnDeath()
    {
        if (pHM.CheckHealth() <= 0)
        {
            weaponsInInventoryList.Clear();
            weaponsInInventory = new string[3];
            playerWeapons.CheckSelectedWeapon(null);
            currentWep = "";
        }
    }
}
