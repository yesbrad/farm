﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FF0000")]
public class DefaultNode : OceanNode
{
	public override void Use(Interactable interactable)
	{
        base.Use(interactable);
        base.NextNode();
    }
}
