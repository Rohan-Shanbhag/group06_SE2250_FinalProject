using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    public GameObject presetRoom;

    public int amtOfRoomsToLevelEnd;

    public bool hasPurchasingRoom;
    public int minAmtOfRoomsToPurchaseRoom, maxAmtOfRoomsToPurchaseRoom;

    public bool hasWeaponMysteryBoxRoom;
    public int minAmtOfRoomsToMysteryBoxRoom, maxAmtOfRoomsToMysteryBoxRoom;

    public Transform pointOfRender;

    public enum Direction {up, right, down, left};
    public Direction chosenDirection;

    public float xOffset = 18f, yOffset = 10;


    public LayerMask roomInspectorLayer;

    private GameObject levelEndRoom, itemPurchasingRoom, mysteryBoxRoom;

    private List<GameObject> presetRooms = new List<GameObject>();

    public RoomOutlines roomOutlnes;

    private List<GameObject> renderedRoomOutlines = new List<GameObject>();

    public RoomConfig roomConfigStart, roomConfigEnd, roomConfigPurchasingArea, roomConfigMysteryBoxArea;
    public RoomConfig[] prospectiveRoomConfigs;

    // Start is called before the first frame update
    void Start()
    {
        chosenDirection = (Direction)Random.Range(0, 4);
        ShiftPointGenerator();

        for(int i = 0; i < amtOfRoomsToLevelEnd; i++)
        {
            GameObject newRoom = Instantiate(presetRoom, pointOfRender.position, pointOfRender.rotation);

            presetRooms.Add(newRoom);

            if(i + 1 == amtOfRoomsToLevelEnd)
            {
                presetRooms.RemoveAt(presetRooms.Count - 1);

                levelEndRoom = newRoom;
            }

            chosenDirection = (Direction)Random.Range(0, 4);
            ShiftPointGenerator();

            while (Physics2D.OverlapCircle(pointOfRender.position, .2f, roomInspectorLayer))
            {
                ShiftPointGenerator();
            }


        }

        if(hasPurchasingRoom)
        {
            int shopSelector = Random.Range(minAmtOfRoomsToPurchaseRoom, maxAmtOfRoomsToPurchaseRoom + 1);
            itemPurchasingRoom = presetRooms[shopSelector];
            presetRooms.RemoveAt(shopSelector);
        }

        if (hasWeaponMysteryBoxRoom)
        {
            int grSelector = Random.Range(minAmtOfRoomsToMysteryBoxRoom, maxAmtOfRoomsToMysteryBoxRoom + 1);
            mysteryBoxRoom = presetRooms[grSelector];
            presetRooms.RemoveAt(grSelector);
        }

        //create room outlines
        RenderRoomOutline(Vector3.zero);
        foreach(GameObject room in presetRooms)
        {
            RenderRoomOutline(room.transform.position);
        }
        RenderRoomOutline(levelEndRoom.transform.position);
        if(hasPurchasingRoom)
        {
            RenderRoomOutline(itemPurchasingRoom.transform.position);
        }
        if (hasWeaponMysteryBoxRoom)
        {
            RenderRoomOutline(mysteryBoxRoom.transform.position);
        }



        foreach (GameObject outline in renderedRoomOutlines)
        {
            bool generateCenter = true;

            if(outline.transform.position == Vector3.zero)
            {
                Instantiate(roomConfigStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();

                generateCenter = false;
            }

            if(outline.transform.position == levelEndRoom.transform.position)
            {
                Instantiate(roomConfigEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();

                generateCenter = false;
            }

            if(hasPurchasingRoom)
            {
                if (outline.transform.position == itemPurchasingRoom.transform.position)
                {
                    Instantiate(roomConfigPurchasingArea, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();

                    generateCenter = false;
                }
            }

            if(hasWeaponMysteryBoxRoom)
            {
                if (outline.transform.position == mysteryBoxRoom.transform.position)
                {
                    Instantiate(roomConfigMysteryBoxArea, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();

                    generateCenter = false;
                }
            }


            if (generateCenter)
            {
                int centerSelect = Random.Range(0, prospectiveRoomConfigs.Length);

                Instantiate(prospectiveRoomConfigs[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }

        }
    }

    public void ShiftPointGenerator()
    {
        switch(chosenDirection)
        {
            case Direction.up:
                pointOfRender.position += new Vector3(0f, yOffset, 0f);
                break;

            case Direction.down:
                pointOfRender.position += new Vector3(0f, -yOffset, 0f);
                break;

            case Direction.right:
                pointOfRender.position += new Vector3(xOffset, 0f, 0f);
                break;

            case Direction.left:
                pointOfRender.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }

    public void RenderRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, roomInspectorLayer);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, roomInspectorLayer);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, roomInspectorLayer);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, roomInspectorLayer);

        int directionCount = 0;
        if(roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }

        switch(directionCount)
        {
            case 0:
                Debug.LogError("Found no room exists!!");
                break;

            case 1:

                if(roomAbove)
                {
                    renderedRoomOutlines.Add( Instantiate(roomOutlnes.singleUp, roomPosition, transform.rotation));
                }

                if(roomBelow)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.singleDown, roomPosition, transform.rotation));
                }

                if(roomLeft)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.singleLeft, roomPosition, transform.rotation));
                }

                if(roomRight)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.singleRight, roomPosition, transform.rotation));
                }

                break;

            case 2:

                if(roomAbove && roomBelow)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.doubleUpDown, roomPosition, transform.rotation));
                }

                if(roomLeft && roomRight)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.doubleLeftRight, roomPosition, transform.rotation));
                }

                if(roomAbove && roomRight)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.doubleUpRight, roomPosition, transform.rotation));
                }

                if(roomRight && roomBelow)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.doubleRightDown, roomPosition, transform.rotation));
                }

                if(roomBelow && roomLeft)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.doubleDownLeft, roomPosition, transform.rotation));
                }

                if(roomLeft && roomAbove)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.doubleLeftUp, roomPosition, transform.rotation));
                }

                break;

            case 3:

                if(roomAbove && roomRight && roomBelow)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.tripleUpRightDown, roomPosition, transform.rotation));
                }

                if (roomRight && roomBelow && roomLeft)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.tripleRightDownLeft, roomPosition, transform.rotation));
                }

                if (roomBelow && roomLeft && roomAbove)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.tripleDownLeftUp, roomPosition, transform.rotation));
                }

                if (roomLeft && roomAbove && roomRight)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.tripleLeftUpRight, roomPosition, transform.rotation));
                }

                break;

            case 4:


                if (roomBelow && roomLeft && roomAbove && roomRight)
                {
                    renderedRoomOutlines.Add(Instantiate(roomOutlnes.fourway, roomPosition, transform.rotation));
                }

                break;
        }
    }
}

[System.Serializable]
public class RoomOutlines
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;
}
