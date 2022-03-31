using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private bool canSelect;
    public GameObject message;
    public CharacterOneController playerToSpawn;
    public bool shouldUnlock;

    // Start is called before the first frame update
    void Start()
    {
        if (shouldUnlock)
        {
            if (PlayerPrefs.HasKey(playerToSpawn.name))
            {
                if (PlayerPrefs.GetInt(playerToSpawn.name) == 1)
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
        if(canSelect)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = CharacterOneController.instance.transform.position;

                Destroy(CharacterOneController.instance.gameObject);

                CharacterOneController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
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
            canSelect = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = false;
            message.SetActive(false);
        }
    }
}
