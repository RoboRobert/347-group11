using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a helper designed to provide some common loot table functionality
public static class LootTable
{
    public static object PickRandom<T>(T[] objects)
    {
        return objects[Random.Range(0, objects.Length)];
    }
}
