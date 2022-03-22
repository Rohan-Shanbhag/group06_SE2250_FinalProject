using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopPurchase : MonoBehaviour
{
    public GameObject purchaseUserMsg;

    private bool characterInPurchaseBoundary;

    public bool replenishesHealth, upgradesHealthAmt, addsNewWeapon;

    public int coinsNeeded;

    public int amtToUpgradeHealthBy;

    public Gun[] weaponsAvailable;
    private Gun weapon;
    public SpriteRenderer gunSprite;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        if(addsNewWeapon)
        {
            int selectedGun = Random.Range(0, weaponsAvailable.Length);
            weapon = weaponsAvailable[selectedGun];

            gunSprite.sprite = weapon.shopIcon;
            infoText.text = weapon.gunName + "\n - " + weapon.gunCost + " Gold - ";
            coinsNeeded = weapon.gunCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(characterInPurchaseBoundary)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(LevelAttributeManagement.instance.amtOfCoinsHeld >= coinsNeeded)
                {
                    LevelAttributeManagement.instance.RedeemCoins(coinsNeeded);

                    if(replenishesHealth)
                    {
                        CharacterOneHealthController.instance.HealPlayer(CharacterOneHealthController.instance.maxHealth);
                    }

                    if(upgradesHealthAmt)
                    {
                        CharacterOneHealthController.instance.IncreaseMaxHealth(amtToUpgradeHealthBy);
                    }

                    if(addsNewWeapon)
                    {
                        Gun gunClone = Instantiate(weapon);
                        gunClone.transform.parent = CharacterOneController.instance.gunHoldingArm;
                        gunClone.transform.position = CharacterOneController.instance.gunHoldingArm.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        CharacterOneController.instance.heldWeapons.Add(gunClone);
                        CharacterOneController.instance.equippedWeapon = CharacterOneController.instance.heldWeapons.Count - 1;
                        CharacterOneController.instance.ChangeWeapon();
                    }
                    gameObject.SetActive(false);
                    characterInPurchaseBoundary = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            purchaseUserMsg.SetActive(true);

            characterInPurchaseBoundary = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            purchaseUserMsg.SetActive(false);

            characterInPurchaseBoundary = false;
        }
    }
}
