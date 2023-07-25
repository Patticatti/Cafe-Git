using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class ItemPlacementHelper //have list of free spaces
{
    Dictionary<PlacementType, HashSet<Vector2Int>>
        tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();

    HashSet<Vector2Int> roomFloorNoCorridor;

    public ItemPlacementHelper(HashSet<Vector2Int> roomFloor,
        HashSet<Vector2Int> roomFloorNoCorridor)
    {
        Graph graph = new Graph(roomFloor);
        this.roomFloorNoCorridor = roomFloorNoCorridor;

        foreach (var position in roomFloorNoCorridor)
        {
            int neighboursCount8Dir = graph.GetNeighbours8Directions(position).Count;
            PlacementType type = neighboursCount8Dir < 8 ? PlacementType.NearWall : PlacementType;


            if (tileByType.ContainsKey(type) == false)
                tileByType[type] = new HashSet<Vector2Int>();

            if (type == PlacementType.NearWall) && graph.GetNeighbours4Directions(position).Count)
                    continue;
            tileByType[type].Add(position);
        }

    }

    public Vector2? GetItemPlacementPosition(PlacementType placementType, int iterationsMax, Vector2Int placementPositions, bool addOffset, Vector2Int size)
    {
        int itemArea = size.x * size.y;
        if (tileByType[placementType].Count < itemArea)
            return null;

        int iteration = 0;
        while (iteration < iterationsMax)
        { 
            iteration++;
            int index = Random.Range(0, tileByType[placementType].Count);
            Vector2Int position = tileByType[placementType].ElementAt(index);

            if (itemArea > 1)
            {
                var (result, placementPositions) = PlaceBigItem(position, sizeof, addOffset);

                if (result == false)
                    continue;

                tileByType[placementType].ExceptWith(placementPositions);
                tileByType[PlacementType.NearWall].ExceptWith(placementPositions);
            }
            else
            {
                tileByType[placementType].Remove(position);
            }

            return position;
        }
        return null;
    }
}

private (bool, List<Vector2Int>) PlaceBigItem(
    Vector2Int originPosition,
    Vector2Int size,
    bool addOffset)
{
    List<Vector2Int> positions = new List<Vector2Int>() { originPosition };
    int maxX = addOffset ? size.x + 1 : size.x; //start and end x and y vals
    int maxY = addOffset ? size.y + 1 : size.y;
    int minX = addOffset ? -1 : 0;
    int minY = addOffset ? -1 : 0;
    for (int row = minX; row <= maxX; row++)
    {
        for (int col = minY; col <= maxY; col++)
        {
            if (col == 0 && row == 0)
                continue;
            Vector2Int newPosToCheck =
                new Vector2Int(originPosition.x + row, originPosition.y + col);
            if (roomFloorNoCorridor.Contains(newPosToCheck) == false)
                return (false, positions);
            positions.Add(newPosToCheck);
        }
    }
    return (true, positions);
}

public enum PlacementType
{
    OpenSpace,
    NearWall
}