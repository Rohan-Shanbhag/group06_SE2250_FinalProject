using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiceManagement : MonoBehaviour
{
    public static CharacterChoiceManagement instance;

    public CharacterOneController activePlayer;
    public CharacterSelection activeCharSelect;


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
