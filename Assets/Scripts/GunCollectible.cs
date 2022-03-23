using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollectible : MonoBehaviour
{
    public Gun gunToBeCollected;

    public float collectionDelay = .5f;

    // Update is called once per frame
    void Update()
    {
        if (collectionDelay > 0)
        {
            collectionDelay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && collectionDelay <= 0)
        {
            bool alreadyHasWeaponToBeCollected = false;
            foreach(Gun gunToCheck in CharacterOneController.instance.heldWeapons)
            {
                if(gunToBeCollected.gunName == gunToCheck.gunName)
                {
                    alreadyHasWeaponToBeCollected = true;
                }
            }

            if(!alreadyHasWeaponToBeCollected)
            {
                Gun newWeapon = Instantiate(gunToBeCollected);
                newWeapon.transform.parent = CharacterOneController.instance.gunHoldingArm;
                newWeapon.transform.position = CharacterOneController.instance.gunHoldingArm.position;
                newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
                newWeapon.transform.localScale = Vector3.one;

                CharacterOneController.instance.heldWeapons.Add(newWeapon);
                CharacterOneController.instance.equippedWeapon = CharacterOneController.instance.heldWeapons.Count - 1;
                CharacterOneController.instance.ChangeWeapon();
            }
            Destroy(gameObject);
        }
    }
}
