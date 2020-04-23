using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#f30f00")]
public class SetTileSwap : OceanNode
{
	public TileSwapIDs tileSwapId;
	public bool swap;
	
	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        TilesSwapManager.instance.SwapTile(tileSwapId, swap);
        
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
