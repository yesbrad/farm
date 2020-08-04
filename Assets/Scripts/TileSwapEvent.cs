using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwapEvent : MonoBehaviour
{
	[Header("Use Swap ID")]
    public TileSwapIDs tileSwapId;

	[Header("Use Scene ID")]
	public Interactable interactable;

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
        originalTile = tilemap.GetTile<Tile>(tilemap.WorldToCell(transform.position));
		TilesSwapManager.instance.RegisterTile(this);
    }

    public void SwapTile(bool swap)
    {
        swapped = swap;
		Tile newTile = swapped ? swapTile : originalTile;
        tilemap.SetTile(tilemap.WorldToCell(transform.position) , newTile);
    }

	public string GetTileID (){
		return interactable != null ? interactable.sceneID : tileSwapId.ToString();
	}

	public void LoadTile (TileSave.TileSaveData data) 
	{
		if (data != null)
			SwapTile(data.swapped);
        else
            SwapTile(startSwapped);
	}
}
