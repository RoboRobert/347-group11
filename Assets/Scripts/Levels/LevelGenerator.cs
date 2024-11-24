using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int numRooms = 10;
    public int roomSize = 18;
    public Vector2 startPos = new Vector2(2, 2);

    private enum DirEnum {
        UP,DOWN,LEFT,RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        //The Level Generator runs when the scene is first initialized
        int[,] rooms = new int[5,5];

        // Generate rooms starting at the start pos until the number of rooms is 
    }
    
    // Gets a random direction to move in
    static DirEnum RandDirection() {
        DirEnum[] dirArray = {DirEnum.UP, DirEnum.DOWN,DirEnum.LEFT,DirEnum.RIGHT};
        return (DirEnum)LootTable.PickRandom(dirArray);
    }
}
