using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkMapGenerator : AbstractMapGenerator //inherits from
{

    [SerializeField]
    protected SimpleRandomWalkData randomWalkParameters; //allow us to swap diff parameters
    [SerializeField]
    protected SimpleRandomWalkData enemyRangedWalkParameters;
    [SerializeField]
    protected SimpleRandomWalkData enemyMeleeWalkParameters;

    protected override void RunProceduralGeneration() //plus generate enemy positions and stuff
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        HashSet<Vector2Int> rangedPositions = RunRandomWalk(enemyRangedWalkParameters, startPosition);
        HashSet<Vector2Int> meleePositions = RunRandomWalk(enemyMeleeWalkParameters, startPosition); 

        tilemapVisualizer.Clear(); 
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions; //return vector2int
    }
}
