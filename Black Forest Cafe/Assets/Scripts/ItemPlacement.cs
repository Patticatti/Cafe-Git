using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class ItemPlacement
{
    Dictionary<PlacementType, HashSet<Vector2Int>>
        tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();//list of 

    HashSet<Vector2Int> floorPositionsNoCorridor;

    public ItemPlacement (HashSet<Vector2Int> floorPositionsNoCorridor) //roomfloor positions
    {
        Graph graph = new Graph(floorPositionsNoCorridor);
        this.floorPositionsNoCorridor = floorPositionsNoCorridor;
        int neighboursCount8Dir;

        foreach (var position in floorPositionsNoCorridor) 
        {
            neighboursCount8Dir = graph.GetNeighbours8Directions(position).Count; //amount of neighbours
            PlacementType type = neighboursCount8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace; //set type either near or not


            if (tileByType.ContainsKey(type) == false) //doesnt exist yet
                tileByType[type] = new HashSet<Vector2Int>();

            //if ((type == PlacementType.NearWall) && (graph.GetNeighbours4Directions(position).Count >0))
                //continue;
            tileByType[type].Add(position); //add position in the location
        }
    }

    public List<Vector2Int> GetRandomSpotsNearWall(HashSet<Vector2Int> floorPositions, int count)
    {
        List<Vector2Int> randomSpots = new List<Vector2Int>();

        if (tileByType.TryGetValue(PlacementType.NearWall, out HashSet<Vector2Int> nearWallPositions))
        {
            int availableCount = nearWallPositions.Count;
            List<Vector2Int> nearWallList = nearWallPositions.ToList();

            if (availableCount <= count)
            {
                randomSpots.AddRange(nearWallList);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    int randomIndex = Random.Range(0, nearWallList.Count);
                    randomSpots.Add(nearWallList[randomIndex]);
                    nearWallList.RemoveAt(randomIndex);
                }
            }
        }

        return randomSpots;
    }


    public List<Vector2Int> GetRandomSpotsForEnemies(HashSet<Vector2Int> floorPositions, int count) //return x amount of random enemy spots from floor tile list
    {
        List<Vector2Int> randomSpots = new List<Vector2Int>();

        if (tileByType.TryGetValue(PlacementType.OpenSpace, out HashSet<Vector2Int> openSpacePositions))
        {
            int availableCount = openSpacePositions.Count;
            List<Vector2Int> openSpaceList = openSpacePositions.ToList();

            if (availableCount <= count)
            {
                randomSpots.AddRange(openSpaceList);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    int randomIndex = Random.Range(0, openSpaceList.Count);
                    randomSpots.Add(openSpaceList[randomIndex]);
                    openSpaceList.RemoveAt(randomIndex);
                }
            }
        }

        return randomSpots;
    }

    public enum PlacementType
    {
        OpenSpace,
        NearWall
    }
}
