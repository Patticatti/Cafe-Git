using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public Dictionary<RoomType, HashSet<Vector2Int>> roomKind = new Dictionary<RoomType, HashSet<Vector2Int>>()
    {
        { RoomType.Empty, new HashSet<Vector2Int>() },
        { RoomType.Enemy, new HashSet<Vector2Int>() },
        { RoomType.Random, new HashSet<Vector2Int>() },
        { RoomType.Treasure, new HashSet<Vector2Int>() },
        { RoomType.Boss, new HashSet<Vector2Int>() }
    };
}

public enum RoomType
{
    Empty,
    Enemy,
    Random,
    Treasure,
    Boss
}
