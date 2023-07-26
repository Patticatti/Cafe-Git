using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class CorridorFirstDungeonGenerator : RandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
   
    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    [SerializeField]
    private GameObject barrelPrefab;

    private Dictionary<RoomType, HashSet<Vector2Int>> roomKind = new Dictionary<RoomType, HashSet<Vector2Int>>()
    {
        { RoomType.Empty, new HashSet<Vector2Int>() },
        { RoomType.Enemy, new HashSet<Vector2Int>() },
        { RoomType.Random, new HashSet<Vector2Int>() },
        { RoomType.Treasure, new HashSet<Vector2Int>() },
        { RoomType.Boss, new HashSet<Vector2Int>() }
    };

    private HashSet<Vector2Int> floorPositions, corridorPositions;

    //Gizmos Data
    //private List<Color> roomColors = new List<Color>();

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
        //AddEnemies();
    }


    private void CorridorFirstGeneration()
    {
        EventManager.Instance.generateEvent.Invoke();
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> floorNoCorridor = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions); //floorPositions will have corridor floor spaces

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions); //rooms

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);
        AddBarrels();

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void AddBarrels()
    {
        List<Vector2Int> barrelSpots = new List<Vector2Int>();
        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> entry in roomsDictionary)
        {
            ItemPlacement itemPlacement = new ItemPlacement(entry.Value);
            barrelSpots = itemPlacement.GetRandomSpotsNearWall(entry.Value, 5);
            foreach (var position in barrelSpots)
            {
                Instantiate(barrelPrefab, new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity);
            }
            barrelSpots.Clear();
        }
    }



    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false) //real dead end
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private HashSet<Vector2Int> floorWithNoCorridor(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> corridorPositions)
    {
        return new HashSet<Vector2Int>(floorPositions.Except(corridorPositions));
    }


    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;

            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }


    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPosi = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent); //count of rooms want to geenrate

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); //creates unique id for positions
        ClearRoomData();
        foreach (var roomPosition in roomsToCreate) //room number
        {
            var floorPositions = RunRandomWalk(randomWalkParameters, roomPosition);

            SaveRoomData(roomPosition, floorPositions); //room number, floortile locations
            roomPosi.UnionWith(floorPositions); //avoid repetitions in collection
        }
        //AddEnemies(roomPositions);
        return roomPosi;
    }


    private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> floorPositions)
    {
        roomsDictionary[roomPosition] = floorPositions;
        //roomColors.Add(UnityEngine.Random.ColorHSV());
    }

    private void ClearRoomKindData()
    {
        roomKind.Clear();
    }

    private void ClearRoomData()
    {
        roomsDictionary.Clear();
        //roomColors.Clear();
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        int corridorAmnt = 0;

        CreateTwoCorridors(floorPositions, potentialRoomPositions, corridorAmnt, currentPosition);
    }

    private void CreateTwoCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions, int corridorAmnt, Vector2Int originPoint)
    {
        var currentPosition = originPoint;
        potentialRoomPositions.Add(currentPosition);
        bool newCorridor;
        corridorAmnt++;

        for (int i = 0; i < 2; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(originPoint, corridorLength);
            newCorridor = false;
            while (newCorridor == false) //keep generating until get amount of corridors
            {
                corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(originPoint, corridorLength);
                if (!floorPositions.Contains(corridor[corridor.Count - 1]))
                {
                    newCorridor = true;
                    currentPosition = corridor[corridor.Count - 1]; //set current to last in corridor so connected, current pos is the end it will start off of
                    potentialRoomPositions.Add(currentPosition);//only add if not yet, make sure to keep iterating if 
                    floorPositions.UnionWith(corridor);
                    if (i == 0)
                    {
                        roomKind[RoomType.Empty].Add(currentPosition);
                    }
                    else
                    {
                        if (corridorAmnt < (corridorCount - 1))
                        {
                            roomKind[RoomType.Enemy].Add(currentPosition);
                        }
                        else
                        {
                            roomKind[RoomType.Boss].Add(currentPosition);
                        }
                    }
                }
            }
        }
        if (corridorAmnt < corridorCount)
        {
            CreateTwoCorridors(floorPositions, potentialRoomPositions, corridorAmnt, currentPosition);
        }

        corridorPositions = new HashSet<Vector2Int>(floorPositions); //use later for prefab placement
    }

    public enum RoomType
    {
        Empty,
        Enemy,
        Random,
        Treasure,
        Boss
    }


}
