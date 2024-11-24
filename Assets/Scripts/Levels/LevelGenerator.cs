using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int numRooms = 10;
    public int roomSize = 18;
    public Vector2 startPos = new Vector2(2, 2);

    public GameObject room;

    private enum DirEnum {
        UP,DOWN,LEFT,RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        if (room == null)
            return;
        //The Level Generator runs when the scene is first initialized
        int[,] rooms = new int[5,5];

        Vector2 currentRoom = startPos;

        // Generate rooms starting at the start pos until the number of rooms is satisfied
        for(int i = 0; i < roomSize; i++)
        {
            DirEnum direction = RandDirection();
            //while ()
        }
    }
    
    // Gets a random direction to move in
    DirEnum RandDirection() {
        DirEnum[] dirArray = {DirEnum.UP, DirEnum.DOWN,DirEnum.LEFT,DirEnum.RIGHT};
        return (DirEnum)LootTable.PickRandom(dirArray);
    }

    bool ValidIndex(Vector2 index, Vector2 bounds) {
        if (index.x > bounds.x || index.y > bounds.y) return false;
        return true;
    }

    //Vector2 Move()
}
