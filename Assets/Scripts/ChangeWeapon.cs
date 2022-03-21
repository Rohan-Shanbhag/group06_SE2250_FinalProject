using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    int totalWeapons = 1;
    public int currentWeaponIndex;

    public GameObject[] allWeapons;
    public GameObject weaponHolder;
    public GameObject currentWeapon;


    void Start()    
    {
        totalWeapons = weaponHolder.transform.childCount;
        allWeapons = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            allWeapons[i] = weaponHolder.transform.GetChild(i).gameObject;
            allWeapons[i].SetActive(false);
        }

        allWeapons[0].SetActive(true);
        currentWeapon = allWeapons[0];  
        currentWeaponIndex = 0;
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeaponIndex < totalWeapons-1)
            {
                allWeapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                allWeapons[currentWeaponIndex].SetActive(true);
                currentWeapon = allWeapons[currentWeaponIndex];
            }

            else if (currentWeaponIndex > 0)
            {
                allWeapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                allWeapons[currentWeaponIndex].SetActive(true);
                currentWeapon = allWeapons[currentWeaponIndex];
            }
        }
    }
}
