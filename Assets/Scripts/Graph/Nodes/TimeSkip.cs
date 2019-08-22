using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#5550FF")]
public class TimeSkip : OceanNode
{
	public override void Use()
	{
        base.Use();

        CropManager.instance.TimeSkip();
        
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
