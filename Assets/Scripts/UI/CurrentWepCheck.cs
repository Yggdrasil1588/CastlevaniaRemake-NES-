using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class CurrentWepCheck : MonoBehaviour 
{
    WeaponSelection wepSelect;
    public Text currentWepText;
    public Text availableText;

    void Awake()
    {
        wepSelect = FindObjectOfType<WeaponSelection>();
    }

    void Update()
    {
        availableText.text = wepSelect.availableAmmoForCurrentWep().ToString();
        currentWepText.text = wepSelect.GetCurrentWepName();
    }

}
