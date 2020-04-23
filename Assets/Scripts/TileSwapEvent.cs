using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwapEvent : MonoBehaviour
{
    public TileSwapIDs tileSwapId;

    [Header("Tiles")]
    public TilemapGroups tilemapGroup;
    public Tile swapTile;
    public bool startSwapped;
    
    private Tile originalTile;
    internal bool swapped;
    private Tilemap tilemap;
    
    private void Start()
    {
        tilemap = TilemapGroup.GetTilemap(tilemapGroup);
        originalTile = tilemap.GetTile<Tile>(Vector3Int.RoundToInt(FarmUtilites.GetCenterOfCell(tilemap, transform.position)));
        
        TileSave.TileSaveData data = TilesSwapManager.instance.RegisterTile(this);

        if (data != null)
        {
            SwapTile(data.swapped);
        }
        else
        {
            SwapTile(startSwapped);
        }
    }

    public void SwapTile(bool swap)
    {
        swapped = swap;
        Tile newTile = swapped ? swapTile : originalTile;
        TilemapGroup.GetTilemap(tilemapGroup).SetTile(tilemap.WorldToCell(transform.position) , newTile);
    }
}
