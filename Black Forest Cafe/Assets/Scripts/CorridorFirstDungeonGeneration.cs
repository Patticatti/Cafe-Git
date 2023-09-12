using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using static CorridorFirstDungeonGenerator;

public class CorridorFirstDungeonGenerator : RandomWalkMapGenerator
{
    private RoomData roomData;
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> rangedEnemiesDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>(); //ranged enemies (room position, floor positions)
    private Dictionary<Vector2Int, HashSet<Vector2Int>> meleeEnemiesDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>(); //^^

    private GameObject newGameObject;
    [SerializeField]
    private GameObject barrelPrefab;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject batPrefab;
    [SerializeField]
    private GameObject chestPrefab;
    [SerializeField]
    private int barrelAmnt = 5;

    public bool generateItems = true;
    public bool generateEnemies = true;

    private HashSet<Vector2Int> floorPositions, corridorPositions;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }


    private void CorridorFirstGeneration()
    {
        EventManager.Instance.generateEvent.Invoke();
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        roomData = new RoomData();

        //List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        corridors = CreateCorridors(corridors, floorPositions, potentialRoomPositions, 0, new Vector2Int(0,0));

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions); //rooms

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++) //generating each room
        {
            corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }
        Level.instance.GenerateLevel();
        if (generateItems)
        {
            Level.instance.Add(chestPrefab, new Vector2Int(0, 3));
            AddBarrels();
        }
        if (generateEnemies)
        AddEnemies();

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;
        for (int i = 1; i<corridor.Count; i++) 
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i-1];
            if(previewDirection != Vector2Int.zero &&
                directionFromCell != previewDirection)
            {
                for (int x = -1; x<2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x, y));
                    }
                }
                previewDirection = directionFromCell;
            }
            else
            {
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return Vector2Int.right;
        if (direction == Vector2Int.right)
            return Vector2Int.down;
        if (direction == Vector2Int.down)
            return Vector2Int.left;
        if (direction == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }

    private void AddBarrels()
    {
        List<Vector2Int> barrelSpots = new List<Vector2Int>();
        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> entry in roomsDictionary)
        {
            ItemPlacement itemPlacement = new ItemPlacement(entry.Value);
            barrelSpots = itemPlacement.GetRandomSpotsNearWall(entry.Value, barrelAmnt);
            foreach (var position in barrelSpots)
            {
                Level.instance.Add(barrelPrefab, position);
            }
            barrelSpots.Clear();
        }
    }

    private void AddEnemies()
    {
        int groupNo = 0;
        List<Vector2Int> enemySpots = new List<Vector2Int>();
        List<Vector2Int> enemyPlaces = new List<Vector2Int>();

        if (roomData.roomKind.TryGetValue(RoomType.Enemy, out HashSet<Vector2Int> enemyFloors)) //returns all possible enemy floors
        {
            enemySpots = enemyFloors.ToList(); //list of all enemy floors
        }

        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> entry in rangedEnemiesDictionary)
        {
            foreach (var position in enemySpots)
            {
                if (entry.Value.Contains(position))
                {
                    groupNo++;
                    ItemPlacement itemPlacement = new ItemPlacement(entry.Value);
                    enemyPlaces = itemPlacement.GetRandomSpotsForEnemies(entry.Value, 2);

                    foreach (var enemyPosition in enemyPlaces) //enemyPlaces is vector2 with x and y
                    {
                        Level.instance.Add(batPrefab, enemyPosition, groupNo);
                    }
                }
            }
        }
        groupNo = 0;
        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> entry in meleeEnemiesDictionary)
        {
            foreach (var position in enemySpots)
            {
                if (entry.Value.Contains(position))
                {
                    groupNo++;
                    List<Vector2Int> meleeSpots = entry.Value.Except(enemyPlaces).ToList();
                    HashSet<Vector2Int> meleePlaces = new HashSet<Vector2Int>(meleeSpots);
                    ItemPlacement itemPlacement = new ItemPlacement(entry.Value);
                    enemyPlaces = itemPlacement.GetRandomSpotsForEnemies(meleePlaces, 3); //returns list of spots

                    foreach (var enemyPosition in enemyPlaces) //enemyPlaces is vector2 with x and y
                    {
                        Level.instance.Add(enemyPrefab, enemyPosition, groupNo);
                    }
                }
            }
        }

        enemySpots.Clear();
        enemyPlaces.Clear();
    }



    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) //make rooms
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
            var rangedEnemyPositions = RunRandomWalk(enemyRangedWalkParameters, roomPosition);
            var meleeEnemyPositions = RunRandomWalk(enemyMeleeWalkParameters, roomPosition);
            rangedEnemiesDictionary[roomPosition] = rangedEnemyPositions;
            meleeEnemiesDictionary[roomPosition] = meleeEnemyPositions;
            floorPositions.UnionWith(rangedEnemyPositions);
            floorPositions.UnionWith(meleeEnemyPositions);
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
        foreach (KeyValuePair<RoomType, HashSet<Vector2Int>> pair in roomData.roomKind)
        {
            pair.Value.Clear(); // Clear the HashSet<Vector2Int> for each RoomType
        }
    }

    private void ClearRoomData()
    {
        meleeEnemiesDictionary.Clear();
        rangedEnemiesDictionary.Clear();
        roomsDictionary.Clear();
        //roomColors.Clear();
    }


    private List<List<Vector2Int>> CreateCorridors(List<List<Vector2Int>> corritors, HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions, int corridorAmnt, Vector2Int originPoint)
    {
        var currentPosition = originPoint;
        potentialRoomPositions.Add(currentPosition);
        bool newCorridor;
        corridorAmnt++;
        List<List<Vector2Int>> newCorritors = corritors;

        for (int i = 0; i < 2; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(originPoint, corridorLength);
            newCorridor = false;
            while (newCorridor == false) //keep generating until get amount of corridors
            {
                corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(originPoint, corridorLength);
                if (!floorPositions.Contains(corridor[corridor.Count - 1]))
                {
                    newCorritors.Add(corridor);
                    newCorridor = true;
                    currentPosition = corridor[corridor.Count - 1]; //set current to last in corridor so connected, current pos is the end it will start off of
                    potentialRoomPositions.Add(currentPosition);//only add if not yet, make sure to keep iterating if 
                    floorPositions.UnionWith(corridor);
                    if (i == 0)
                    {
                        roomData.roomKind[RoomType.Empty].Add(currentPosition);
                    }
                    else if (i == 1)
                    {
                        if (corridorAmnt < (corridorCount))
                        {
                            roomData.roomKind[RoomType.Enemy].Add(currentPosition);
                        }
                        else
                        {
                            roomData.roomKind[RoomType.Boss].Add(currentPosition);
                        }
                    }
                }
            }
        }
        if (corridorAmnt < corridorCount)
        {
            CreateCorridors(newCorritors, floorPositions, potentialRoomPositions, corridorAmnt, currentPosition);
        }
        corridorPositions = new HashSet<Vector2Int>(floorPositions); //use later for prefab placement
        return newCorritors;
    }


}
