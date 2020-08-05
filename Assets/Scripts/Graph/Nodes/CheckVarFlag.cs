using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#4b40e6")]
public class CheckVarFlag : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode reached;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode notReached;

    public VarIDs flag;
    public int amount = 1;
    
    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        
        if (SaveManager.CheckVarFlag(flag.ToString()) == amount)
        {
            NextNode("reached");
        }
        else
        {
            NextNode("notReached");
        }
    }
}
