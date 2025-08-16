using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Room[] rooms;
    [SerializeField] private GameObject cameraObject;
    
    private int currentRoom;
    private float[] roomsSize;
    private int leftMostRoom;
    private int rightMostRoom;

    void Start()
    {
        roomsSize = new float[rooms.Length];

        leftMostRoom = 0;
        rightMostRoom = rooms.Length - 1;

        for (int i = 0; i < rooms.Length; i++)
        {
            roomsSize[i] = rooms[i].roomSprite.bounds.size.x;
        }

        currentRoom = GetCurrentRoom();
        PlaceRoomsInCorrectPlaces();
    }

    private void FixedUpdate()
    {
        currentRoom = GetCurrentRoom();
        if (currentRoom == leftMostRoom) 
        {
            MoveRoomLeft();
        }
        else if (currentRoom == rightMostRoom)
        {
            MoveRoomRight();
        }
    }

    private float DistanceBetweenRooms(int room1, int room2)
    {
        return roomsSize[room1] / 2 + roomsSize[room2] / 2;
    }

    private void PlaceRoomsInCorrectPlaces()
    {
        for (int i = currentRoom - 1; i >= 0; i--)
        {

            float roomNewXPos = rooms[i + 1].roomGameobject.transform.position.x - DistanceBetweenRooms(i, i + 1);
            rooms[i].roomGameobject.transform.position = new Vector2(roomNewXPos, 0);
        }
        for (int i = currentRoom + 1; i < rooms.Length; i++)
        {
            float roomNewXPos = rooms[i - 1].roomGameobject.transform.position.x + DistanceBetweenRooms(i, i - 1);
            rooms[i].roomGameobject.transform.position = new Vector2(roomNewXPos, 0);
        }
    }

    private int GetCurrentRoom() 
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            GameObject room = rooms[i].roomGameobject;

            float roomLeftBound = room.transform.position.x - roomsSize[i] / 2;
            float roomRightBound = room.transform.position.x + roomsSize[i] / 2;

            if (roomLeftBound < cameraObject.transform.position.x && cameraObject.transform.position.x < roomRightBound)
            {
                return i;
            }
        }
        return -1;
    }

    private void MoveRoomLeft() 
    {
        GameObject rightRoom = rooms[rightMostRoom].roomGameobject;
        GameObject leftRoom = rooms[leftMostRoom].roomGameobject;

        float roomNewXPosition = leftRoom.transform.position.x - DistanceBetweenRooms(leftMostRoom, rightMostRoom);

        rightRoom.transform.position = new Vector3(roomNewXPosition, 0, 0);

        leftMostRoom = rightMostRoom;
        rightMostRoom -= 1;

        if (rightMostRoom < 0) 
        {
            rightMostRoom = rooms.Length - 1;
        }
    }

    private void MoveRoomRight()
    {
        GameObject rightRoom = rooms[rightMostRoom].roomGameobject;
        GameObject leftRoom = rooms[leftMostRoom].roomGameobject;

        float roomNewXPosition = rightRoom.transform.position.x + DistanceBetweenRooms(rightMostRoom, leftMostRoom);

        leftRoom.transform.position = new Vector3(roomNewXPosition, leftRoom.transform.position.y, leftRoom.transform.position.z);

        rightMostRoom = leftMostRoom;
        leftMostRoom = (leftMostRoom + 1) % rooms.Length;
    }
}

[System.Serializable]
public class Room 
{
    public GameObject roomGameobject;
    public SpriteRenderer roomSprite;
}