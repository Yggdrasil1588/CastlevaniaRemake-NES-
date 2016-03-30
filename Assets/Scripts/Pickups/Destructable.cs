using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class Destructable : MonoBehaviour
{
    [SerializeField]
    GameObject[] childObjects;
    PlayerWeapons playerWeapons;
    int childCount;

    void Start()
    {
        playerWeapons = FindObjectOfType<PlayerWeapons>();

        SetArray();
    }

    void SetArray()
    {
        childCount = gameObject.transform.childCount;
        childObjects = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childObjects[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    void SearchArray()
    {
        if (playerWeapons.weaponVariables.getPhase1 || playerWeapons.weaponVariables.getPhase2 && !playerWeapons.weaponVariables.getPhase3)
        {
            for (int s = 0; s < childCount; s++)
            {
                if (childObjects[s].tag == "Whip")
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    childObjects[s].SetActive(true);
                    childObjects[s].GetComponent<Rigidbody>().useGravity = true;
                    childObjects[s].transform.parent = null;
                    RebuildArray();
                }
            }
        }
        else if (playerWeapons.weaponVariables.getPhase3)
        {
            for (int s = 0; s < childCount; s++)
            {
                if (childObjects[s].tag == "Ammo")
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    childObjects[s].SetActive(true);
                    childObjects[s].GetComponent<Rigidbody>().useGravity = true;
                    childObjects[s].transform.parent = null;
                    RebuildArray();
                }
            }
        }

        for (int s = 0; s < childCount; s++)
        {
            if (childObjects[s].tag == "Life" || childObjects[s].tag == "Dagger" || childObjects[s].tag == "HolyCross"
                || childObjects[s].tag == "LargeAmmo" || childObjects[s].tag == "StopWatch" || childObjects[s].tag == "Health")
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                childObjects[s].SetActive(true);
                childObjects[s].GetComponent<Rigidbody>().useGravity = true;
                childObjects[s].transform.parent = null;
                RebuildArray();
            }
        }
    }

    public void RebuildArray()
    {
        childCount = gameObject.transform.childCount;
        childObjects = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childObjects[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        print(collision);
        if (collision.tag == "MainWeapon")
        {
            SearchArray();
        }
    }
}
