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

    public ItemPlacement(HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> floorPositionsNoCorridor) //roomfloor positions
    {
        Graph graph = new Graph(floorPositions);
        this.floorPositionsNoCorridor = floorPositionsNoCorridor;

        foreach (var position in floorPositionsNoCorridor) //iterate through all rooms
        {
            int neighboursCount8Dir = graph.GetNeighbours8Directions(position).Count; //amount of neighbours
            PlacementType type = neighboursCount8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace; //set type either near or not


            if (tileByType.ContainsKey(type) == false) //doesnt exist yet
                tileByType[type] = new HashSet<Vector2Int>(); 

            //if ((type == PlacementType.NearWall) && (graph.GetNeighbours4Directions(position).Count))
                //continue;
            tileByType[type].Add(position); //add position in the location
        }
    }



    public enum PlacementType
    {
        OpenSpace,
        NearWall
    }
}
