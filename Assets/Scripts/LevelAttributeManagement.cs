using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class LevelAttributeManagement : MonoBehaviour
{
    public static LevelAttributeManagement instance;

    public float loadDelay = 4f;

    public bool isGameCurrentlyPaused;

    public int amtOfCoinsHeld;

    public Transform startingPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set starting attributes of character
        CharacterOneController.instance.transform.position = startingPoint.position;
        CharacterOneController.instance.isAbleToMove = true;
        amtOfCoinsHeld = CharacterInstanceUpdater.instance.amtOfCoinsHeld;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    
    public void PauseUnpause()
    {
        if(!isGameCurrentlyPaused)
        {
            isGameCurrentlyPaused = true;

            Time.timeScale = 0f;
        } else
        {
            isGameCurrentlyPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void AccumulateCoins(int amount)
    {
        amtOfCoinsHeld += amount;
    }

    public void RedeemCoins(int amount)
    {
        amtOfCoinsHeld -= amount;

        if(amtOfCoinsHeld < 0)
        {
            amtOfCoinsHeld = 0;
        }
    }
}
