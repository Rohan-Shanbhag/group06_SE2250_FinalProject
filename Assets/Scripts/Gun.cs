using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;

    public string gunName;
    public Sprite gunHUDIcon;

    public int gunCost;
    public Sprite shopIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // implement conditional check for LevelManager
        if (CharacterOneController.instance.isAbleToMove)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots;
                }
            }
        }
    }
}
