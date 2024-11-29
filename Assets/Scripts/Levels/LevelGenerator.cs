using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float numRooms = 10;
    public float roomSize = 18;
    public Vector2 startPos = new Vector2(2, 2);

    public GameObject room;

    private enum DirEnum {
        UP,DOWN,LEFT,RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        //The Level Generator runs when the scene is first initialized
        int[,] rooms = new int[5, 5];

        Vector2 bounds = new Vector2(5, 5);
        Vector2 currentRoom = startPos;
        rooms[(int)currentRoom.x, (int)currentRoom.y] = 1;

        // Generate rooms starting at the start pos until the number of rooms is satisfied
        for (int i = 0; i < numRooms; i++)
        {
            Vector2 direction = RandDirection();
            while (!ValidIndex(currentRoom + direction, bounds)) {
                direction = RandDirection();
            }
            currentRoom += direction;
            if (rooms[(int)currentRoom.x, (int)currentRoom.y] == 1)
                numRooms += 1;
            rooms[(int)currentRoom.x, (int)currentRoom.y] = 1;
        }

        // If the room GameObject isn't set, return.
        if (room == null)
            return;

        float offsetConstant = roomSize * (8f / 25f);
        Debug.Log(offsetConstant);
        // Instantiate the rooms with offsets
        string debugMessage = "";
        for (int i = 0; i < 5; i++)
        {
            for (int j =0; j < 5; j++)
            {
                if (rooms[i,j] == 1)
                {
                    float translateX = (float)((i - startPos.x) * offsetConstant);
                    float translateY = (float)((j - startPos.x) * offsetConstant);
                    GameObject newRoom = Instantiate(room, this.transform);
                    newRoom.transform.Translate(translateX, translateY, 0);
                }
                    
                debugMessage += rooms[i, j] + " ";
            }
            debugMessage += "\n";
        }

        Debug.Log(debugMessage);
    }
    
    // Gets a random direction to move in
    Vector2 RandDirection() {
        DirEnum[] dirArray = {DirEnum.UP, DirEnum.DOWN,DirEnum.LEFT,DirEnum.RIGHT};
        DirEnum direction = (DirEnum)LootTable.PickRandom(dirArray);

        if (direction == DirEnum.UP)
            return new Vector2(0, -1);
        else if (direction == DirEnum.DOWN)
            return new Vector2(0, 1);
        else if (direction == DirEnum.LEFT)
            return new Vector2(-1,0);

        // Return right by default
        return new Vector2(1,0);
    }

    // Returns whether an index is valid inside some specified array bounds
    bool ValidIndex(Vector2 index, Vector2 bounds) {
        if (index.x >= bounds.x || index.y >= bounds.y || index.x < 0 || index.y < 0) return false;
        return true;
    }
}
