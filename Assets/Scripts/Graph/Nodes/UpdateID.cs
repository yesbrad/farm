using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#0FF0FF")]
public class UpdateID : OceanNode
{
    public EventIDs newId;

	public override void Use()
	{
        GameManager.CurrentID = newId;        
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
