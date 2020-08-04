using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#f30f00")]
public class SetTileSwap : OceanNode
{
	[Header("Use ID")]
	public TileSwapIDs tileSwapId;

	[Header("Use Scene ID")]
	public bool useSceneID;

	[Header("Is Swapped")]
	public bool swap;
	
	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        TilesSwapManager.instance.SwapTile(useSceneID ? interactable.sceneID : tileSwapId.ToString(), swap);
        
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
