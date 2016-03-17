using UnityEngine;
using System.Collections.Generic;
using System.Collections;


//Author: J.Anderson

public class WeaponSelection : MonoBehaviour
{
    PlayerWeapons playerWeapons;
    [SerializeField]
    List<string> weaponsInInventoryList;
    [SerializeField]
    string[] weaponsInInventory;
    [SerializeField]
    string currentWep;

    [SerializeField]
    int arrayPos = 0;

    void Awake()
    {
        playerWeapons = FindObjectOfType<PlayerWeapons>();
    }

    void Start()
    {
        weaponsInInventory = new string[3];
        currentWep = "None";
    }

    void Update()
    {
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
            if (arrayPos >= weaponsInInventory.Length - 1)
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
            if (arrayPos <= 0)
            {
                arrayPos = weaponsInInventory.Length - 1;
                currentWep = weaponsInInventory[arrayPos];
            }
            else
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

    void OnDeath()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            weaponsInInventoryList.Clear();
            weaponsInInventory = new string[3];
        }
    }
}
