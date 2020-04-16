using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#5550FF")]
public class TimeSkip : OceanNode
{
	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        CropManager.instance.TimeSkip();
        
        base.NextNode();
    }
}
