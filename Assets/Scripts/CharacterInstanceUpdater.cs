using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstanceUpdater : MonoBehaviour
{
    public static CharacterInstanceUpdater instance;

    public int currentHealth, maxHealth, amtOfCoinsHeld;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
