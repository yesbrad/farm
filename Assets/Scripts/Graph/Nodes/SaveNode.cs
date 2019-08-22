using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#000000")]
public class SaveNode : OceanNode
{
	public override void Use()
	{
        base.Use();
        GameManager.instance.Save();
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
