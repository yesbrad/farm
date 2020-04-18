using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#e64067")]
public class RemoveFlag : OceanNode
{
    public string flag;

    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        SaveManager.RemoveFlag(flag);
        NextNode();
    }
}
