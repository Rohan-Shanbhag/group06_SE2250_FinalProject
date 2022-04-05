using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private bool isAbleToSelect;
    public GameObject msg;
    public CharacterOneController switchableCharacter;
    public bool canFree;

    // Start is called before the first frame update
    void Start()
    {
        if (canFree)
        {
            if (PlayerPrefs.HasKey(switchableCharacter.name))
            {
                if (PlayerPrefs.GetInt(switchableCharacter.name) == 1)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAbleToSelect)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Vector3 switchableCharacterPosition = CharacterOneController.instance.transform.position;

                Destroy(CharacterOneController.instance.gameObject);

                CharacterOneController newPlayer = Instantiate(switchableCharacter, switchableCharacterPosition, switchableCharacter.transform.rotation);
                CharacterOneController.instance = newPlayer;

                gameObject.SetActive(false);

                CameraController.instance.trgt = newPlayer.transform;

                CharacterChoiceManagement.instance.activePlayer = newPlayer;
                CharacterChoiceManagement.instance.activeCharSelect.gameObject.SetActive(true);
                CharacterChoiceManagement.instance.activeCharSelect = this;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isAbleToSelect = true;
            msg.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAbleToSelect = false;
            msg.SetActive(false);
        }
    }
}
