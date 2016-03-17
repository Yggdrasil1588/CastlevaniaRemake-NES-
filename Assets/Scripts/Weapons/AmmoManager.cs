using UnityEngine;

using System.Collections;


//Author: J.Anderson
    public class AmmoManager
    {
        int totalAmmo = 1;

        public void RemoveAmmo(int amount)
        {
            totalAmmo = totalAmmo - amount;
        }

        public void AddAmmo(int amount)
        {
            totalAmmo = totalAmmo + amount;
        }

        public int CheckAmmo()
        {
            return totalAmmo;
        }


    }
