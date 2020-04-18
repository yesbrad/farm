using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#4b40e6")]
public class SetFlag : OceanNode
{
    public string flag;

    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        SaveManager.SetFlag(flag);
        NextNode();
    }
}
