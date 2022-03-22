using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConfig : MonoBehaviour
{
    public bool areEnemiesClearedInCurrentRoom;

    public List<GameObject> enemies = new List<GameObject>();

    public Room theRoom;

    // Start is called before the first frame update
    void Start()
    {
        if(areEnemiesClearedInCurrentRoom)
        {
            theRoom.barricadeWhenInRoom = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (areEnemiesClearedInCurrentRoom && enemies.Count > 0 && theRoom.isRoomInUse)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}
