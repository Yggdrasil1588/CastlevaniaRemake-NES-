using UnityEngine;
using UnityEditor;
using System.Collections;


//Author: J.Anderson
[ExecuteInEditMode]
public class PickupWindow : EditorWindow
{
    [System.Serializable]
    public class Pickups
    {
        public Object dagger
        {
            get
            {
                return Resources.Load("Pickups/DaggerPickup", typeof(GameObject));
            }
        }
        public Object holyCross
        {
            get
            {
                return Resources.Load("Pickups/HolyCrossPickup", typeof(GameObject));
            }
        }
        public Object stopWatch
        {
            get
            {
                return Resources.Load("Pickups/StopWatchPickup", typeof(GameObject));
            }
        }
        public Object smallAmmo
        {
            get
            {
                return Resources.Load("Pickups/SmallAmmoPickup", typeof(GameObject));
            }
        }
        public Object largeAmmo
        {
            get
            {
                return Resources.Load("Pickups/LargeAmmoPickup", typeof(GameObject));
            }
        }
        public Object health
        {
            get
            {
                return Resources.Load("Pickups/HealthPickup", typeof(GameObject));
            }
        }
        public Object whip
        {
            get
            {
                return Resources.Load("Pickups/WhipPickup", typeof(GameObject));
            }
        }
        public Object life
        {
            get
            {
                return Resources.Load("Pickups/LifePickup", typeof(GameObject));
            }
        }
    }

    Pickups pickups = new Pickups();
    public Object selected;
    Object[] selectedArray;
    GameObject selectedGO;
    bool multipleSelected;

    [MenuItem("Tools/Josh's Tools/Add Pickup")]

    static void ShowWindow()
    {
        GetWindow(typeof(PickupWindow), true, "Add Drop To Object", true);
    }

    void OnGUI()
    {
        selectedArray = Selection.objects;
        selected = Selection.activeObject;
        selectedGO = selected as GameObject;
        GUILayout.Label("Selected Objects", EditorStyles.boldLabel);
        GUILayout.TextArea(selectedArray.Length.ToString(), EditorStyles.boldLabel);

        if (Selection.activeObject)
        {
            if (selectedGO.GetComponent<Destructable>())
            {
                // FocusWindowIfItsOpen<PickupWindow>();
                EditorGUILayout.BeginFadeGroup(1);
                GUILayout.Label("Pickup To Add", EditorStyles.boldLabel);

                if (GUILayout.Button("Add Dagger"))
                {
                    AddDagger();
                }
                if (GUILayout.Button("Add Stop Watch"))
                {
                    AddStopwatch();
                }
                if (GUILayout.Button("Add Holy Cross"))
                {
                    AddHolyCross();
                }
                if (GUILayout.Button("Add Health"))
                {
                    AddHealth();
                }
                if (GUILayout.Button("Add Small Ammo"))
                {
                    AddSmallAmmo();
                }
                if (GUILayout.Button("Add Large Ammo"))
                {
                    AddLargeAmmo();
                }
                if (GUILayout.Button("Add Whip"))
                {
                    AddWhip();
                }
                if (GUILayout.Button("Add Life"))
                {
                    AddLife();
                }
            }

            else
            {
                EditorGUILayout.EndFadeGroup();
                string warning = "Can't add pickup to selected GameObject " + "\n" + "GameObject needs Destructable component to add pickups to it";
                GUILayout.TextArea(warning, EditorStyles.helpBox);
            }

        }
        else if (!Selection.activeObject)
        {
            string warning = "No Object Selected";
            GUILayout.TextArea(warning, EditorStyles.helpBox);
        }
    }

    void AddDagger()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.dagger, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "DaggerPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddStopwatch()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.stopWatch, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "StopwatchPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddHolyCross()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.holyCross, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "HolyCrossPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddHealth()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.health, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "HealthPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddSmallAmmo()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.smallAmmo, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "SmallAmmoPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddLargeAmmo()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.largeAmmo, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "LargeAmmoPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddWhip()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.whip, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "WhipPickup";
            tempMulti.SetActive(false);
        }
    }
    void AddLife()
    {
        for (int i = 0; i < selectedArray.Length; i++)
        {
            selectedGO = selectedArray[i] as GameObject;
            GameObject tempMulti = Instantiate(pickups.life, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
            tempMulti.transform.parent = selectedGO.transform;
            tempMulti.name = "LifePickup";
            tempMulti.SetActive(false);
        }
    }
}
