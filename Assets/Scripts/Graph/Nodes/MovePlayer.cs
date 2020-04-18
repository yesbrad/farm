using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#e6409b")]
public class MovePlayer : OceanNode
{
    public Vector2 movement;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        PlayerController.instance.Move(movement);
        
        base.NextNode();
    }
}
