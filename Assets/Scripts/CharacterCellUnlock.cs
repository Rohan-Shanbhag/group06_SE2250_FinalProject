using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCellUnlock : MonoBehaviour
{
    private bool isAbleToBeFreed;
    public GameObject msg;

    public CharacterSelection[] potentialCharacters;
    private CharacterSelection freedCharacter;

    // Start is called before the first frame update
    void Start()
    {
        freedCharacter = potentialCharacters[Random.Range(0, potentialCharacters.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if(isAbleToBeFreed)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetInt(freedCharacter.playerToSpawn.name, 1);
                Instantiate(freedCharacter, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isAbleToBeFreed = true;
            msg.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAbleToBeFreed = false;
            msg.SetActive(false);
        }
    }
}
