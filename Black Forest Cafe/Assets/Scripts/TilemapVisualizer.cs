using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile1, floorTile2,floorTile3,wallTop; //tile we can paint on our tile map
    private float randomNum;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        //PaintTiles(floorPositions, floorTilemap, floorTile1);
        PaintTiles(floorPositions, floorTilemap);/*
        randomNum = Random.Range(0,10f);
        if (randomNum < 5f)
        {
            PaintTiles(floorPositions, floorTilemap, floorTile1);
        }
        if (randomNum < 8f)
        {
            PaintTiles(floorPositions, floorTilemap, floorTile2);
        }
        else
        {
            PaintTiles(floorPositions, floorTilemap, floorTile3);
        }*/
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap)
    {
        foreach (var position in positions)
        {
            randomNum = Random.Range(0, 10f);
            if (randomNum < 5f)
            {
                PaintSingleTile(tilemap, floorTile1, position);
            }
            else if (randomNum < 8f)
            {
                PaintSingleTile(tilemap, floorTile2, position);
            }
            else
            {
                PaintSingleTile(tilemap, floorTile3, position);
            }
        }
    }


    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
