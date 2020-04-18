using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#40e682")]
public class CheckFlag : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode set;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode notSet;

    public string flag;

    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        NextNode(SaveManager.CheckFlag(flag) ? "set" : "notSet");
    }
}
