﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#00FF00")]
public class Start : OceanNode
{
    public EventIDs eventIDs;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);
        base.NextNode();
    }
}
