using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] barricadedDoors;
    public bool barricadeWhenInRoom;

    [HideInInspector]
    public bool isRoomInUse;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            if(barricadeWhenInRoom)
            {
                foreach(GameObject barricadedDoor in barricadedDoors)
                {
                    barricadedDoor.SetActive(true);
                }
            }

            isRoomInUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isRoomInUse = false;
        }
    }

    public void OpenDoors()
    {
        foreach (GameObject barricadedDoor in barricadedDoors)
        {
            barricadedDoor.SetActive(false);

            barricadeWhenInRoom = false;
        }
    }
}
